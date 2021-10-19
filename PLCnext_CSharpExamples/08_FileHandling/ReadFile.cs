#region Copyright
//  
// Copyright (c) Phoenix Contact GmbH & Co. KG. All rights reserved.  
// Licensed under the MIT. See LICENSE file in the project root for full license information.  
//  
#endregion

using System;
using System.Iec61131Lib;
using System.IO;
using Iec61131.Engineering.Prototypes.Types;
using Iec61131.Engineering.Prototypes.Variables;
using Iec61131.Engineering.Prototypes.Methods;
using Iec61131.Engineering.Prototypes.Common;

namespace ExampleLib
{
    [FunctionBlock]
    public class READ_FILE
    {
        [Input]
        public bool EXECUTE;

        [Output]
        public bool DONE;

        [Output]
        public IecString80 DATA1;

        [InOut, DataType("ANY")]
        public Any DATA2;

        private bool ExecutePreviousState;  // used for rising edge detection

        private const string TextFileName = "TextFile.txt";
        private const string BinaryFileName = "BinaryFile.bin";

        [Initialization]
        public void __Init()
        {
            DATA1.ctor();
        }

        [Execution]
        public void __Process()
        {
            if (EXECUTE && (EXECUTE != ExecutePreviousState))
            {
                try
                {
                    // read data from files
                    byte[] binaryData = File.ReadAllBytes(BinaryFileName);
                    string textData = File.ReadAllText(TextFileName);

                    // Put data to parameters
                    // ... for IecString
                    DATA1.s.Init(textData);

                    // ... for binary data
                    Utils.CopyByteArrayToAny(binaryData, ref DATA2);

                    DONE = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error reading a file: {0}", ex.ToString());
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
