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
using System;
using System.Iec61131Lib;

namespace ExampleLib
{
    [FunctionBlock]
    public class Finalizer
    {
        [Input]
        public short IN1;
        [Input]
        public short IN2;
        [Output, DataType("WORD")]
        public ushort OUT;

        [Initialization]
        public void __Init()
        {
            //
            // TODO: Initialize the variables of the function block here
            //
        }
        ~Finalizer()
        {

        }


        [Execution]
        public void __Process()
        {
            OUT = (ushort)(IN1 + IN2);
        }
    }
}
