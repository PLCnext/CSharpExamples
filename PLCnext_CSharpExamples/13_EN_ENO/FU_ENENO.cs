#region Copyright
//  
// Copyright (c) Phoenix Contact GmbH & Co. KG. All rights reserved.  
// Licensed under the MIT. See LICENSE file in the project root for full license information.  
//  
#endregion

using System;
using System.Iec61131Lib;
using Eclr;
using Iec61131.Engineering.Prototypes.Types;
using Iec61131.Engineering.Prototypes.Variables;
using Iec61131.Engineering.Prototypes.Methods;
using Iec61131.Engineering.Prototypes.Common;

namespace ExampleLib
{
    [Function, DataType("ANY_NUM")]
    public static class FU_ENENO
    {

        [Execution]
        public static void __Process(
            [Output] ref Any Fun_with_ANY,
            [Input, DataType("ANY_NUM")] ref Any VALUE,
            [Input, DataType("ANY_NUM")] ref Any MIN,
            [Input, DataType("ANY_NUM")] ref Any MAX)
        {
            
        }
    }
}
