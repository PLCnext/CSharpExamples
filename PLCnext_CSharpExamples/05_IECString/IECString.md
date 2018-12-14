# IEC String
([FBWithString.cs](FBWithString.cs))

The user-defined IECString allows the implementation of different sized strings, besides the fixed-length IECString80.
The attribute `String()` defines the size of the string, and `StructLayout()` must include the size of the actual string + 4 byte header + 1 byte terminating zero + padding for two byte alignment.
The struct itself contains a field of the type `IecStringEx` with a `FieldOffset()` attribute.
The constructor (`public void ctor()`) sets the maximum length of the string. Each string ctor() must be called during initialization, otherwise the string will have a length of 0.
`rctor()` is the retain constructor and is currently not used in PLCnext.
