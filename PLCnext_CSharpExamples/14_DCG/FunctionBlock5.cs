#region Copyright

//
// Copyright (c) Phoenix Contact GmbH & Co. KG. All rights reserved.
// Licensed under the MIT. See LICENSE file in the project root for full license information.
//

#endregion

using System;
using System.Iec61131Lib;
using System.Collections.Generic;
using Eclr;
using System.Threading;
using Eclr.Pcos;

using Iec61131.Engineering.Prototypes.Common;
using Iec61131.Engineering.Prototypes.Methods;
using Iec61131.Engineering.Prototypes.Types;
using Iec61131.Engineering.Prototypes.Variables;

// This pattern represents the use case that code of a FB should be executed in a background task.
// The background task executes all FBs that exist in a container.
// Compared to the example of DcgBestPracticePattern3 in this pattern the FB remains in the container until its job is done. Then it is removed from it.
// The advantage of this solution is that no implementation layer of the function block is needed (as shown in DcgBestPracticePattern3).
namespace DcgBestPracticePattern5
{
    [FunctionBlock]
    public class BackgroundDemoFb3 : BackgroundFb
    {
        [Input, DataType("BOOL")]
        public bool Execute;
        [Output, DataType("DINT")]
        public int Value;

        private DateTime dueTime;

        #region Rising edge detection
        private bool lastExecute;
        private bool IsExecute
        {
            get
            {
                if (Execute && !lastExecute)
                {
                    lastExecute = Execute;
                    return true;
                }
                lastExecute = Execute;
                return false;
            }
        }
        #endregion

        public BackgroundDemoFb3()
        {
        }
        ~BackgroundDemoFb3()
        {
        }

        [Initialization]
        public void __Init()
        {
        }

        [Execution]
        public void __Process()
        {
            if(IsExecute && (dueTime < DateTime.Now))
            {
                dueTime = DateTime.Now.AddSeconds(1);   // Do a job for 1 second
                WorkerThread.AddJob(this);
            }
        }
        public override void Exec()
        {
            Value++;
            if (DateTime.Now < dueTime)
            {
                // If the FB has still something to do add a new job to the worker thread
                WorkerThread.AddJob(this);
            }
        }

    }

    // Base class for background FBs
    public class BackgroundFb
    {
        internal BackgroundFb next;
        internal BackgroundFb()
        {
            // Worker thread is designed to create just one thread per AppDomain although called more than once.
            WorkerThread.CreateThread();
        }
        public virtual void Exec() { }
    }

#region Worker Thread
    /// <summary>
    /// Class creating a thread (that must be static) 
    /// </summary>
    class WorkerThread
    {
        private static Thread worker;
        private static BackgroundFb top;

        internal static bool AddJob(BackgroundFb fb)
        {
            bool result = false;
            if (Resource.Running)
            {
                if (fb != null)
                {
                    fb.Exec();
                    result = true;
                }
            }
            else
            {
                lock (worker)
                {
                    if (top == null)
                    {
                        top = fb;
                    }
                    else
                    {
                        bool bBreak = false;
                        BackgroundFb iter = top;
                        while (iter.next != null)
                        {
                            if (iter.next == fb)
                            {
                                // an object is allowed to exist only once in the queue
                                bBreak = true;
                                break;
                            }
                            iter = iter.next;
                        }
                        if (!bBreak)
                        {
                            if (iter != fb)    // avoid recursion in chain (important if top.next == null)
                            {
                                iter.next = fb;
                                result = true;
                            }
                        }
                    }
                }
            }
            return result;
        }

        internal static bool CreateThread()
        {
            if (worker == null)
            {
                ThreadStart start = new ThreadStart(TaskBody);
                worker = new Thread(start);
                worker.Priority = ThreadPriority.Lowest;
                worker.Start();
                return true;
            }
            return false;
        }

        private static void TaskBody()
        {
            while (true)
            {
                // Just execute jobs if the PLC state is Running
                if (Resource.Running)
                {
                    Exec();
                }
                Thread.Sleep(10);
            }
        }

        private static void Exec()
        {
            if (top != null)
            {
                // determine amount of jobs
                BackgroundFb next = top.next;
                int amount = 1;
                while (next != null)
                {
                    amount++;
                    next = next.next;
                }
                // execute this amount of jobs
                // if jobs are added during execution of this jobs then
                // these jobs are executed in a next cycle
                while (top != null && amount != 0)
                {
                    BackgroundFb current = top;
                    lock (worker)
                    {
                        BackgroundFb temp = top;    // remember top object to set top.next to null
                        top = top.next;             // next job
                        temp.next = null;
                    }
                    amount--;
                    current.Exec();                     // execute job
                }
            }
        }
    }
#endregion

}
