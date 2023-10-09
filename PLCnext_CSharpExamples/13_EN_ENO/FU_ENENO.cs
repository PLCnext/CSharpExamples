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

namespace ExampleLib
{
    /// <summary>
    /// Default Function with EN/ENO handled by the compiler
    /// </summary>
    [Function]
    public static class FU_wo_ENENO
    {
        [Execution]
        // The return value data type can be either
        // the one of your function Output or void
        public static short __Process(
            // If the data type is void, the Output must be set explicitly
            // with the class name (function name)
            // [Output] short FU_with_ENENO,
            [Input, DataType("INT")] short IN1,
            [Input, DataType("INT")] short IN2)
        {
            // TODO: Write the logic of the function
            // Make sure the return value is well defined
            short Function1 = (short) (IN1 + IN2);

            // Return the result
            return Function1;
        }
    }

    /// <summary>
    /// Function with ENO handled inside the function
    /// </summary>
    // Mark the class with [Eno] attribute
    [Eno]
    [Function]
    public static class FU_with_ENENO
    {
        [Execution]
        // Set the type of the return value to bool
        public static bool __Process(
             // Add the class name (function name) as single output parameter without the "[Output]" attribute.
            ref short FU_with_ENENO,
            [Input, DataType("WORD")] ushort IN1,
            [Input, DataType("WORD")] ushort IN2)
        {
            // Make sure the Output value is well defined
            FU_with_ENENO = 0;

            // TODO: Write the logic of the function. 
            // This sets the output to the value even if the result isn't valid. Eno will mark it as invalid if the check below failes.
            // This is a typical use case. If this is not wanted, put the allocation into the "if". 
            FU_with_ENENO = (short) (IN1 + IN2);
            // and implement the condition
            // for changing ENO to false e.g.

            if (IN1 < IN2)
                return false;

            // Return ENO
            return true;
        }
    }

    /// <summary>
    /// Function without ENO and bool as function result
    /// </summary>
    [Function]
    public static class FU_wo_ENENO_bool
    {
        [Execution]
        // Set the type of the return value to bool and not additional output
        public static bool __Process(
            [Input, DataType("WORD")] ushort IN1,
            [Input, DataType("WORD")] ushort IN2)
        {

            // TODO: Write the logic of the function for the return type of bool
            if (IN1 < IN2)
                return false;
            return true;
        }
    }
}
