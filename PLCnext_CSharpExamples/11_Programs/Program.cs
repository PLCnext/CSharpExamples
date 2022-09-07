using Iec61131.Engineering.Prototypes.Methods;
using Iec61131.Engineering.Prototypes.Ports;
using Iec61131.Engineering.Prototypes.Types;
using Iec61131.Engineering.Prototypes.Variables;
using Iec61131.Engineering.Prototypes.Common;

namespace ExampleLib
{
    [Program]
    public class Program
    {
        // Use the attributes [Global] and either [InputPort] or [OutputPort] to mark fields,
        // that should exchange data with other IEC- or C#-Programs
        [Global, OutputPort, DataType("DINT")]
        public int a = 1;

        [Global, InputPort, DataType("DINT")]
        public int b = 2;

        public int c = 3;

        [Initialization]
        public void __Init()
        {
            //
            // TODO: Initialize the variables of the program here
            //
        }

        [Execution]
        public void __Process()
        {
            a = b + c;
        }
    }
}
