#region Copyright

//
// Copyright (c) Phoenix Contact GmbH & Co. KG. All rights reserved.
// Licensed under the MIT. See LICENSE file in the project root for full license information.
//

#endregion Copyright

using Iec61131.Engineering.Prototypes.Common;
using Iec61131.Engineering.Prototypes.Methods;
using Iec61131.Engineering.Prototypes.Pragmas;
using Iec61131.Engineering.Prototypes.Types;
using Iec61131.Engineering.Prototypes.Variables;
using System;
using System.Iec61131Lib;

namespace PLCnextFirmwareLibrarySample
{
    // The attribute "Enumeration" is necessary to make the enum visible in PLCnext Engineer
    [Enumeration]
    public enum DaysOfWeek
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }

    [Function]
    public static class IsWeekendFunction
    {
        [Execution]
        public static bool __Process(
            [Input] DaysOfWeek IN1)
        {
            bool IsWeekendFunction = IN1 == DaysOfWeek.Saturday || IN1 == DaysOfWeek.Sunday;

            return IsWeekendFunction;
        }
    }
}

