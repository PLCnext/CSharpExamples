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
    [FunctionBlock]
    public class FB_with_methods
    {

        //reuse of the struct in FBWithUserStruct.cs
        Position CurrentPosition;

        [Initialization]
        public void __Init()
        {
        }

        [Execution]
        public void __Process()
        {
        }
        //the attribute "User" indicates a method for the PCWorx Engineer
        //and make it available for the user in IEC code
        [User]
        public void SetX(int x)
        {
            CurrentPosition.x = x;
        }
        [User]
        public void SetY(int y)
        {
            CurrentPosition.y = y;
        }
        [User]
        public int GetX()
        {
            return CurrentPosition.x;
        }
        [User]
        public int GetY()
        {
            return CurrentPosition.y;
        }
        //Complex data types are returned as referenced parameter
        [User]
        public void GetPosition(ref Position actualPosition)
        {
            actualPosition = CurrentPosition;
        }

        // NOT SUPPORTED: Complex data types as return value
        [User]
        public Position NotSupported_GetPositionReturningTheStructure()
        {
            return CurrentPosition;
        }
    }
}
