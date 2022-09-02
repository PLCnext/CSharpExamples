#region Copyright

//
// Copyright (c) Phoenix Contact GmbH & Co. KG. All rights reserved.
// Licensed under the MIT. See LICENSE file in the project root for full license information.
//

#endregion


using System.Iec61131Lib;
using System.Collections.Generic;
using Iec61131.Engineering.Prototypes.Common;
using Iec61131.Engineering.Prototypes.Methods;
using Iec61131.Engineering.Prototypes.Types;
using Iec61131.Engineering.Prototypes.Variables;


// This pattern represents the use case that a function block's method is executed by a another FB. 
// Typically the first FB is put into a container and is accessed by a FB handle. For Download Changes support
// the FB must not be put into a container directly. This leads to a reference that cannot be removed during 
// Download Change. The garbage collector will not finalize the instance.
namespace DcgBestPracticePattern2
{
    [FunctionBlock]
    public class SlaveFB2
    {
        [Input, DataType("BOOL")]
        public bool In;
        [Input, DataType("DINT")]
        public int Handle;
        [Output, DataType("DINT")]
        public int Value;

        // Do not permanently put a Function Block into a Container. 
        // This will cause a Memory leak and may cause resource leaks.
        // Better create an impl class that is lazily (only when used by the active domain) added to the container and will execute all logic.
        private SlaveFbImpl impl;

        public SlaveFB2()
        {
            impl = new SlaveFbImpl();
        }
        ~SlaveFB2()
        {
            if (Eclr.Environment.IsPrimaryDomain(this))
            {
                // The finalizer is typically executed in primary domain. This is done to access object references from this 
                // domain in order to do clean ups.
                if (Handle != Container.InvalidHandle)
                {
                    Container.GetInstance().Remove(Handle);
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
            if (Handle == Container.InvalidHandle)
            {
                Handle = Container.GetInstance().Add(impl);
            }
            impl.Process(this);
        }
    }

    public class SlaveFbImpl
    {
        private int tempResult;

        public SlaveFbImpl()
        {
        }

        ~SlaveFbImpl()
        {
        }

        public void Process(SlaveFB2 fb)
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

        private Dictionary<int, SlaveFbImpl> fbs;
        private int nextHandle = 1;
        public const int InvalidHandle = 0;
        private Container()
        {
            fbs = new Dictionary<int, SlaveFbImpl>();
        }
        ~Container()
        {
            if (fbs != null)
            {
            }
        }
        internal int Add(SlaveFbImpl o)
        {
            int handle = Container.InvalidHandle;
            lock (this)
            {
                handle = nextHandle;
                fbs.Add(handle, o);
                nextHandle++;
            }
            return handle;
        }
        internal bool Remove(int handle)
        {
            int fbHash = 0;
            if (fbs.TryGetValue(handle, out SlaveFbImpl fb))
            {
                fbHash = fb.GetHashCode();
            }
            lock (this)
            {
                fbs.Remove(handle);
            }
            if (fbHash != 0)
            {
                return false;
            }
            return true;
        }
        internal SlaveFbImpl Get(int handle)
        {
            SlaveFbImpl fb;
            lock (this)
            {
                fbs.TryGetValue(handle, out fb);
            }
            return fb;
        }
    }

    [FunctionBlock]
    public class MasterFB2
    {
        [Input, DataType("BOOL")]
        public bool In;
        [Input, DataType("DINT")]
        public int Handle;
        [Input, DataType("DINT")]
        public int Job;
        [Output, DataType("BOOL")]
        public bool Done;

        public MasterFB2()
        {
        }
        [Initialization]
        public void __Init()
        {
        }
        [Execution]
        public void __Process()
        {
            Done = false;
            if(In)
            {
                SlaveFbImpl fb = Container.GetInstance().Get(Handle);
                if (fb != null)
                {
                    switch (Job)
                    {
                        case 0:
                            fb.Add1();
                            Done = true;
                            break;
                        case 1:
                            fb.Add10();
                            Done = true;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
