# EN\/ENO

[FB_ENENO.cs](FB_ENENO.cs), [FU_ENENO.cs](FU_ENENO.cs)

Minimal required:

[![PLCnext Engineer](https://img.shields.io/badge/PLCnext_Engineer-2021.0_LTS-blue.svg)](http://www.phoenixcontact.net/qr/1046008/softw)

[![PLCnext Engineer](https://img.shields.io/badge/PLCnext_Firmware-2021.0_LTS-blue.svg)](http://www.phoenixcontact.net/qr/2404267/firmware)

In IEC 61131-3 the variables EN and ENO are defined as following:
If the EN parameter is FALSE, the function body is skipped and ENO is set to FALSE.
If the EN parameter is TRUE, the function body is executed. If the function executes without error, the ENO parameter is set to TRUE. If there is an error in the execution of the function, the ENO parameter is set to FALSE.

To use EN/ENO with Functions and FunctionBlocks in C# the developer has to implement the behavior.
To fullfill the standard, the following rules have to be considered.

- **EN** has to be the first defined input in capital letters
- **ENO** has to be the first defined output in capital letters
- On error return (ENO == false) , the outputs must be well defined
