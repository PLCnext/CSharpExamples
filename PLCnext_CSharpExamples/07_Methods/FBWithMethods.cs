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
    [FunctionBlock]
    public class FB_with_methods
    {
        //reuse of the struct in FBWithUserStruct.cs
        private Position CurrentPosition;

        [Initialization]
        public void __Init()
        {
        }

        [Execution]
        public void __Process()
        {
        }

        //the attribute "User" indicates a method for the PLCnext Engineer
        //and make it available for the user in IEC code
        [User]
        public void SetX([Input, DataType("DINT")] int x)
        {
            CurrentPosition.x = x;
        }

        [User]
        public void SetY([Input, DataType("DINT")] int y)
        {
            CurrentPosition.y = y;
        }

        [User, DataType("DINT")]
        public int GetX()
        {
            return CurrentPosition.x;
        }

        [User, DataType("DINT")]
        public int GetY()
        {
            return CurrentPosition.y;
        }

        //Complex data types are returned as referenced parameter
        [User]
        public void GetPosition([Output] ref Position GetPosition)
        {
            GetPosition = CurrentPosition;
        }
    }
}
