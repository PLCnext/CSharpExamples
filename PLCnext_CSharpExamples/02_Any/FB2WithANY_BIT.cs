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
    public class FB2_with_ANY_BIT
    {
        [Input, DataType("ANY_BIT")]
        public Any VALUE;

        [Output, DataType("UDINT")]
        public uint SIZE_OF_VALUE;

        // Output parameters are not supported as ANY. Use InOut parameters instead.
        [InOut, DataType("ANY_BIT")]
        public Any RESULT;

        [Initialization]
        public void __Init()
        {
        }

        [Execution]
        public unsafe void __Process()
        {
            // Important due to unsafe programming:
            // Check whether all parameters have the same data type!!!
            if (VALUE.pRuntimeTypeHandle != RESULT.pRuntimeTypeHandle)
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
            switch (code)
            {
                case Eclr.TypeCode.Boolean:
                    {
                        bool tempValue = *(bool*)VALUE.pValue;
                        bool* pResult = (bool*)RESULT.pValue;

                        *pResult = tempValue;
                        break;
                    }
                case Eclr.TypeCode.Byte:
                    {
                        byte tempValue = *(byte*)VALUE.pValue;
                        byte* pResult = (byte*)RESULT.pValue;

                        *pResult = tempValue;
                        break;
                    }
                case Eclr.TypeCode.UInt16:
                    {
                        ushort tempValue = *(ushort*)VALUE.pValue;
                        ushort* pResult = (ushort*)RESULT.pValue;

                        *pResult = tempValue;
                        break;
                    }
                case Eclr.TypeCode.UInt32:
                    {
                        uint tempValue = *(uint*)VALUE.pValue;
                        uint* pResult = (uint*)RESULT.pValue;

                        *pResult = tempValue;
                        break;
                    }
                case Eclr.TypeCode.UInt64:
                    {
                        ulong tempValue = *(ulong*)VALUE.pValue;
                        ulong* pResult = (ulong*)RESULT.pValue;

                        *pResult = tempValue;
                        break;
                    }
            }
        }
    }
}
