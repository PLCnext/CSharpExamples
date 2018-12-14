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
    [Structure]
    public struct FUExampleStruct
    {
        public int a;
    }
    [Function]
    public static class FU_with_complex_data_type
    {
        // Valid Example
        // The return value must be passed by reference, if it is not elementary
        // Elementary data types must be returned.
        [Execution]
        public static void __Process(
            [Output] ref FUExampleStruct FU_with_complex_data_type,
            [Input] ref FUExampleStruct IN1)
        {
            FU_with_complex_data_type = IN1;
            FU_with_complex_data_type.a++;
        }

        // NOT SUPPORTED! Do not return complex data types. This code pattern is NOT supported by the Engineering tools.
        //[Execution]
        public static FUExampleStruct ProcessReturningStruct([Input] ref FUExampleStruct IN1)
        {
            FUExampleStruct result;
            result = IN1;
            result.a++;

            return result;
        }
    }
}
