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
    /// <summary>
    /// Default Function with EN/ENO handled by the compiler
    /// </summary>
    [Function]
    public static class FU_wo_ENENO
    {
        [Execution]
        // The return value datatype can be either 
        // the one of your function Output or void
        public static short __Process(
            // If the data type is void, the Output must be set explicitly
            // with the class name (function name)
            // [Output] short FU_with_ENENO,
            [Input] short IN1,
            [Input, DataType("WORD")] ushort IN2)
        {
            // Make shure the return value is well defined
            short Function1 = 0;

            //
            // TODO: Write the logic of the function
            //

            // Return the result
            return Function1;
        }
    }
    
    /// <summary>
    /// Function with ENO handled inside the function
    /// </summary>
    [Function]
    public static class FU_with_ENENO
    {
        [Execution]
        // Set the type of the return value to bool
        public static bool __Process(
            // Add the class name (function name) as single Output parameter
            [Output] short FU_with_ENENO,
            [Input] short IN1,
            [Input, DataType("WORD")] ushort IN2)
        {
            // Make shure the Output value is well defined
            FU_with_ENENO = 0;

            //
            // TODO: Write the logic of the function
            // and implement the condition 
            // for changing ENO to false e.g.
            if(IN1 < 0)
                return false;

            // Return ENO
            return true;
        }
    }

}
