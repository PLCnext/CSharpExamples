#region Copyright

//
// Copyright (c) Phoenix Contact GmbH & Co. KG. All rights reserved.
// Licensed under the MIT. See LICENSE file in the project root for full license information.
//

#endregion Copyright

using Iec61131.Engineering.Prototypes.Common;
using Iec61131.Engineering.Prototypes.Methods;
using Iec61131.Engineering.Prototypes.Types;
using Iec61131.Engineering.Prototypes.Variables;
using System.Iec61131Lib;

namespace ExampleLib
{
    /// <summary>
    /// The example limits a value into a defined range
    /// </summary>
    [Function]
    public static class Fun_with_ANY_NUM
    {
        // All Any parameters must be passed by ref!
        [Execution]
        public unsafe static bool __Process(
            [Input, DataType("ANY_NUM")] ref Any VALUE,
            [Input, DataType("ANY_NUM")] ref Any MIN,
            [Input, DataType("ANY_NUM")] ref Any MAX)
        {
            bool retValue = false;

            // get the size of the connected value type
            uint SIZE_OF_VALUE = VALUE.nLength;

            // Important due to unsafe programming:
            // Check whether all parameters have the same data type!!!
            if (VALUE.pRuntimeTypeHandle != MIN.pRuntimeTypeHandle || VALUE.pRuntimeTypeHandle != MAX.pRuntimeTypeHandle)
            {
                return false;
            }

            // Get the element type constants associate to the runtime type handle.
            // The values are defined in the standard ECMA-335 "Common Language Infrastructure (CLI)",
            // Partition II, chapter II.23.1.16 "Element types used in signatures")
            Eclr.TypeCode code = (Eclr.TypeCode)Eclr.TypeInfo.GetTypeCode(VALUE.pRuntimeTypeHandle);

            // type dependent action
            switch (code)
            {
                case Eclr.TypeCode.SByte:
                    {
                        sbyte tempValue = *(sbyte*)VALUE.pValue;
                        sbyte tempMin = *(sbyte*)MIN.pValue;
                        sbyte tempMax = *(sbyte*)MAX.pValue;

                        if (tempValue >= tempMin && tempValue <= tempMax)
                            retValue = true;
                        break;
                    }
                case Eclr.TypeCode.Byte:
                    {
                        byte tempValue = *(byte*)VALUE.pValue;
                        byte tempMin = *(byte*)MIN.pValue;
                        byte tempMax = *(byte*)MAX.pValue;

                        if (tempValue >= tempMin && tempValue <= tempMax)
                            retValue = true;
                        break;
                    }
                case Eclr.TypeCode.Int16:
                    {
                        short tempValue = *(short*)VALUE.pValue;
                        short tempMin = *(short*)MIN.pValue;
                        short tempMax = *(short*)MAX.pValue;

                        if (tempValue >= tempMin && tempValue <= tempMax)
                            retValue = true;
                        break;
                    }
                case Eclr.TypeCode.UInt16:
                    {
                        ushort tempValue = *(ushort*)VALUE.pValue;
                        ushort tempMin = *(ushort*)MIN.pValue;
                        ushort tempMax = *(ushort*)MAX.pValue;

                        if (tempValue >= tempMin && tempValue <= tempMax)
                            retValue = true;
                        break;
                    }
                case Eclr.TypeCode.Int32:
                    {
                        int tempValue = *(int*)VALUE.pValue;
                        int tempMin = *(int*)MIN.pValue;
                        int tempMax = *(int*)MAX.pValue;

                        if (tempValue >= tempMin && tempValue <= tempMax)
                            retValue = true;
                        break;
                    }
                case Eclr.TypeCode.UInt32:
                    {
                        uint tempValue = *(uint*)VALUE.pValue;
                        uint tempMin = *(uint*)MIN.pValue;
                        uint tempMax = *(uint*)MAX.pValue;

                        if (tempValue >= tempMin && tempValue <= tempMax)
                            retValue = true;
                        break;
                    }
                case Eclr.TypeCode.Int64:
                    {
                        long tempValue = *(long*)VALUE.pValue;
                        long tempMin = *(long*)MIN.pValue;
                        long tempMax = *(long*)MAX.pValue;

                        if (tempValue >= tempMin && tempValue <= tempMax)
                            retValue = true;
                        break;
                    }
                case Eclr.TypeCode.UInt64:
                    {
                        ulong tempValue = *(ulong*)VALUE.pValue;
                        ulong tempMin = *(ulong*)MIN.pValue;
                        ulong tempMax = *(ulong*)MAX.pValue;

                        if (tempValue >= tempMin && tempValue <= tempMax)
                            retValue = true;
                        break;
                    }
                case Eclr.TypeCode.Single:
                    {
                        float tempValue = *(float*)VALUE.pValue;
                        float tempMin = *(float*)MIN.pValue;
                        float tempMax = *(float*)MAX.pValue;

                        if (tempValue >= tempMin && tempValue <= tempMax)
                            retValue = true;
                        break;
                    }
                case Eclr.TypeCode.Double:
                    {
                        double tempValue = *(double*)VALUE.pValue;
                        double tempMin = *(double*)MIN.pValue;
                        double tempMax = *(double*)MAX.pValue;

                        if (tempValue >= tempMin && tempValue <= tempMax)
                            retValue = true;
                        break;
                    }
            }

            return retValue;
        }
    }
}
