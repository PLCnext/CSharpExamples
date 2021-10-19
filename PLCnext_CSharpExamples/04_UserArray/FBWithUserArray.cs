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
    [Array(1), ArrayDimension(0, ArrayProperties.LowerBound, ArrayProperties.UpperBound), DataType("DINT")]
    [StructLayout(LayoutKind.Explicit, Size = ArrayProperties.ByteSize)]
    public struct IntArrayFB
    {
        // Helper containing constants to have a 
        // clear and maintainable definition for boundaries and size
        struct ArrayProperties
        {
            public const int LowerBound = -10;     // must not necessarily being zero, it also can be negative
            public const int UpperBound = 9;       // IEC61131 representation is : userArray : ARRAY[-10..9] OF DINT     (* size == 20 *)
            // the size must be changed to the correct size of your elements times the amount of elements
            public const int ByteSize = (UpperBound - LowerBound + 1) * sizeof(int);
        }
        public const int ByteSize = ArrayProperties.ByteSize;

        // Fields
        // The field "Anchor" defines the beginning of the array.
        [FieldOffset(0)]
        // The Anchor's data type is the child data type of the array
        public int Anchor;
        // The constants LB and UB define the upper and lower bound. Boundaries will be checked by using them.
        public const int LB = ArrayProperties.LowerBound;
        public const int UB = ArrayProperties.UpperBound;
        public int this[int index]
        {
            get
            {
                if (index >= (LB - ArrayProperties.LowerBound) && index <= (UB - ArrayProperties.LowerBound))
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
                if (index >= (LB - ArrayProperties.LowerBound) && index <= (UB - ArrayProperties.LowerBound) )
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
        public IntArrayFB IN_ARRAY;
        [Output]
        public int MIN_VALUE;
        [Output]
        public int MAX_VALUE;
        [Output]
        public int AVERAGE;
        [Output]
        public bool IS_DATA_CHANGED;


        [Initialization]
        public void __Init()
        {
        }

        public const int BackupArraySize = 20;
        public int[] BackupData = new int[BackupArraySize];

        // This function block example demonstrates two jobs:
        // First job is to calculate the average value of the given arrays elements.
        // Second job is to compare the current array content to its content of the previous FB call.
        [Execution]
        public unsafe void __Process()
        {
            // Implementation of first job
            MIN_VALUE = int.MaxValue;
            MAX_VALUE = 0;
            int total = 0;
            fixed (int* data = &IN_ARRAY.Anchor)
            {
                for (int i = 0; i < (IntArrayFB.UB - IntArrayFB.LB + 1); i++)
                {
                    int currentValue = data[i];
                    total += currentValue;
                    MIN_VALUE = Math.Min(MIN_VALUE, currentValue);
                    MAX_VALUE = Math.Max(MAX_VALUE, currentValue);
                }
            }
            AVERAGE = total / (IntArrayFB.UB - IntArrayFB.LB + 1);

            // Implementation of second job
            // For arrays of elementary types the index operator can be used effectively.
            IS_DATA_CHANGED = false;
            // if input array of function block fits locally created backup array
            int elementSize = IntArrayFB.ByteSize / sizeof(int);
            if (BackupArraySize >= elementSize)
            {
                for (int i = 0; i <= (IntArrayFB.UB - IntArrayFB.LB + 1); i++)
                {
                    if (BackupData[i] != IN_ARRAY[i])
                    {
                        IS_DATA_CHANGED = true;
                    }
                    BackupData[i] = IN_ARRAY[i];
                }
            }
            else
            {
                throw new IndexOutOfRangeException(); // Array size mismatch
            }
        }
    }
}
