#Misc Attributes
([MiscAttributes.md](MiscAttributes.md))

`[DataType]`

The following table shows how the IEC 61131-3 data types
are mapped to the .NET-Framework and to C#, respectively.
Variables of data types that are marked in the column "DataType Attribute" with
a '+' must have the optional attribute "DataType" for an unambiguous assignment.

| IEC 61131-3       | .NET Framework                 | C#     | DataType Attribute  |
|-------------------|--------------------------------|--------|-----------|
|    BOOL           | System.Boolean                 | bool   |     -     |
|    SINT           | System.SByte                   | sbyte  |     -     |
|    INT            | System.Int16                   | short  |     -     |
|    DINT           | System.Int32                   | int    |     -     |
|    LINT           | System.Int64                   | long   |     -     |
|    USINT          | System.Byte                    | byte   |     -     |
|    UINT           | System.UInt16                  | ushort |     -     |
|    UDINT          | System.UInt32                  | uint   |     -     |
|    ULINT          | System.UInt64                  | ulong  |     -     |
|    REAL           | System.Single                  | float  |     -     |
|    LREAL          | System.Double                  | double |     -     |
|    TIME           | System.UInt32                  | uint   |     +     |
|    LTIME          | System.Int64                   | long   |     +     |
|    LDATE          | System.Int64                   | long   |     +     |
|    LTOD           | System.Int64                   | long   |     +     |
|    LDT            | System.Int64                   | long   |     +     |
|    BYTE           | System.Byte                    | byte   |     +     |
|    WORD           | System.UInt16                  | ushort |     +     |
|    DWORD          | System.UInt32                  | uint   |     +     |
|    LWORD          | System.UInt64                  | ulong  |     +     |
|    STRING         | System.Iec61131Lib.IecStringEx |  |     -     |
|    ANY            | System.Iec61131Lib.Any         |  |     +     |
|    ANY_MAGNITUDE  | System.Iec61131Lib.Any         |  |     +     |
|    ANY_NUM        | System.Iec61131Lib.Any         |  |     +     |
|    ANY_INT        | System.Iec61131Lib.Any         |  |     +     |
|    ANY_SIGNED     | System.Iec61131Lib.Any         |  |     +     |
|    ANY_UNSIGNED   | System.Iec61131Lib.Any         |  |     +     |
|    ANY_REAL       | System.Iec61131Lib.Any         |  |     +     |
|    ANY_BIT        | System.Iec61131Lib.Any         |  |     +     |
|    ANY_ELEMENTARY | System.Iec61131Lib.Any         |  |     +     |

`[OPC]`

PLCnext has an integrated OPC server. The [OPC] attribute adds the related field as a new entry in the server tree, under the root entry ARP.Eclr.

`[GdsRetain]`

This attribute allowes variable and port values to be kept during power down so these values can be restored on power return. For more information you can read about retain handling [here](https://www.plcnext.help/te/PLCnext_Runtime/Extended_retain_handling.htm) in the PLCnext Info Center. The attribute [Retain] is depricate and does not have any function.
