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
using System.Iec61131Lib;

namespace ExampleLib
{
    [FunctionBlock]
    public class FB_with_DateTime
    {
        [Input]
        public bool EN;

        [Output]
        public long UTC_TICKS;

        [Output]
        public IecString80 LOCAL_TIME;

        [Initialization]
        public void __Init()
        {
            LOCAL_TIME.ctor();
        }

        [Execution]
        public void __Process()
        {
            if (EN)
            {
                // Time stamp
                UTC_TICKS = DateTime.UtcNow.Ticks;

                // ... formatted to string
                DateTime dateTimeNow = DateTime.Now;
                LOCAL_TIME.s.Init(dateTimeNow.ToString(), false);
            }
        }
    }

    [FunctionBlock]
    public class FB_DateTime_conversion
    {
        //uint#0 equals TIME#00:00:00
        [Input, DataType("TIME")]
        public uint Time;

        [Input, DataType("LTIME")]
        public long LTime;

        //long#0 equals LDATE#1970-01-01
        [Input, DataType("LDATE")]
        public long LDate;

        // the short form LTOD and LDT does not work
        [Input, DataType("LTIME_OF_DAY")]
        public long Time_of_Day;

        [Input, DataType("LDATE_AND_TIME")]
        public long Date_and_Time;

        [Output]
        public uint uint_TIME;

        [Output]
        public long lLTIME;

        [Output]
        public long lLDATE;

        [Output]
        public long lLTOD;

        [Output]
        public long lLDT;

        [Output, DataType("TIME")]
        public uint out_TIME;

        [Output, DataType("LTIME")]
        public long out_LTIME;

        [Output, DataType("LDATE")]
        public long out_LDATE;

        [Output, DataType("LTIME_OF_DAY")]
        public long out_LTOD;

        [Output, DataType("LDATE_AND_TIME")]
        public long out_LDT;

        [Output, DataType("LDATE_AND_TIME")]
        public long LDT_now;

        [Initialization]
        public void __Init()
        {
        }

        [Execution]
        public void __Process()
        {
            uint_TIME = out_TIME = Time;
            lLTIME = out_LTIME = LTime;
            lLDATE = out_LDATE = LDate;
            lLTOD = out_LTOD = Time_of_Day;
            lLDT = out_LDT = Date_and_Time;
            TimeSpan test = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0);
            LDT_now = (long)(test.TotalMilliseconds * 1000000.0);
        }
    }
}
