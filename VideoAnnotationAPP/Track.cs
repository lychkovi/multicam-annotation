// Класс представляет размеченную траекторию отдельного объета на видео.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace VideoAnnotationAPP
{

    public class TrackNode : IXmlSerializable
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
        public int iframe;

        #region IXmlSerializable Members
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            XmlSerializer intSerializer = new XmlSerializer(typeof(int));

            bool wasEmpty = reader.IsEmptyElement;
            reader.Read();

            if (wasEmpty)
                return;

            {
                reader.ReadStartElement("left");
                left = (int)intSerializer.Deserialize(reader);
                reader.ReadEndElement();

                reader.ReadStartElement("top");
                top = (int)intSerializer.Deserialize(reader);
                reader.ReadEndElement();

                reader.ReadStartElement("right");
                right = (int)intSerializer.Deserialize(reader);
                reader.ReadEndElement();

                reader.ReadStartElement("bottom");
                bottom = (int)intSerializer.Deserialize(reader);
                reader.ReadEndElement();
            }
            reader.ReadEndElement();
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            XmlSerializer intSerializer = new XmlSerializer(typeof(int));

            //writer.WriteStartElement("TrackNode");

            writer.WriteStartElement("left");
            intSerializer.Serialize(writer, left);
            writer.WriteEndElement();

            writer.WriteStartElement("top");
            intSerializer.Serialize(writer, top);
            writer.WriteEndElement();

            writer.WriteStartElement("right");
            intSerializer.Serialize(writer, right);
            writer.WriteEndElement();

            writer.WriteStartElement("bottom");
            intSerializer.Serialize(writer, bottom);
            writer.WriteEndElement();

            //writer.WriteEndElement();
        }
        #endregion
    }

    public class Track : SerializableDictionary<int, TrackNode>, IXmlSerializable
    {
        public int objType;            // тип движущегося объекта
        public int iframeStart;        // индекс начального кадра траектории
        public int iframeEnd;          // индекс конечного кадра траектории

        protected override string listName
        {
            get { return "NodeList"; }
        } 

        protected override string itemName
        {
            get { return "NodeListItem"; }
        }

        protected override string keyName
        {
            get { return "FrameNumber"; }
        }

        protected override string valueName
        {
            get { return "Value"; }
        }

        #region IXmlSerializable Members

        public new void ReadXml(System.Xml.XmlReader reader)
        {
            XmlSerializer intSerializer = new XmlSerializer(typeof(int));

            bool wasEmpty = reader.IsEmptyElement;
            reader.Read();

            if (wasEmpty)
                return;

            {
                reader.ReadStartElement(listName);

                reader.ReadStartElement("objType");
                objType = (int)intSerializer.Deserialize(reader);
                reader.ReadEndElement();

                reader.MoveToContent();
                ((SerializableDictionary<int, TrackNode>)this).ReadXml(reader);

                reader.ReadEndElement();
            }
            reader.ReadEndElement();
        }

        public new void WriteXml(System.Xml.XmlWriter writer)
        {
            XmlSerializer intSerializer = new XmlSerializer(typeof(int));

            writer.WriteStartElement(listName);

            writer.WriteStartElement("objType");
            intSerializer.Serialize(writer, objType);
            writer.WriteEndElement();

            ((SerializableDictionary<int, TrackNode>)this).WriteXml(writer);

            writer.WriteEndElement();
        }
        #endregion
    }

    public class TrackList : SerializableDictionary<int, Track>, IXmlSerializable
    {
        public string videoFilePath;// путь к файлу видеозаписи

        protected override string listName
        {
            get { return "TrackList"; }
        }
        
        protected override string itemName
        {
            get { return "TrackListItem"; }
        }

        protected override string keyName
        {
            get { return "TrackID"; }
        }

        protected override string valueName
        {
            get { return "Value"; }
        }

        #region IXmlSerializable Members

        public new void ReadXml(XmlReader reader)
        {
            XmlSerializer stringSerializer = new XmlSerializer(typeof(string));

            bool wasEmpty = reader.IsEmptyElement;
            //reader.Read();

            if (wasEmpty)
                return;

            reader.ReadStartElement(listName);

            reader.ReadStartElement("videoFilePath");
            videoFilePath = (string)stringSerializer.Deserialize(reader);
            reader.ReadEndElement();

            reader.MoveToContent();
            ((SerializableDictionary<int, Track>)this).ReadXml(reader);

            reader.ReadEndElement();
        }

        public new void WriteXml(System.Xml.XmlWriter writer)
        {
            XmlSerializer stringSerializer = new XmlSerializer(typeof(string));

            writer.WriteStartElement(listName);

            writer.WriteStartElement("videoFilePath");
            stringSerializer.Serialize(writer, videoFilePath);
            writer.WriteEndElement();

            ((SerializableDictionary<int, Track>)this).WriteXml(writer);

            writer.WriteEndElement();
        }
        #endregion

        // Метод очищает содержимое экземпляра класса
        public void Reset()
        {
            Clear();
            videoFilePath = "";
        }
    }
}
