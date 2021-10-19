# User Struct

([FBWithUserStructs.cs](FBWithUserStructs.cs))

To make a structure visible in PLCnext Engineer, the attribute 
`[Structure]` is needed and the fields must be declared public.

```cs
    [Structure]
    public struct Position
    {
        public int x;
        public int y;
    }
```

Using the structure in your C# code can be used as Input and Output as well.

```cs
    [FunctionBlock]
    public class FB_with_user_struct
    {
        [Input]
        public Position NEW_POSITION;
```

For larger structs it could be interesting to pass it by reference. 
Not buffering the values saves memory and CPU time.
This works only for InOut parameters.
The field and the execution method needs the `unsafe` modifier. 
This denotes an `unsafe` code, which is required for any operation involving pointers.<br><br>
**Note:**
 - `unsafe` code is only possible if in project Properties Build settings 
"Allow unsafe code" is switched on.
 - The use of pointer is currently only supported by InOut parameter.<br>

```cs
[InOut]
unsafe public Position* NEW_POSITION;
...
[Execution]
unsafe public void __Process()
{
    if ((CURRENT_POSITION).x < (*NEW_POSITION).x)
    {
       ...
```
## Two function block variants are implemented
Both function blocks are called with an input and output structure.
The output structure is modified in dependency of the input structure.
1. **FunctionBlock1**<br>
   Implements the function block with parameter passing by value
2. **FunctionBlock2**<br>
   Implements the function block with parameter passing by reference 
which means [InOut] parameter declaration is used.

