using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using MarkupData;

namespace Tracking
{
    // Структура для составления списка значений с индесами
    public struct IndexedDouble
    {
        public double Value;
        public int Index;
    }

    // Класс реализует отслеживание методом Track из алгоритма TLD
    public class TldTracker : Tracker
    {
        override public bool TrackBox(Image srcImage, Image dstImage,
            Box srcBox, out Box dstBox)
        {
            List<PointF> srcPts, dstPts, retPts;
            List<byte> srcStatus, dstStatus, retStatus;
            List<float> dstError, retError;
            List<IndexedDouble> indexedError, sortedIndexedError;

            // Вычисляем координаты точек сетки на исходном кадре
            m_CalcSrcPts(srcBox, 5, 5, out srcPts, out srcStatus);
            // Отслеживание точек "туда"
            OCV.OpticalFlowLK(srcImage, dstImage, 3, srcPts, 
                out dstPts, out dstStatus, out dstError, srcStatus);
            // Отслеживание точек "обратно"
            OCV.OpticalFlowLK(dstImage, srcImage, 3, dstPts, out retPts, 
                out retStatus, out retError, dstStatus);
            // Вычисляем показатели погрешности отслеживания точек
            m_CalcErrorList(srcPts, retPts, retStatus, dstError, retError, 
                out indexedError);
            // Сортируем массив отслеженных точек по возрастанию погрешности
            m_SortErrorList(indexedError, out sortedIndexedError);
            // Передвигаем рамку объекта на смещение, усредненное по всем
            // точкам, расположенным до середины отсортированного списка
            m_CalcDstBox(srcPts, dstPts, sortedIndexedError, srcBox, 
                out dstBox);
            return true;
        }

        override public bool TrackMarker(Image srcImage, Image dstImage,
            Marker srcMarker, out Marker dstMarker)
        {
            throw new Exception("TLD: Marker tracking is not supported!");
        }

        override public bool CanTrackBox()
        {
            return true;
        }

        override public bool CanTrackMarker()
        {
            return false;
        }

        // Метод создает массив координат узлов сетки размером ndivx X ndivy,
        // наложенной на рамку srcBox. 
        private void m_CalcSrcPts(Box srcBox, int ndivx, int ndivy,
            out List<PointF> srcPts, out List<byte> srcStatus)
        {
            if (ndivx == 0 || ndivy == 0) 
                throw new Exception("TLD: Wrong grid configuration!");
            
            PointF step = new PointF();
            step.X = srcBox.Width * (1.0f / ndivx);
            step.Y = srcBox.Height * (1.0f / ndivy);
            PointF point;
            srcPts = new List<PointF>();
            srcStatus = new List<byte>();

            for (int i = 0; i < ndivx; i++) // по столбцам
            for (int j = 0; j < ndivy; j++) // по строкам
            {
                point = new PointF();
                point.X = srcBox.PosX + i * step.X;
                point.Y = srcBox.PosY + j * step.Y;
                srcPts.Add(point);
                srcStatus.Add(1);
            }
        }

        // Метод вычисляет значения показателей надежности отслеживания точек
        // и сохраняет их в индексированный список. 
        private void m_CalcErrorList(
            List<PointF> srcPts,// координаты точек на предыдущем кадре
            List<PointF> retPts,// координаты точек после отслеживания точек 
                                // с предыдущего кадра на следующий и обратно
            List<byte> retStatus, // признаки успешного отслеживания retPts
            List<float> dstError, // погрешность отслеживания точек с 
                                  // предыдущего кадра на следующий
            List<float> retError, // погрешность отслеживания точек со
                                  // следующего кадра на предыдущий
            out List<IndexedDouble> indexedError // сводные показатели 
        )                         // надежности отслеживания точек                  
        {
            IndexedDouble error;
            indexedError = new List<IndexedDouble>();

            for (int i = 0; i < srcPts.Count; i++)
            if (retStatus[i] == 1)
            {
                double distX = retPts[i].X - srcPts[i].X;
                double distY = retPts[i].Y - srcPts[i].Y;
                double dist = Math.Sqrt(distX * distX + distY * distY);
                error = new IndexedDouble();
                error.Value = dist;
                error.Index = i;
                indexedError.Add(error);
            }
        }

        // Метод сортировки индексированного списка ошибок по возрастанию
        private void m_SortErrorList(List<IndexedDouble> indexedError, 
            out List<IndexedDouble> sortedIndexedError)
        {
            sortedIndexedError = new List<IndexedDouble>();
            IndexedDouble[] indexedErrorArr = indexedError.ToArray();
            int numel = indexedError.Count;
            IndexedDouble minError = new IndexedDouble();
            int minIndex;
            for (int i = 0; i < numel - 1; i++)
            {
                minError = indexedErrorArr[i];
                minIndex = i;
                for (int j = i + 1; j < numel; j++)
                if (indexedErrorArr[j].Value < minError.Value)
                {
                    minError = indexedErrorArr[j];
                    minIndex = j;
                }
                // Переставляем местами текущий элемент с наименьшим
                if (minIndex != i)
                {
                    indexedErrorArr[minIndex] = indexedErrorArr[i];
                    indexedErrorArr[i] = minError;
                }
                // Добавляем наименьший элемент в результирующий список
                sortedIndexedError.Add(minError);
            }
        }

        // Метод вычисляет среднее смещение точек, попавших до медианного
        // значения ошибки и применяет его к исходным координатам рамки
        // объекта на предыдущем кадре
        private void m_CalcDstBox(
            List<PointF> srcPts,
            List<PointF> dstPts,
            List<IndexedDouble> sortedIndexedError,
            Box srcBox,
            out Box dstBox)
        {
            int numel = sortedIndexedError.Count / 2;
            dstBox = new Box();

            if (numel == 0)
            {
                dstBox = srcBox;
                return;
            }
            PointF disp = new PointF();
            PointF meanDisp = new PointF(0, 0);
            for (int i = 0; i < numel; i++)
            {
                int index = sortedIndexedError[i].Index;
                disp.X = dstPts[index].X - srcPts[index].X;
                disp.Y = dstPts[index].Y - srcPts[index].Y;
                meanDisp.X += disp.X;
                meanDisp.Y += disp.Y;
            }
            meanDisp.X *= (1.0f / numel);
            meanDisp.Y *= (1.0f / numel);
            dstBox.PosX = srcBox.PosX + (int)meanDisp.X;
            dstBox.PosY = srcBox.PosY + (int)meanDisp.Y;
            dstBox.Width = srcBox.Width;
            dstBox.Height = srcBox.Height;
        }
    }
}
