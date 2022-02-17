#region Copyright

//
// Copyright (c) Phoenix Contact GmbH & Co. KG. All rights reserved.
// Licensed under the MIT. See LICENSE file in the project root for full license information.
//

#endregion Copyright

//uncomment the next row to change the signature of the function block (adding a second input)
//#define AddParameter

using Iec61131.Engineering.Prototypes.Methods;
using Iec61131.Engineering.Prototypes.Types;
using Iec61131.Engineering.Prototypes.Variables;

namespace ExampleLib
{
    //the attribute [FunctionBlock] above a class indicates the class to be a function block
    [FunctionBlock]
    //the name of the class equals the name of the function block
    public class Counter
    {
        [Input]
        public bool xActivate;

#if AddParameter
        //adding an additional input
        [Input]
        public bool xDOWN;
#endif

        [Output]
        public int iOUT;

        [Initialization]
        public void __Init()
        {
        }

        [Execution]
        public void __Process()
        {
#if !AddParameter
            //counter counts up
            if (!xActivate)
            {
                iOUT++;
            }
#else
            //counter counts up if input "xDOWN" is false and counts down if "xDOWN" is set to true
            if (xActivate)
            {
                if(xDOWN)
                {
                    iOUT--;
                }
                else
                {
                    iOUT++;
                }
            }
#endif
        }
    }
}
