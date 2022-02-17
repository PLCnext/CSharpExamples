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
using System.IO;

namespace ExampleLib
{
    [FunctionBlock]
    public class WRITE_FILE
    {
        [Input]
        public bool EXECUTE;

        [Input]
        public IecString80 DATA1;

        [Input, DataType("ANY")]
        public Any DATA2;

        [Output]
        public bool DONE;

        private bool ExecutePreviousState;  // used for rising edge detection

        private const string TextFileName = "TextFile.txt";
        private const string BinaryFileName = "BinaryFile.bin";

        [Initialization]
        public void __Init()
        {
            DATA1.ctor();
        }

        [Execution]
        public unsafe void __Process()
        {
            if (EXECUTE && (EXECUTE != ExecutePreviousState))
            {
                try
                {
                    // IecString to .NetString
                    string textData = Utils.BytesToString(DATA1.s.GetIecString(), DATA1.s.currentLength);

                    // byte* to byte[]
                    byte[] binaryData = Utils.BytesToArray((byte*)DATA2.pValue, (int)DATA2.nLength);

                    // check if files already exists
                    if (File.Exists(TextFileName))
                    {
                        // remove file to be ready for this test
                        File.Delete(TextFileName);
                    }
                    if (File.Exists(BinaryFileName))
                    {
                        // remove file to be ready for this test
                        File.Delete(BinaryFileName);
                    }

                    // write data to files
                    File.WriteAllBytes(BinaryFileName, binaryData);
                    File.WriteAllText(TextFileName, textData);

                    DONE = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error writing a file: {0}", ex.ToString());
                }
            }

            if (!EXECUTE)
            {
                DONE = false;
            }

            ExecutePreviousState = EXECUTE;
        }
    }
}
