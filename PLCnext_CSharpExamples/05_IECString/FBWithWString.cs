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
    // Define a string data type with a maximum string length of 200 characters
    // Size is 2x string length + 4 byte header + 2 byte terminating zero
    // Mark the declaration with a String attribute, define the length and set the WString characterization to true
    // Use IecWString class for the field
    [String(200, true)]
    [StructLayout(LayoutKind.Explicit, Size = 406)]
    public struct TWString200
    {
        // Fields
        [FieldOffset(0)]
        public IecWString s;  // This member must have the name 's' because the name is evaluated by PLCnext Engineer!

        // Methods
        // Init is needed to set the maximum size and called in the initialization
        public void Init()
        {
            s.m_cap = 200;
            s.Empty();
        }
    }

    [FunctionBlock]
    public class IecWStringFunctionBlock
    {
        [Input]
        public TWString200 IN1;
        [Output]
        public TWString200 OUT;

        [Initialization]
        public void __Init()
        {
            IN1.Init();
            OUT.Init();
        }

        [Execution]
        public void __Process()
        {
            // adding a string to the input string; one character is a 4byte UTF-16 character
            OUT.s.Init(string.Format("{0} {1}", IN1.s.ToString(), "_€𤽜_End"));
        }
    }
}
