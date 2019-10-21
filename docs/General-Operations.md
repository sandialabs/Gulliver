# General Byte Array Operations

There exist a number of operations one may want to do on an array of bytes that don't directly relate to explicit higher order mathematical operations or typical bitwise operations, baring any other name space we're considering these general operations. Typically their about building, transforming, mutating, or gathering meta data.

Unless otherwise stated the following methods are statically defined in the `ByteArrayUtils` class and do not modify their input.

## Byte Array Creation and Population

It is usually easy enough to new up a new byte array, however sometimes something a little more exotic than an array of `0x00` bytes are desired.

### Create

It can be necessary to create a byte array filled with a known value. In this case `ByteArrayUtils.CreateByteArray` can be used to create a byte array of a given `length` filled with an optional `element` value.

```c#
public static byte[] ByteArrayUtils.CreateByteArray(int length, byte element = 0x00)
```

In the following example a byte array of length `10` is filled with the the repeated byte value of `0x42`

```c#
public static void CreateByteArrayExample()
{
   // Setup
   const int length = 10;
   const byte element = 0x42; // optional, defaults to 0x00

   // Act
   var result = ByteArrayUtils.CreateByteArray(length, element); // creates a byte array of length 10, filled with bytes of 0x42

   // Conclusion
   Console.WriteLine("CreateByteArray example");
   Console.WriteLine($"length:\t{length}");
   Console.WriteLine($"element:\t{element}");
   Console.WriteLine($"result:\t{result.ToString("H")}");
}
```

```none
CreateByteArray example
length:  10
element: 66
result: 42 42 42 42 42 42 42 42 42 42
```

Creates a byte array `resultBytes` with a value of `[0x42, 0x42, 0x42, 0x42, 0x42, 0x42, 0x42, 0x42, 0x42, 0x42]`


## Byte Array Mutation

Byte arrays often need to be altered in some way to process them, the addition of needing to be concerned with endiness can make this a bit less straightforward. 

### Trimming

Leading zero byte trimming works similarly for both big and little endian byte arrays. In both cases leading, or most significant, zero value bytes are removed. For big endian those bytes starting at the 0th index are removed, whereas for little endian zero bytes are removed from the tail of the array. 

If a byte array has no most significant zero valued bytes then a copy of the original will be returned.

#### Big Endian

To trim all `0x00` bytes starting at the 0th index of the byte array

```c#
public static byte[] ByteArrayUtils.TrimBigEndianLeadingZeroBytes(this byte[] input)
```

The following example trims the array `[0x00, 0x00, 0x2A, 0x00]` returning `[0x2A, 0x00`].

```c#
public static void TrimBigEndianLeadingZeroBytes()
{
   // Setup
   var input = new byte[] { 0x00, 0x00, 0x2A, 0x00 };

   // Act
   var result = input.TrimBigEndianLeadingZeroBytes();

   // Conclusion
   Console.WriteLine("TrimBigEndianLeadingZeroBytes example");
   Console.WriteLine($"input:\t{input.ToString("H")}");
   Console.WriteLine($"result:\t{result.ToString("H")}");
}
```

```none
TrimBigEndianLeadingZeroBytes example
input:  00 00 2A 00
result: 2A 00
```

Not that the final `0x00` value was not removed as we were only trimming most significant zero values.


#### Little Endian

To trim all `0x00` bytes starting at the end of the byte array

```c#
public static byte[] ByteArrayUtils.TrimLittleEndianLeadingZeroBytes(this byte[] input)
```
```c#
public static void TrimLittleEndianLeadingZeroBytes()
{
   // Setup
   var input = new byte[] { 0x2A, 0xFF, 0x2A, 0x00 };

   // Act
   var result = input.TrimLittleEndianLeadingZeroBytes();

   // Conclusion
   Console.WriteLine("TrimLittleEndianLeadingZeroBytes");
   Console.WriteLine($"input:\t{input.ToString("H")}");
   Console.WriteLine($"result:\t{result.ToString("H")}");
}
```

```none
TrimLittleEndianLeadingZeroBytes
input:  2A FF 2A 00
result: 2A FF 2A
```

### Padding

When padding a byte array, if the the given array length is equal to or larger than `finalLength` a copy of the original array will be returned. Otherwise bytes with the value of `element` will be padded in the most significant value place.

#### Big Endian

```c#
public static byte[] ByteArrayUtils.PadBigEndianMostSignificantBytes(this byte[] source, int finalLength, byte element = 0x00)
```

```c#
public static void PadBigEndianMostSignificantBytesExample()
{
    // Setup
    var bytes = new byte[] { 0xDE, 0xFA, 0xCE, 0xC0, 0xDE };
    const int finalLength = 6;
    // Act
    var result = bytes.PadBigEndianMostSignificantBytes(finalLength);
    // Conclusion
    Console.WriteLine("PadBigEndianMostSignificantBytes Short Example");
    Console.WriteLine($"input:\t{bytes.ToString("H")}");
    Console.WriteLine($"result:\t{result.ToString("H")}");
    Console.WriteLine(string.Empty);
}
```

```none
PadBigEndianMostSignificantBytes Short Example
input:  DE FA CE C0 DE
result: 00 DE FA CE C0 DE
```

#### Little Endian

```c#
public static byte[] ByteArrayUtils.PadLittleEndianMostSignificantBytes(this byte[] source, int finalLength, byte element = 0x00)
```

```c#
public static void PadLittleEndianMostSignificantBytesExample()
{
    // Setup
    var input = new byte[] { 0xDE, 0xFA, 0xCE, 0xC0, 0xDE };
    const int finalLength = 6;
    // Act
    var result = input.PadLittleEndianMostSignificantBytes(finalLength);
    // Conclusion
    Console.WriteLine("PadLittleEndianMostSignificantBytes Example");
    Console.WriteLine($"input:\t{input.ToString("H")}");
    Console.WriteLine($"result:\t{result.ToString("H")}");
    Console.WriteLine(string.Empty);
}
```

```none
PadLittleEndianMostSignificantBytes Example
input:  DE FA CE C0 DE
result: DE FA CE C0 DE 00
```

### Appending

Appending operations are endian agnostic, new byte values will appear after the highest order index of the input array.

#### Append Bytes

The `ByteArrayUtils.AppendBytes` operation simply adds `count` bytes to the end of the value provided by the `source` array. Optional the `element` parameter may be provided to use a byte value other than the default `0x00`.

```c#
public static byte[] ByteArrayUtils.AppendBytes(this byte[] source, int count, byte element = 0x00)
```

```c#
public static void AppendBytesExample()
{
    // Setup
    var input = new byte[] { 0xC0, 0xC0, 0xCA, 0xFE };
    const int count = 4;
    // Act
    var result = input.AppendBytes(count);
    // Conclusion
    Console.WriteLine("AppendBytes Example");
    Console.WriteLine($"input:\t{input.ToString("H")}");
    Console.WriteLine($"result:\t{result.ToString("H")}");
    Console.WriteLine(string.Empty);
}
```

```none
AppendBytes Example
input:  C0 C0 CA FE
result: C0 C0 CA FE 00 00 00 00
```

#### Append Shortest

`ByteArrayUtils.AppendShortest` works much like `ByteArrayUtils.AppendBytes`, except instead of providing a desired byte count, the two input arrays lengths are compared and the shortest array is returned, along with the the longest array, with enough `0x00` bytes such that both byte arrays are now the same length.

Effectively this adds most significant `0x00` bytes to the shortest little endian byte array, but may be useful for big endian arrays as well.

```c#
public static (byte[] left, byte[] right) ByteArrayUtils.AppendShortest(byte[] left, byte[] right)
```

```c#
public static void AppendShortestExample()
{
    // Setup
    var lhs = new byte[] { 0xDE, 0xCA, 0xF0 };
    var rhs = new byte[] { 0xCA, 0xFE, 0xC0, 0xFF, 0xEE };
    // Act
    var (lhsResult, rhsResult) = ByteArrayUtils.AppendShortest(lhs, rhs);
    // Conclusion
    Console.WriteLine("AppendShortest Example");
    Console.WriteLine($"lhs:\t{lhs.ToString("H")}");
    Console.WriteLine($"rhs:\t{rhs.ToString("H")}");
    Console.WriteLine($"lhsResult:\t{lhsResult.ToString("H")}");
    Console.WriteLine($"lhsResult:\t{rhsResult.ToString("H")}");
    Console.WriteLine(string.Empty);
}
```

```none
AppendShortest Example
lhs:    DE CA F0
rhs:    CA FE C0 FF EE
lhsResult:      DE CA F0 00 00
lhsResult:      CA FE C0 FF EE
```

### Prepend

Prepending operations are endian agnostic, new byte values will appear after the lowest order index of the input array.

#### Prepend Bytes

The `ByteArrayUtils.PrependBytes` operation simply adds `count` bytes to the start of the value provided by the `source` array. Optional the `element` parameter may be provided to use a byte value other than the default `0x00`. This is essentially the inverse of `ByteArrayUtils.AppendBytes` operation.

```c#
public static byte[] ByteArrayUtils.PrependBytes(this byte[] source, int count, byte element = 0x00)
```

```c#
public static void PrependBytesExample()
{
    // Setup
    var input = new byte[] { 0xC0, 0xC0, 0xCA, 0xFE };
    const int count = 4;
    // Act
    var result = input.PrependBytes(count);
    // Conclusion
    Console.WriteLine("PrependBytes Example");
    Console.WriteLine($"input:\t{input.ToString("H")}");
    Console.WriteLine($"result:\t{result.ToString("H")}");
    Console.WriteLine(string.Empty);
}
```

```none
PrependBytes Example
input:  C0 C0 CA FE
result: 00 00 00 00 C0 C0 CA FE
```
#### Prepend Shortest

`ByteArrayUtils.PrependShortest` works much like `ByteArrayUtils.PrependBytes`, except instead of providing a desired byte count, the two input arrays lengths are compared and the shortest array is returned, along with the the longest array, with enough `0x00` bytes such that both byte arrays are now the same length.

Effectively this adds most significant `0x00` bytes to the shortest big endian byte array, but may be useful for little endian arrays as well.

```c#
public static (byte[] left, byte[] right) ByteArrayUtils.PrependShortest(byte[] left, byte[] right)
```

```c#
public static void PrependShortestExample()
{
    // Setup
    var lhs = new byte[] { 0xDE, 0xCA, 0xF0 };
    var rhs = new byte[] { 0xCA, 0xFE, 0xC0, 0xFF, 0xEE };
    // Act
    var (lhsResult, rhsResult) = ByteArrayUtils.PrependShortest(lhs, rhs);
    // Conclusion
    Console.WriteLine("PrependShortest Example");
    Console.WriteLine($"lhs:\t{lhs.ToString("H")}");
    Console.WriteLine($"rhs:\t{rhs.ToString("H")}");
    Console.WriteLine($"lhsResult:\t{lhsResult.ToString("H")}");
    Console.WriteLine($"lhsResult:\t{rhsResult.ToString("H")}");
    Console.WriteLine(string.Empty);
}
```

```none
PrependShortest Example
lhs:    DE CA F0
rhs:    CA FE C0 FF EE
lhsResult:      00 00 DE CA F0
lhsResult:      CA FE C0 FF EE
```

### Reversing

Unsurprisingly, hopefully, `ByteArrayUtils.ReverseBytes` returns the reverse of the provided `bytes` byte array.

The `ReverseBytes` operation is endian agnostic.

```c#
public static byte[] ByteArrayUtils.ReverseBytes(this byte[] bytes)
```

```c#
public static void ReverseBytesExample()
{
    // Setup
    var input = new byte[] { 0xC0, 0x1D, 0xC0, 0xFF, 0xEE };
    // Act
    var result = input.ReverseBytes();
    // Conclusion
    Console.WriteLine("ReverseBytes example");
    Console.WriteLine($"input:\t{input.ToString("H")}");
    Console.WriteLine($"result:\t{result.ToString("H")}");
    Console.WriteLine(string.Empty);
}
```

```none
ReverseBytes example
input:  C0 1D C0 FF EE
result: EE FF C0 1D C0
```

## Stringification

What can be better than making byte arrays slightly more human readable!? We provide a number of ways to format a byte array that will hopefully meet your needs. Because `byte[]` don't implement `IFormattable` you have to be explicit about calling `ByteArrayUtils.ToString` and cannot rely on  string interpolation or `string.Format` and the typical format provider.


Implemented `format` values include:
 
 - `g` (default) `G`, `H`, or empty string

   General Format Hexadecimal (base 16) bytes

   Formats as uppercase hexadecimal digits with individual bytes delimited by a single space character

   example: `C0 FF EE C0 DE`
   
 - `HC`

   Uppercase Hexadecimal (base 16) Contiguous
   
   Formats bytes as uppercase contiguous hexadecimal digits

   *example*: `C0FFEEC0DE`

 - `h`

   Lowercase Hexadecimal (base 16) bytes

   Formats bytes as lowercase hexadecimal digits with individual bytes delimited by a single space character

   *example*: `c0 ff ee c0 de`
 
 - `hc`

   Lowercase Hexadecimal Contiguous
   
   Formats bytes as lowercase contiguous hexadecimal digits

   *example*: `c0ffeec0de`

 - `b`, or `B`

   Binary (base 2) bytes

   Formats bytes as binary digits with individual bytes delimited by a single space character

   *example*: `11000000 11111111 11101110 11000000 11011110`

 - `bc`, or `BC`

   Binary Contiguous

   Formats bytes as contiguous binary digits

   *example*: `1100000011111111111011101100000011011110`

 - `d`, or `D`

   Decimal (base 10) bytes

   Formats bytes as decimal (base 10) digits with individual bytes delimited by a single space character

   *example*: `192 255 238 192 222`


 - `I`, or `IBE`

   Integer (Big Endian)

   Formats bytes as big endian unsigned decimal (base 10) integer

   *example*: `828927557854`

 - `ILE` 
 
   Integer (Little Endian)

   Formats bytes as little endian unsigned decimal (base 10) integer

   *example*: `956719628224`

```c#
public static string ByteArrayUtils.ToString(this byte[] bytes, string format = "g", IFormatProvider formatProvider = null)
```

```c#
public static void StringificationExample()
{
   // Setup
   var input = new byte[] { 0xC0, 0xFF, 0xEE, 0xC0, 0xDE };

   // Conclusion
   Console.WriteLine("Stringification Example");
   Console.WriteLine($"input:\t{input.ToString("H")}");

   Console.WriteLine("Hexadecimal Formats");
   Console.WriteLine($"H:\t\"{input.ToString("H")}\"");
   Console.WriteLine($"h:\t\"{input.ToString("h")}\"");
   Console.WriteLine($"HC:\t\"{input.ToString("HC")}\"");
   Console.WriteLine($"hc:\t\"{input.ToString("hc")}\"");

   Console.WriteLine("Binary Formats");
   Console.WriteLine($"b:\t\"{input.ToString("b")}\"");
   Console.WriteLine($"bc:\t\"{input.ToString("bc")}\"");

   Console.WriteLine("Integer Formats");
   Console.WriteLine($"d:\t\"{input.ToString("d")}\"");
   Console.WriteLine($"IBE:\t\"{input.ToString("IBE")}\"");
   Console.WriteLine($"ILE:\t\"{input.ToString("ILE")}\"");

   Console.WriteLine(string.Empty);
}
```

```none
Stringification Example
input:  C0 FF EE C0 DE
Hexadecimal Formats
H:      "C0 FF EE C0 DE"
h:      "c0 ff ee c0 de"
HC:     "C0FFEEC0DE"
hc:     "c0ffeec0de"
Binary Formats
b:      "11000000 11111111 11101110 11000000 11011110"
bc:     "1100000011111111111011101100000011011110"
Integer Formats
d:      "192 255 238 192 222"
IBE:    "828927557854"
ILE:    "956719628224"
```

## Effective Length

Effective length provides the ability to count the number of non-most significant bytes within a byte array. Eg. the length of meaningful bytes within the array.

### Big Endian

`ByteArrayUtils.BigEndianEffectiveLength` returns an `int` representing the byte length of the given `input` disregarding the `0x00` bytes at the beginning of the array.

```c#
public static int ByteArrayUtils.BigEndianEffectiveLength(this byte[] input)
```

```c#
public static void BigEndianEffectiveLengthExample()
{
    // Setup
    var input = new byte[] { 0x00, 0x00, 0x00, 0xDA, 0xBD, 0xAD };
    // Act
    var result = input.BigEndianEffectiveLength();
    // Conclusion
    Console.WriteLine("BigEndianEffectiveLength Example");
    Console.WriteLine($"input:\t{input.ToString("H")}");
    Console.WriteLine($"result:\t{result}");
    Console.WriteLine(string.Empty);
}
```

```none
BigEndianEffectiveLength Example
input:  00 00 00 DA BD AD
result: 3
```

### Little Endian

`ByteArrayUtils.LittleEndianEffectiveLength` returns an `int` representing the byte length of the given `input` disregarding the `0x00` bytes at the end of the array.

```c#
public static int ByteArrayUtils.LittleEndianEffectiveLength(this byte[] input)
```

```c#
public static void LittleEndianEffectiveLengthExample()
{
    // Setup
    var input = new byte[] { 0xDA, 0xB0, 0x00, 0x00, 0x00, 0x00 };
    // Act
    var result = input.LittleEndianEffectiveLength();
    // Conclusion
    Console.WriteLine("LittleEndianEffectiveLength Example");
    Console.WriteLine($"input:\t{input.ToString("H")}");
    Console.WriteLine($"result:\t{result}");
    Console.WriteLine(string.Empty);
}
```

```none
LittleEndianEffectiveLength Example
input:  DA B0 00 00 00 00
result: 2
```
