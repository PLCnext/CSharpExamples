# Programs
[Program.cs](Program.cs)

Minimal Verions Required: 
[![PLCnext Engineer](https://img.shields.io/badge/PLCnext_Engineer-2019.3-blue.svg)](http://www.phoenixcontact.net/qr/1046008/softw)
[![PLCnext Engineer](https://img.shields.io/badge/PLCnext_Firmware-2019.0_LTS-blue.svg)](http://www.phoenixcontact.net/qr/2404267/firmware)

The Program.cs is a basic template to write realtime C# programs for PLCnext Technology.

Firstly, use the `[Program]` attribute above classes that shall be an program.
The program will be given the same name as the class.

The next attributes are inside a class. `[Global, OutputPort]`, `[Global, InputPort]` are used to define the port variables of your program.
The ports will be given the same names as the corresponding fields. 

The last two attributes in this example are `[Initialization]` and `[Execution]`. 
The Initialization method will run once when your controller starts. The method marked with the Execution attribute will be called whenever the program is called.
The method names do not matter for these two attributes.

```cs
[Program]
public class Sample
{
    [Global, OutputPort]
    public bool XX;

    [Global, InputPort]
    public int YY;
		
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