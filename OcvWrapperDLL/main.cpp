// main.cpp: ������� ������ ������������ ����������-������� ������� OpenCV
// https://www.c-sharpcorner.com/uploadfile/tanmayit08/unmanaged-cpp-dll-call-from-managed-C-Sharp-application/

#include <stdio.h>


extern "C"  
{
       __declspec(dllexport) int add(int a,int b) 
       {
              return a+b;
       }
       __declspec(dllexport) int subtract(int a,int b) 
       {
              return a-b;
       }
}
