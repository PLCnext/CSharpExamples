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
    [Function, DataType("LWORD")]
    public static class Fun_with_ANY_BIT
    {
        // All Any parameters must be passed by ref!
        [Execution]
        public unsafe static ulong __Process(
            [Input, DataType("ANY_BIT")] ref Any VALUE)
        {
            ulong retValue = 0;

            // get the size of the connected value type
            uint SIZE_OF_VALUE = VALUE.nLength;

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
                            retValue = 1;
                        }
                        else
                        {
                            retValue = 0;
                        }
                        break;
                    }
                case Eclr.TypeCode.Byte:
                    {
                        byte tempValue = *(byte*)VALUE.pValue;
                        retValue = tempValue;
                        break;
                    }
                case Eclr.TypeCode.UInt16:
                    {
                        ushort tempValue = *(ushort*)VALUE.pValue;
                        retValue = tempValue;
                        break;
                    }
                case Eclr.TypeCode.UInt32:
                    {
                        uint tempValue = *(uint*)VALUE.pValue;
                        retValue = tempValue;
                        break;
                    }
                case Eclr.TypeCode.UInt64:
                    {
                        ulong tempValue = *(ulong*)VALUE.pValue;
                        retValue = tempValue;
                        break;
                    }
            }

            return retValue;
        }
    }
}
