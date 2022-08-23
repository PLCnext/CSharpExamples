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
    // The attribute "Enumeration" is necessary to make the enum visible in PLCnext Engineer
    [Enumeration]
    public enum MyEnumeration : int
    {
        first,
        second,
        third = 8,
        forth = 9,
        defaultvalue = 9999
    }

    [FunctionBlock]
    public class USER_ENUMERATION
    {
        [Input]
        [DataType("DINT")]
        public MyEnumeration WHICH_ENUM;

        [Output]
        [DataType("DINT")]
        public MyEnumeration RETURN_ENUM;

        [Initialization]
        public void __Init()
        {
        }

        [Execution]
        public void __Process()
        {
            switch (WHICH_ENUM)
            {
                case MyEnumeration.first:
                    RETURN_ENUM = MyEnumeration.first;
                    break;

                case MyEnumeration.second:
                    RETURN_ENUM = MyEnumeration.second;
                    break;

                case MyEnumeration.third:
                    RETURN_ENUM = MyEnumeration.third;
                    break;

                case MyEnumeration.forth:
                    RETURN_ENUM = MyEnumeration.forth;
                    break;

                default:
                    RETURN_ENUM = MyEnumeration.defaultvalue;
                    break;
            }
        }
    }
}
