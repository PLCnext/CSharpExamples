# User Array

([FBWithUserArray.cs](FBWithUserArray.cs))

To program a user defined IECArray, it is recommended to use the complete sample struct - provided here - as a template.
The sample function block shows how to use an int array in C#.

The implementation of an IECArray needs the PLCnext specific attributes, `Array()` `ArrayDimension()` and `DataType()`.
Furthermore the `StructLayout()` with the "Anchor" field as `FieldOffset()` provides a full implementation, including the ability to use the index operator.

In some cases of complex datatypes the `sizeof()` keyword does not return the correct size.
Following example shows how to calculate the size of a struct as array elements:

| Variable Name | Datatype | size |
|--- | --- | ---|
| index | int | 4 Byte |
| value | double | 8 Byte |
| txt | IecString80 | 86 Byte ([calculation](../05_IECString/IECString.md)) |

+ 6 Byte padding (8 Byte aligment due to the double data type)
Struct size = 104

The alignment always depends on the biggest variable. The IECString itself is only a 2 Byte aligned struct, therefore in this example the double is the leading element and the structure becomes the 8 Byte alignment.

**The [Execution] method __Process() demonstrates two jobs:**

1. Calculate the average value of the given array's elements.
2. Compare the current array content to its content of the previous FB call.
