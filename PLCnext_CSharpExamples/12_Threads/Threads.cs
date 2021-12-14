#region Copyright

//
// Copyright (c) Phoenix Contact GmbH & Co. KG. All rights reserved.
// Licensed under the MIT. See LICENSE file in the project root for full license information.
//

#endregion Copyright
using Iec61131.Engineering.Prototypes.Methods;
using Iec61131.Engineering.Prototypes.Types;
using Iec61131.Engineering.Prototypes.Variables;
using System.Threading;

namespace ExampleLib
{
    [FunctionBlock]
    public class Threads
    {
        [InOut]
        public bool startThread;

        [Input]
        public short setPrio;

        [Initialization]
        public void __Init()
        {
            //
            // TODO: Initialize the variables of the function block here
            //
        }

        [Execution]
        public void __Process()
        {
            if (startThread)
            {
                // Create the thread object, passing in the
                // serverObject.StaticMethod method using a
                // ThreadStart delegate.
                Thread StaticCaller = new Thread(
                new ThreadStart(ServerClass.ThreadBody));

                switch (setPrio)
                {
                    case 100:
                        StaticCaller.Priority = ThreadPriority.Lowest;
                        StaticCaller.Name = "cs_lowest";
                        break;

                    case 101:
                        StaticCaller.Priority = ThreadPriority.BelowNormal;
                        StaticCaller.Name = "cs_bnormal";
                        break;

                    case 102:
                        StaticCaller.Priority = ThreadPriority.Normal;
                        StaticCaller.Name = "cs_normal";
                        break;

                    case 103:
                        StaticCaller.Priority = ThreadPriority.AboveNormal;
                        StaticCaller.Name = "cs_anormal";
                        break;

                    case 104:
                        StaticCaller.Priority = ThreadPriority.Highest;
                        StaticCaller.Name = "cs_higest";
                        break;

                    default:
                        // It is possible to cast integers from 0-99 as
                        // ThreadPriority. Like in C++ its not recommended
                        // to run threads in higher priority.
                        StaticCaller.Priority = (ThreadPriority)setPrio;
                        StaticCaller.Name = "cs_prio" + setPrio;
                        break;
                }

                StaticCaller.Start();
                startThread = false;
            }
        }
    }

    public class ServerClass
    {
        public static void ThreadBody()
        {
            int a = 0;
            int b = 1;
            int n = 300000;
            // In N steps compute Fibonacci sequence iteratively.
            for (int i = 0; i < n; i++)
            {
                int temp = a;
                a = b;
                b = temp + b;
            }

            // Pause for a moment to provide a delay to make
            // threads more apparent.
            Thread.Sleep(60000);
        }
    }
}