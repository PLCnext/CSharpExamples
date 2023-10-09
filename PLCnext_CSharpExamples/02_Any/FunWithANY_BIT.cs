#region Copyright

//
// Copyright (c) Phoenix Contact GmbH & Co. KG. All rights reserved.
// Licensed under the MIT. See LICENSE file in the project root for full license information.
//

#endregion Copyright

using Iec61131.Engineering.Prototypes.Common;
using Iec61131.Engineering.Prototypes.Methods;
using Iec61131.Engineering.Prototypes.Pragmas;
using Iec61131.Engineering.Prototypes.Types;
using Iec61131.Engineering.Prototypes.Variables;
using System;
using System.Iec61131Lib;
using System.Runtime.InteropServices;

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
            if (code == Eclr.TypeCode.Int16)    // short (2 bytes)
            {
                short tempValue = *((short*)VALUE.pValue);
                retValue = (ulong)tempValue;
            }
            else if (code == Eclr.TypeCode.UInt16)  // unsigned short (2 bytes)
            {
                ushort tempValue = *((ushort*)VALUE.pValue);
                retValue = (ulong)tempValue;
            }
            else if (code == Eclr.TypeCode.Int32)   // int (4 Bytes)
            {
                int tempValue = *((int*)VALUE.pValue);
                retValue = (ulong)tempValue;
            }
            else if (code == Eclr.TypeCode.UInt32)  // unsigned (4 Bytes)
            {
                uint tempValue = *((uint*)VALUE.pValue);
                retValue = (ulong)tempValue;
            }
            else
            {
                // ...
            }

            return retValue;
        }
    }
}
