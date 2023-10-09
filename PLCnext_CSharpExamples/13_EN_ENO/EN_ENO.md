# EN\/ENO

[FB_ENENO.cs](FB_ENENO.cs), [FU_ENENO.cs](FU_ENENO.cs)

Minimal required:

[![PLCnext Engineer](https://img.shields.io/badge/PLCnext_Engineer-2021.0_LTS-blue.svg)](http://www.phoenixcontact.net/qr/1046008/softw)

[![PLCnext Engineer](https://img.shields.io/badge/PLCnext_Firmware-2021.0_LTS-blue.svg)](http://www.phoenixcontact.net/qr/2404267/firmware)

In IEC 61131-3 the variables EN and ENO are defined as following:
If the EN parameter is FALSE, the function body is skipped and ENO is set to FALSE.
If the EN parameter is TRUE, the function body is executed. If the function executes without error, the ENO parameter is set to TRUE. If there is an error in the execution of the function, the ENO parameter is set to FALSE.

## EN/ENO in FunctionBlocks

To use EN/ENO with FunctionBlocks in C#, the developer has to implement the behavior. The developers FB code must check the incomming EN and set ENO accodingly, as well as all outputs to default values (excluding arrays and structs).
To fullfill the standard, the following rules have to be considered.

- **EN** has to be the first defined input and be in capital letters
- **ENO** has to be the first defined output and be in capital letters
- On error return (ENO == false) , the outputs must be well defined

## EN/ENO in Functions

The function class has to be attributed with "[Eno]".
If ENO shall be set to FALSE by the function, the prototype must be defined with a bool return value and for the function output the class/function name need to be used for the first parameter as reference.

The C# code has not to handle the EN/ENO parameters at all. If there is no need of manipulating ENO by the function, the EN/ENO functionality is handled and added by the IEC compiler itself and always returns "TRUE".
