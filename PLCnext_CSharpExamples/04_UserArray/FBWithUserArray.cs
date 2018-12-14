#region Copyright
//  
// Copyright (c) Phoenix Contact GmbH & Co. KG. All rights reserved.  
// Licensed under the MIT. See LICENSE file in the project root for full license information.  
//  
#endregion

using System;
using System.Iec61131Lib;
using Eclr;
using Eclr.Pcos;
using System.Runtime.InteropServices;
using Iec61131.Engineering.Prototypes.Types;
using Iec61131.Engineering.Prototypes.Variables;
using Iec61131.Engineering.Prototypes.Methods;
using Iec61131.Engineering.Prototypes.Common;

namespace ExampleLib
{
    // The IecArray must be defined as a struct with a fixed size.
    // The array definition MUST have following Attributes:
    // 1. Array (Actually only one-dimensional arrays are supported by the PCWorx Engineer)
    // 2. ArrayDimension
    // 3. DataType to define the data type of the array elements
    [Array(1), ArrayDimension(0, ArrayProperties.lowerBound, ArrayProperties.upperBound), DataType("DINT")]
    [StructLayout(LayoutKind.Explicit, Size = ArrayProperties.size)]
    public struct IntArray
    {
        // Helper containing constants to have a 
        // clear and maintainable definition for boundaries and size
        struct ArrayProperties
        {
            public const int lowerBound = 0;
            public const int upperBound = 9;
            public const int size = (upperBound - lowerBound + 1) * sizeof(int);
        }

        // Fields
        // The field "Anchor" defines the beginning of the array.
        [FieldOffset(0)]
        // The Anchor's data type is the child data type of the array
        public int Anchor;
        // The constants LB and UB define the upper and lower bound. Boundaries will be checked by using them.
        public const int LB = ArrayProperties.lowerBound;
        public const int UB = ArrayProperties.upperBound;
        public int this[int index]
        {
            get
            {
                if (index >= LB && index <= UB)
                {
                    unsafe
                    {
                        fixed (int* pValue = &Anchor)
                        {
                            int result = *(pValue + index);
                            return result;
                        }
                    }
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
            set
            {
                if (index >= LB && index <= UB)
                {
                    unsafe
                    {
                        fixed (int* pValue = &Anchor)
                        {
                            *(pValue + index) = value;
                        }
                    }
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }
    }

    [FunctionBlock]
    public class FB_with_user_array
    {
        [Input]
        public IntArray iIN;
        [Output]
        public int iMIN_VALUE;
        [Output]
        public int iMAX_VALUE;
        [Output]
        public int iAVERAGE;
        [Output]
        public bool xIS_DATA_CHANGED;

        private IntArray Backup;

        [Initialization]
        public void __Init()
        {
        }

        [Execution]
        public unsafe void __Process()
        {
            iMIN_VALUE = int.MaxValue;
            iMAX_VALUE = 0;
            int total = 0;

            // This kind of accessing the array works fine with all types of arrays but it is really cumbersome
            // Nevertheless it can also be used for arrays of e.g. IecStrings.
            fixed (int* data = &iIN.Anchor)
            {
                for (int i = 0; i < (IntArray.UB - IntArray.LB + 1); i++)
                {
                    int currentValue = data[i];
                    total += currentValue;
                    iMIN_VALUE = Math.Min(iMIN_VALUE, currentValue);
                    iMAX_VALUE = Math.Max(iMAX_VALUE, currentValue);
                }
            }

            // For arrays of elementary types the index operator can be used effectively.
            xIS_DATA_CHANGED = false;
            for (int i = IntArray.LB; i <= IntArray.UB; i++)
            {
                if(Backup[i] != iIN[i])
                {
                    xIS_DATA_CHANGED = true;
                }
            }
            Backup = iIN;

            iAVERAGE = total / (IntArray.UB - IntArray.LB + 1);
        }
    }
}
