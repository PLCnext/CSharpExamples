# User Array
([FBWithUserArray.cs](FBWithUserArray.cs))

To program a user defined IECArray, it is recommended to use the complete sample struct - provided here - as a template.
The sample function block shows how to use these arrays in C#, especially with IECStrings.

The implementation of an IECArray needs the PLCnext specific attributes, `Array()` `ArrayDimension()` and `DataType()`.
Furthermore the `StructLayout()` with the "Anchor" field as `FieldOffset()` provides a full implementation, including the ability to use the index operator.