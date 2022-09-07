# IEC WString

([FBWithWString.cs](FBWithWString.cs))

IEC WStrings always contain the number of characters in double bytes. Preceding is a 4 byte header. The character string must be terminated with two bytes containing a zero character.  All IEC WStrings use internally a structure `IecWString`. Though the string is defined as using double characters, also 4 byte UTF-16 characters can be used. The length of the string is shortened.

## Fixed length string

### IecWString80

The default string IECWString80's maximum size is 160 bytes, which is set implicitly. The memory representation is 80 characters (double byte) of string content + 4 byte header + 2 byte terminating zeroes.  

## User-defined string

The IEC user string type can be defined to the users needs. The memory representation is the amount of characters in the string (double byte) + 4 byte header + 2 byte terminating zeroes. For user defined strings, a structure of type `IecWString` is necessary. The `Init()` method of this string must set its maximum character size.

## IecWString

The `IecWString` structure contains the member `m_cap` The member `m_cap` must be initialized to the required character size before use. A field of the type `IecWString` must have the `[FieldOffset(0)]` attribute, to define the beginning of the IEC strings physical representation.  

The sample function block adds to an input string another string. One character is a special 4 byte UTF-16 character.
