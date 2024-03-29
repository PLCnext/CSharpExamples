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
    public class Counter_ENENO
    {
        // define EN as first input parameter
        [Input]
        public bool EN;

        [Input]
        public bool xDOWN;

        // define ENO as first output parameter
        [Output]
        public bool ENO;

        [Output, DataType("DINT")]
        public int iOUT;

        [Initialization]
        public void __Init()
        {
        }

        [Execution]
        public void __Process()
        {
            // EN/ENO handling must be implemented by the developer
            ENO = EN;
            if (ENO == false)
            {
                SetOutputValuesToDefault();
                return;
            }

            if (xDOWN)
            {
                iOUT--;
            }
            else
            {
                iOUT++;
            }
            // going into error state can be defined by the developer by setting ENO to false
            if (iOUT < 0 || iOUT > 1000)
            {
                ENO = false;
            }
        }

        // Outputs must be well defined on error return
        private void SetOutputValuesToDefault()
        {
            iOUT = 0;
        }
    }
}
