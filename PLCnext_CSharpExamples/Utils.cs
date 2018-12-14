using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Iec61131Lib;

namespace ExampleLib
{
    class Utils
    {
        static public unsafe byte[] BytesToArray(byte* data, int length)
        {
            byte[] result = new byte[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = data[i];
            }

            return result;
        }

        static public unsafe string BytesToString(byte* data, int length)
        {
            return new string((sbyte*)data, 0, length, Encoding.UTF8);
        }

        static public unsafe void CopyByteArrayToAny(byte[] src, ref Any dst)
        {
            fixed (byte* bytes = src)
            {
                int length = Math.Min((int)dst.nLength, src.Length);
                Copy(dst.pValue, bytes, (uint)length);
            }
        }

        static public unsafe void Copy(void* dst, void* src, UInt32 size)
        {
            byte* p0 = (byte*)dst;
            byte* p1 = (byte*)src;
            for (uint i = 0; i < size; i++)
            {
                *p0++ = *p1++;
            }
        }

    }
}
