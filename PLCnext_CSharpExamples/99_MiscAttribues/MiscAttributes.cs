using Iec61131.Engineering.Prototypes.Common;
using Iec61131.Engineering.Prototypes.Methods;
using Iec61131.Engineering.Prototypes.Types;
using Iec61131.Engineering.Prototypes.Variables;

namespace ExampleLib
{   // Structure with explicit IEC data type definition by the attribute [DataType]:
    // Use the DataType Attribute if the data type is not clear.
    // Find more information in the Readme.txt in the Visual Studio Project.
    [Structure]
    public struct BytePair
    {
        [DataType("BYTE")]
        public byte LoByte;

        [DataType("BYTE")]
        public byte HiByte;
    }

    [FunctionBlock]
    public class MiscExamples
    {
        // Fields that are marked with the [OPC] attribute are made visible by the OPC server when activated.
        // Allowed are Input and Output parameter, but not InOut parameter.
        [Input, OPC]
        public short IN1;

        [Input]
        public short IN2;

        [Output, DataType("WORD"), OPC]
        public ushort OUT;

        // Fields of a complex data type can be marked with the OPC attribute, too.
        [Output, OPC]
        public BytePair BYTES;

        // Also private data can be made visible to the OPC, see the "Sum" field.
        [OPC]
        private short Sum;

        [Initialization]
        public void __Init()
        {
        }

        [Execution]
        public void __Process()
        {
            Sum = (short)(IN1 + IN2);
            OUT = (ushort)(Sum);
            BYTES.LoByte = (byte)(OUT & 0xFF);
            BYTES.HiByte = (byte)((OUT >> 8) & 0xFF);
        }
    }
}
