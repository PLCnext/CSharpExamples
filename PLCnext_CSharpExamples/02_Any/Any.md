# Any In & Output  
([FunWithAny.cs](FunWithAny.cs), [FB1WithAny.cs](FB1WithAny.cs), [FB2WithAny.cs](FB2WithAny.cs))

FunWithAny.cs is an example how to use the IEC 61131-3 `ANY` parameter in a C# function.  
First we have the partly optional attribute `DataType()`.
This attribute is only necessary if you have an IEC type as in/output,
which does not have a unique mapping to a C# data type.
For example, `uint` in C# is `UDINT` in IEC.
But if you want an input to be of data type  `DWORD`, which is `uint` in C#,
the `DataType("DWORD")` attribute is needed.
For `ANY` parameters, the `DataType()` attribute must always be used.
For more information on data type mapping, read the [Readme.txt](../Readme.txt),
available in every Visual Studio eCLR project.
```cs
[Function, DataType("ANY_NUM")]
public static class Fun_with_ANY
{
```

In Functions all `ANY` parameters must be passed by reference.
```cs
    [Execution]
    public unsafe static void __Process(
        [Output] ref Any Fun_with_ANY,
        [Input, DataType("ANY_NUM")] ref Any VALUE,
        [Input, DataType("ANY_NUM")] ref Any MIN,
        [Input, DataType("ANY_NUM")] ref Any MAX)
    {
    ...
```
Use the runtime type handle to identify the element type.

```cs
code = (Eclr.TypeCode)Eclr.TypeInfo.GetTypeCode(VALUE.pRuntimeTypeHandle);
```

FB1WithAny.cs is an example of using `ANY` in a function block. There, you don't need to reference your fields. 
But `ANY` as an `Output` parameter is not supported. For this, the FB2WithAny.cs shows how to use an `InOut` parameter instead.
