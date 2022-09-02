#region Copyright

//
// Copyright (c) Phoenix Contact GmbH & Co. KG. All rights reserved.
// Licensed under the MIT. See LICENSE file in the project root for full license information.
//

#endregion Copyright

using Iec61131.Engineering.Prototypes.Methods;
using Iec61131.Engineering.Prototypes.Types;
using Iec61131.Engineering.Prototypes.Variables;
using Iec61131.Engineering.Prototypes.Common;

namespace ExampleLib
{
    // The attribute "Structure" is necessary to make the struct visible in the PCWorx Engineer
    [Structure]
    public struct Position
    {
        // the fields must be public as well as the struct itself
        [DataType("DINT")]
        public int x;
        [DataType("DINT")]
        public int y;
    }

    // Pass 'Input' and 'Output' parameter by value.
    [FunctionBlock]
    public class FB_with_user_struct1
    {
        [Input]
        public Position NEW_POSITION;

        [Output]
        public Position CURRENT_POSITION;

        [Initialization]
        public void __Init()
        {
        }

        [Execution]
        public void __Process()
        {
            if (CURRENT_POSITION.x < NEW_POSITION.x)
            {
                CURRENT_POSITION.x++;
            }
            else if (CURRENT_POSITION.x > NEW_POSITION.x)
            {
                CURRENT_POSITION.x--;
            }
            if (CURRENT_POSITION.y < NEW_POSITION.y)
            {
                CURRENT_POSITION.y++;
            }
            else if (CURRENT_POSITION.y > NEW_POSITION.y)
            {
                CURRENT_POSITION.y--;
            }
        }
    }

    // Pass parameters by reference as an 'InOut' parameter. This saves memory and CPU time for copying values for large arrays and structures.
    [FunctionBlock]
    public class FB_with_user_struct2
    {
        [InOut]
        unsafe public Position* NEW_POSITION;

        [InOut]
        unsafe public Position* CURRENT_POSITION;

        [Initialization]
        public void __Init()
        {
        }

        [Execution]
        unsafe public void __Process()
        {
            if ((*CURRENT_POSITION).x < (*NEW_POSITION).x)
            {
                (*CURRENT_POSITION).x++;
            }
            else if ((*CURRENT_POSITION).x > (*NEW_POSITION).x)
            {
                (*CURRENT_POSITION).x--;
            }
            if ((*CURRENT_POSITION).y < (*NEW_POSITION).y)
            {
                (*CURRENT_POSITION).y++;
            }
            else if ((*CURRENT_POSITION).y > (*NEW_POSITION).y)
            {
                (*CURRENT_POSITION).y--;
            }
        }
    }
}
