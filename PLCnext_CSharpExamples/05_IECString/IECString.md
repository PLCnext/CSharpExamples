# IEC String

([FBWithString.cs](FBWithString.cs))

IEC strings always contain the number of characters. Preceding is a 4 byte header.
The character string must be terminated with one byte containing zero. The memory representation may contain one byte padding to fulfill 2 byte alignment. All IEC strings use internally a structure `IecStringEx`

## Fixed length string

### IECString80

The default string IECString80's maximum size is 80 bytes, which is set implicitly. The memory representation is 80 characters of string content + 4 byte header + 1 byte terminating zero + padding for two byte alignment.

## User-defined string

### IECString

The IEC user string type can be defined to the users needs. The memory representation is the amount of characters in the string + 4 byte header + 1 byte terminating zero + padding for two byte alignment. For user defined strings, a structure of type "IecStringEx" is necessary. The `Init()` method of this string must set its maximum size via `.maximumLength`.

## IecStringEx

The IecStringEx structure contains the members `maximumLength` and `currentLength`
The member maximumLength must be initialized to the required size before use.
A field of the type `IecStringEx` must have the `FieldOffset()` attribute, to define the beginning of the IEC strings physical representation.

## Two function block variants are implemented

Both function blocks copy the input string `VALUE` to the output string `RESULT1` and
set the output string `RESULT2` to a different text controlled by input `MESSAGE_ID`.

1. **FunctionBlock1**

   Implements the function block with parameter passing by value
2. **FunctionBlock2**

   Implements the function block with parameter passing by reference which means `[InOut]` parameter declaration is used.
