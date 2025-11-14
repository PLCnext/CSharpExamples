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
    /// The example function block limits a value into a defined range
    /// and returns the result as integer.
    /// </summary>
    [FunctionBlock]
    public class FB1_with_ANY_BIT
    {
        [Input, DataType("ANY_BIT")]
        public Any VALUE;

        // Caution: Output parameters are not supported as ANY. Use InOut parameters instead (see FB2WithAny.cs)
        [Output, DataType("LWORD")]
        public ulong RESULT;

        [Output, DataType("UDINT")]
        public uint SIZE_OF_VALUE;

        [Initialization]
        public void __Init()
        {
        }

        [Execution]
        public unsafe void __Process()
        {
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
                        if (tempValue)
                        {
                            RESULT = 1;
                        }
                        else
                        {
                            RESULT = 0;
                        }
                        break;
                    }
                case Eclr.TypeCode.Byte:
                    {
                        byte tempValue = *(byte*)VALUE.pValue;
                        RESULT = tempValue;
                        break;
                    }
                case Eclr.TypeCode.UInt16:
                    {
                        ushort tempValue = *(ushort*)VALUE.pValue;
                        RESULT = tempValue;
                        break;
                    }
                case Eclr.TypeCode.UInt32:
                    {
                        uint tempValue = *(uint*)VALUE.pValue;
                        RESULT = tempValue;
                        break;
                    }
                case Eclr.TypeCode.UInt64:
                    {
                        ulong tempValue = *(ulong*)VALUE.pValue;
                        RESULT = tempValue;
                        break;
                    }
            }
        }
    }
}
