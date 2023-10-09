# Basics
[Basics.cs](Basics.cs)

The file basic.cs shows the basic attributes to make something visible to the PLCnext Engineer.

Firstly, use the `[Function]` or `[FunctionBlock]` attribute above classes that are IEC FUs or FBs.
The FU/FB will be given the same name as the class.

The next attributes are inside a class. `[Input]`, `[Output]` and `[InOut]` are used to define the interface of your FU/FB.
The FU/FB ports will be given the same names as the corresponding fields. 

The last two attributes in this example are `[Initialization]` and `[Execution]`. 
The Initialization method will run once when your controller starts. The method marked with the Execution attribute will be called whenever the FU/FB is called.
The method names are unregarded for these two attributes.

```cs
[FunctionBlock]
public class Sample
{
    [Input]
    public bool XX;

    [Output]
    public int YY;

    [InOut]
    public int XXYY;
        
    [Initialization]
    public void __Init()
    {
    }

    [Execution]
    public void __Process()
    {
    }
}
```