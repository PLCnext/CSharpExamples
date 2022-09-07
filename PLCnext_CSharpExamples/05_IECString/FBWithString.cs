#region Copyright

//
// Copyright (c) Phoenix Contact GmbH & Co. KG. All rights reserved.
// Licensed under the MIT. See LICENSE file in the project root for full license information.
//

#endregion Copyright

using Iec61131.Engineering.Prototypes.Methods;
using Iec61131.Engineering.Prototypes.Types;
using Iec61131.Engineering.Prototypes.Variables;
using System;
using System.Iec61131Lib;
using System.Runtime.InteropServices;

namespace ExampleLib
{
    // Define a string data type with a maximum string length of 200 characters
    // Size is string length + 4 byte header + 1 byte terminating zero + padding for two byte alignment
    // Mark the declaration with a String attribute and define the length
    [String(200)]
    [StructLayout(LayoutKind.Explicit, Size = 206)]
    public struct TString200
    {
        // Fields
        [FieldOffset(0)]
        public IecStringEx s;  // This member must have the name 's' because the name is evaluated by PLCnext Engineer!

        // Methods
        // Init is needed to set the maximum size and called in the initialization

        public void Init()
        {
            s.maximumLength = 200;
            s.Empty();
        }
    }

    // Input and InOut parameter are passed by value
    [FunctionBlock]
    public class FB_with_string1
    {
        [Input]
        public IecString80 VALUE;

        [Input]
        public short MESSAGE_ID;

        [Output]
        public IecString80 RESULT1; // Standard string

        [Output]
        public TString200 RESULT2;  // User defined string

        [Output]
        public short RESULT1_SIZE;

        [Output]
        public short RESULT2_SIZE;

        [Initialization]
        public void __Init()
        {
            // Call ctor of strings in order to set the maximum size
            VALUE.ctor();
            RESULT1.ctor();
            RESULT2.Init();
        }

        [Execution]
        public void __Process()
        {
            // Assign one IecString to another
            IecStringEx.Copy(ref VALUE.s, ref RESULT1.s);

            // assign .Net String to an IecString
            switch (MESSAGE_ID)
            {
                case 0:
                    RESULT2.s.Init("Nothing selected!");
                    break;

                case 1:
                    RESULT2.s.Init("The quick brown fox jumps over the lazy dog.");
                    break;

                case 2:
                    RESULT2.s.Init("The quick brown fox jumps over the lazy dog. The quick brown fox jumps over the lazy dog. The quick brown fox jumps over the lazy dog.");
                    break;

                default:
                    RESULT2.s.Init("Undefined message ID!");
                    break;
            }
            RESULT1_SIZE = RESULT1.s.currentLength;
            RESULT2_SIZE = RESULT2.s.currentLength;
        }
    }

    // Input and InOut parameter can be passed by reference. This saves memory and CPU time for copying values for large arrays and structures.
    [FunctionBlock]
    public class FB_with_string2
    {
        [InOut]
        public unsafe IecStringEx* VALUE;

        [Input]
        public short MESSAGE_ID;

        [InOut]
        public unsafe IecStringEx* RESULT1;

        [InOut]
        public unsafe IecStringEx* RESULT2;

        [Initialization]
        public void __Init()
        {
            //Calling the ctor is not allowed when the string is passed by reference
            //at initialization time the pointer has no valid reference
        }

        [Execution]
        public void __Process()
        {
            unsafe
            {
                // Assign one IecString to another (assign string VALUE to string RESULT1)
                IecStringEx.Copy(ref *VALUE, ref *RESULT1);

                // assign .Net String to an IecString
                switch (MESSAGE_ID)
                {
                    case 0:
                        RESULT2->Init("Nothing selected!");
                        break;

                    case 1:
                        RESULT2->Init("The quick brown fox jumps over the lazy dog.");
                        break;

                    case 2:
                        RESULT2->Init("The quick brown fox jumps over the lazy dog. The quick brown fox jumps over the lazy dog. The quick brown fox jumps over the lazy dog.");
                        break;

                    default:
                        RESULT2->Init("Undefined message ID!");
                        break;
                }
            }
        }
    }
}
