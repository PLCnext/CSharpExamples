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
using System;
using System.Threading;
using System.Iec61131Lib;

namespace ExampleLib
{
    [FunctionBlock]
    public class BackgroundThread
    {
        [InOut]
        public bool startThread;

        [Input]
        public short setPrio;

        [Output]
        public bool done;

        public Thread StaticCaller;
        private bool m_startThread;

        [Initialization]
        public void __Init()
        {
            // Create the thread object, passing in the
            // serverObject.ThreadBody method using a
            // ThreadStart delegate.
            ThreadStart threadStarter = new ThreadStart(BackgroundServerThread.ThreadBody);
            StaticCaller = new Thread(threadStarter);
            StaticCaller.Priority = ThreadPriority.Lowest;
            StaticCaller.Name = "backgroundThread";

            StaticCaller.Start();
        }

        ~BackgroundThread()
        {
            BackgroundServerThread.TerminateBackgroundThread = true;
        }

        [Execution]
        public void __Process()
        {
            if (BackgroundHelper.IsRisingEdge(startThread, ref m_startThread))
            {
                done = false;
                BackgroundServerThread.doSomething = true;
            }
            done = BackgroundServerThread.done;

            if (BackgroundHelper.IsFallingEdge(startThread, ref m_startThread))
            {
                BackgroundServerThread.doSomething = false;
                done = false;
                startThread = false;
            }
        }
    }
    public class BackgroundServerThread
    {
        public static bool doSomething;
        public static bool done;
        public static bool TerminateBackgroundThread;
        public static void ThreadBody()
        {
            while (true)         // run so long as initiator exists.
            {
                if(TerminateBackgroundThread == true)
                {
                    break;       // terminates this thread if initiator is removed
                }
                done = false;
                if (BackgroundHelper.IsRisingEdge(doSomething, ref doSomething))
                {
                    int a = 0;
                    int b = 1;
                    int n = 300000;
                    // If something is to do we do this. 
                    // Example: In N steps compute Fibonacci sequence iteratively .
                    for (int i = 0; i < n; i++)
                    {
                        int temp = a;
                        a = b;
                        b = temp + b;
                    }
                    done = true; // Initiator of this thread looks on this bit to detect the job has been done.
                }
                else
                {
                    // Pause for a moment to provide a delay to make
                    // threads more apparent.
                    Thread.Sleep(100);
                }
            }
        }
    }
    public static class BackgroundHelper
    {
        public static bool IsRisingEdge(bool value, ref bool _value)
        {
            if (value && !_value)
            {
                _value = value;
                return true;
            }
            return false;
        }
        public static bool IsFallingEdge(bool value, ref bool _value)
        {
            if (!value && _value)
            {
                _value = value;
                return true;
            }
            return false;
        }
    }
}
