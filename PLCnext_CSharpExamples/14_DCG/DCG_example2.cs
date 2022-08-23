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
// In this pattern an instance of an implementation class is added to the container and will not be removed till the finalization of the FB instance.
// It is important that the FB is not added directly to the container.
namespace DcgBestPracticePattern3
{
    [FunctionBlock]
    public class BackgroundDemoFb1
    {
        [Input, DataType("BOOL")]
        public bool In;
        [Output, DataType("DINT")]
        public int Value;

        // Do not permanently put a Function Block into a Container. 
        // This will cause a Memory leak and may cause resource leaks.
        // Better lazily (when used by an active domain) create an impl class that is added to the container and will execute all logic.
        private BackgroundFbImpl impl;

        public BackgroundDemoFb1()
        {
            // Create a worker thread during construction
            // Worker thread is designed to create just one thread per AppDomain although called more than once.
            WorkerThread.CreateThread();
        }
        ~BackgroundDemoFb1()
        {
            if (Eclr.Environment.IsPrimaryDomain(this))
            {
                // The finalizer is typically executed in primary domain. This is done to access object references from this 
                // domain in order to do clean ups.
                if (impl != null)
                {
                    Container.GetInstance().Remove(impl);
                }
            }
            else
            {
                // When the type has been changed by adding, removing or changing fields by Download Changes
                // then the Finalizer is executed in the secondary domain.
                // In this case it is not possible to access object references from the primary domain.
            }
        }

        [Initialization]
        public void __Init()
        {
        }

        [Execution]
        public void __Process()
        {
            // Lazy initialization of container
            if (impl == null)
            {
                impl = new BackgroundFbImpl();
                Container.GetInstance().Add(impl);
            }
            impl.Process(this);
        }
    }

    public class BackgroundFbImpl
    {
        private int tempResult;

        public BackgroundFbImpl()
        {
        }

        ~BackgroundFbImpl()
        {
        }

        public void Process(BackgroundDemoFb1 fb)
        {
            // Accessing public data of the FB
            if (fb.In) 
            {
                fb.Value = tempResult;
            }
        }

        public void Add1()
        {
            tempResult++;
        }
        public void Add10()
        {
            tempResult += 10;
        }
    }

    public class Container
    {
        // Implementation of singleton pattern
        private static Container instance;
        internal static Container GetInstance()
        {
            if (instance == null)
            {
                instance = new Container();
            }
            return instance;
        }

        private List<BackgroundFbImpl> fbs;
        private Container()
        {
            fbs = new List<BackgroundFbImpl>();
        }
        ~Container()
        {
            if (fbs != null)
            {
            }
        }
        internal void Add(BackgroundFbImpl o)
        {
            lock (this)
            {
                fbs.Add(o);
            }
        }
        internal void Remove(BackgroundFbImpl o)
        {
            lock (this)
            {
                fbs.Remove(o);
            }
        }
        internal BackgroundFbImpl[] GetAll()
        {
            lock (this)
            {
                BackgroundFbImpl[] allFbs = allFbs = new BackgroundFbImpl[fbs.Count];
                fbs.CopyTo(allFbs);
                return allFbs;
            }
        }
    }

    #region Worker Thread
    /// <summary>
    /// Class creating a thread (that must be static) 
    /// </summary>
    static class WorkerThread
    {
        private static Thread worker;
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

        static void TaskBody()
        {
            while (true)
            {
                // Just execute jobs if the PLC state is Running
                if (Resource.Running)
                {
                    Exec();
                }
                Thread.Sleep(1);
            }
        }

        private static void Exec()
        {
            // This example of Exec() is a very simple implementation. It gets all registered instances from the container and executes a method of it.
            // Following can be optimized: 
            // - The array produces garbage in each cycle. 
            // - If the container may potentially contain a large list of FBs then process just one or some instead of all instances per cycle. 
            //   This is more cooperative and allows lower prior tasks to get CPU time.
            BackgroundFbImpl[] fbs = Container.GetInstance().GetAll();
            foreach (BackgroundFbImpl fb in fbs)
            {
                fb.Add1();
            }
        }
    }
    #endregion

}
