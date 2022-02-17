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
    /// The example FB limits a value into a defined range
    /// and returns the result as Any
    /// </summary>
    [FunctionBlock]
    public class FB2_with_ANY_NUM
    {
        [Input, DataType("ANY_NUM")]
        public Any VALUE;

        [Input, DataType("ANY_NUM")]
        public Any MIN;

        [Input, DataType("ANY_NUM")]
        public Any MAX;

        // Caution: Output parameters are not supported as ANY. Use InOut parameters instead.
        [InOut, DataType("ANY_NUM")]
        public Any RESULT;

        [Output]
        public uint SIZE_OF_VALUE;

        [Initialization]
        public void __Init()
        {
        }

        [Execution]
        public unsafe void __Process()
        {
            // Important due to unsafe programming:
            // Check whether all parameters have the same data type!!!
            if (VALUE.pRuntimeTypeHandle != RESULT.pRuntimeTypeHandle || VALUE.pRuntimeTypeHandle != MIN.pRuntimeTypeHandle || VALUE.pRuntimeTypeHandle != MAX.pRuntimeTypeHandle)
            {
                return;
            }

            // get the size of the connected value type
            SIZE_OF_VALUE = VALUE.nLength;

            // Get the element type constants associate to the runtime type handle.
            // The values are defined in the standard ECMA-335 "Common Language Infrastructure (CLI)",
            // Partition II, chapter II.23.1.16 "Element types used in signatures")
            Eclr.TypeCode code = (Eclr.TypeCode)Eclr.TypeInfo.GetTypeCode(VALUE.pRuntimeTypeHandle);

            // type dependent action
            if (code == Eclr.TypeCode.Int16)    // short (2 bytes)
            {
                short tempValue = *((short*)VALUE.pValue);
                short tempMin = *((short*)MIN.pValue);
                short tempMax = *((short*)MAX.pValue);
                short* pResult = (short*)RESULT.pValue;

                if (tempValue < tempMin)
                {
                    tempValue = tempMin;
                }

                if (tempValue > tempMax)
                {
                    tempValue = tempMax;
                }

                *pResult = tempValue;
            }
            else if (code == Eclr.TypeCode.UInt16)  // unsigned short (2 bytes)
            {
                ushort tempValue = *((ushort*)VALUE.pValue);
                ushort tempMin = *((ushort*)MIN.pValue);
                ushort tempMax = *((ushort*)MAX.pValue);
                ushort* pResult = (ushort*)RESULT.pValue;

                if (tempValue < tempMin)
                {
                    tempValue = tempMin;
                }

                if (tempValue > tempMax)
                {
                    tempValue = tempMax;
                }

                *pResult = tempValue;
            }
            else if (code == Eclr.TypeCode.Int32)   // int (4 bytes)
            {
                int tempValue = *((int*)VALUE.pValue);
                int tempMin = *((int*)MIN.pValue);
                int tempMax = *((int*)MAX.pValue);
                int* pResult = (int*)RESULT.pValue;

                if (tempValue < tempMin)
                {
                    tempValue = tempMin;
                }

                if (tempValue > tempMax)
                {
                    tempValue = tempMax;
                }

                *pResult = tempValue;
            }
            else if (code == Eclr.TypeCode.UInt32)  // unsigned int (4 bytes)
            {
                uint tempValue = *((uint*)VALUE.pValue);
                uint tempMin = *((uint*)MIN.pValue);
                uint tempMax = *((uint*)MAX.pValue);
                uint* pResult = (uint*)RESULT.pValue;

                if (tempValue < tempMin)
                {
                    tempValue = tempMin;
                }

                if (tempValue > tempMax)
                {
                    tempValue = tempMax;
                }

                *pResult = tempValue;
            }
            else
            {
                // ...
            }
        }
    }
}
