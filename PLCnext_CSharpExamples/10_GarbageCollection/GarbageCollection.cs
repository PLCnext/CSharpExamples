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
    public class SET_GC_EXECUTION
    {
        [Input]
        public bool EXECUTE;
        [Input]
        public bool IMPLICITE;
        [Output]
        public bool IS_IMPLICITE;

        private bool ExecutePreviousState;  // used for rising edge detection

        [Initialization]
        public void __Init()
        {
        }

        [Execution]
        public void __Process()
        {
            if (EXECUTE && !ExecutePreviousState)
            {
                if (IMPLICITE)
                {
                    // Set GC to implicit collection 
                    if (!Eclr.Environment.IsGcImplicit())
                    {
                        // Set collecting with a threshold of 100000 which means that after allocating 100000 new byte from the heap the collector is started
                        Eclr.Environment.SetGcImplicit(100000);
                    }
                }
                else
                {
                    // Set GC to explicit collection
                    if (Eclr.Environment.IsGcImplicit())
                    {
                        Eclr.Environment.SetGcExplicit();
                    }
                }
            }
            IS_IMPLICITE = Eclr.Environment.IsGcImplicit();
            ExecutePreviousState = EXECUTE;
        }
    }
}
