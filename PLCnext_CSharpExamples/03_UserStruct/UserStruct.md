# User Struct

([FBWithUserStructs.cs](FBWithUserStructs.cs))

To make a structure visible in PLCnext Engineer, the attribute `[Structure]` is needed and the fields must be public.

```cs
    [Structure]
    public struct Position
    {
        public int x;
        public int y;
    }
```

Using the structure in your C# code can be done the safe way as Input, Output or InOut.

```cs
    [FunctionBlock]
    public class FB_with_user_struct
    {
        [Input]
        public Position NEW_POSITION;
```

For larger structs it could be interesting to pass it by reference. Not copying the values can save Memory and CPU time. This works only for Input and InOut parameters.
The field and the execution method need the `unsafe` modifier. This denotes an unsafe context, which is required for any operation involving pointers.

```cs
[Input]
unsafe public Position* NEW_POSITION;
...
[Execution]
unsafe public void __Process()
{
    if ((CURRENT_POSITION).x < (*NEW_POSITION).x)
{
...
```
