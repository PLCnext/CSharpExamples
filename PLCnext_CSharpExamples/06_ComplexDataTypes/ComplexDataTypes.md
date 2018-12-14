# Complex Data Type
([FUWithComplexDataType.cs](FUWithComplexDataType.cs))

The return type of an IEC Function (FU) is normally determined by the return type of the static `[Execution]` method.
However, this only works for Elementary data types.
Complex data types must be returned by reference, via an `[Output]` parameter in the argument list of the `[Execution]` method.
The function shows an example of how to do this, and a second example that is **not** supported by PLCnext Engineer.