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
    [Function, DataType("ANY_BIT")]
    public static class Fun_with_ANY_BIT
    {
        // All Any parameters must be passed by ref!
        [Execution]
        public unsafe static void __Process(
            [DataType("ANY")] ref Any Fun_with_ANY_BIT,
            [Input, DataType("ANY_BIT")] ref Any VALUE)
        {
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
                short* pResult = (short*)Fun_with_ANY_BIT.pValue;

                *pResult = tempValue;
            }
            else if (code == Eclr.TypeCode.UInt16)  // unsigned short (2 bytes)
            {
                ushort tempValue = *((ushort*)VALUE.pValue);
                ushort* pResult = (ushort*)Fun_with_ANY_BIT.pValue;

                *pResult = tempValue;
            }
            else if (code == Eclr.TypeCode.Int32)   // int (4 Bytes)
            {
                int tempValue = *((int*)VALUE.pValue);
                int* pResult = (int*)Fun_with_ANY_BIT.pValue;

                *pResult = tempValue;
            }
            else if (code == Eclr.TypeCode.UInt32)  // unsigned (4 Bytes)
            {
                uint tempValue = *((uint*)VALUE.pValue);
                uint* pResult = (uint*)Fun_with_ANY_BIT.pValue;

                *pResult = tempValue;
            }
            else
            {
                // ...
            }
        }
    }
}
