# Methods
([FBWithMethods.cs](FBWithMethods.cs))

Beside the obligatory methods for initialization and execution, a class can contain optional methods. To make these user methods available in IEC, the attribute `[User]` is used.

```cs
[User]
public void SetX(int x)
{
    CurrentPosition.x = x;
}
```

As in the Complex Data Types example, user methods must also return complex types by reference.

```cs
[User]
public void GetPosition(ref Position actualPosition)
{
    actualPosition = CurrentPosition;
}
```