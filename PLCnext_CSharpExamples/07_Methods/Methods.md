# Methods

([FBWithMethods.cs](FBWithMethods.cs))

Beside the obligatory methods for initialization and execution, a class can contain optional methods. To make these user methods available in IEC, the attribute `[User]` is used.

For input methods, the method must be marked with the `[Input]` parameter.

```cs
[User]
public void SetX([Input] int x)
{
    CurrentPosition.x = x;
}
```

As in the Complex Data Types example, user methods must also return complex types by reference. The return variable must have the method name and marked with the `[Output]` attribute. Otherwise it will be interpreted as an `[InOut]`.

```cs
[User]
public void GetPosition([Output] ref Position GetPosition)
{
    GetPosition = CurrentPosition;
}
```
