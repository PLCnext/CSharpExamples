# User Enumeration

([UserEnumeration.cs](UserEnumeration.cs))

First of all the definition of an enum with the `[Enumeration]` attribute added is nessesary to make it available for the PLCnext Engineer.

```cs
[Enumeration]
public enum MyEnumeration : int
{
    first,
    second,
    third = 8,
    forth = 9,
    defaultvalue = 9999
}
```

Now the enum can be used as usual in your program code. For using enums as `[Input]` and `[Output]` variables, you have to set explicit the correct `DataType` of the enum.

```cs
[Input]
[DataType("DINT")]
public MyEnumeration WHICH_ENUM;
```