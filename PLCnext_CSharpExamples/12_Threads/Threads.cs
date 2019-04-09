using System;
using System.Iec61131Lib;
using Iec61131.Engineering.Prototypes.Types;
using Iec61131.Engineering.Prototypes.Variables;
using Iec61131.Engineering.Prototypes.Methods;
using Iec61131.Engineering.Prototypes.Common;
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
                new ThreadStart(ServerClass.StaticMethod));

                switch (setPrio)
                {
                    case 0:
                        StaticCaller.Priority = ThreadPriority.Lowest;
                        break;
                    case 1:
                        StaticCaller.Priority = ThreadPriority.BelowNormal;
                        break;
                    case 2:
                        StaticCaller.Priority = ThreadPriority.Normal;
                        break;
                    case 3:
                        StaticCaller.Priority = ThreadPriority.AboveNormal;
                        break;
                    case 4:
                        StaticCaller.Priority = ThreadPriority.Highest;
                        break;
                    default:
                        StaticCaller.Priority = ThreadPriority.Normal;
                        break;
                }

                StaticCaller.Start();
                startThread = false;
            }

        }
    }

    public class ServerClass
    {
        public static void StaticMethod()
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