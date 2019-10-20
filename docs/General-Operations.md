# General Byte Array Operations

Unless otherwise stated the following methods are statically defined in the `ByteArrayUtils` class and do not modify their input.

## Byte Array Creation and Population

### Create

Sometimes it is necessary to create a byte array filled with a known value. In this case `ByteArrayUtils.CreateByteArray` can be used to create a byte array of a given `length` filled with an option `element` value.

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

### Trimming

Leading zero byte trimming works similarly for both big and little endian byte arrays. In both cases leading, or most significant, zero value bytes are removed. For big endian those bytes starting at the 0th index are removed, whereas for little endian zero bytes are removed from the tail of the array. 

If a byte array has no most significant zero values then a copy of the original will be returned.

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

Appending operations are endian agnostic.

#### Append Bytes

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

Prepending operations are endian agnostic.

#### Prepend Bytes
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

```c#
public static byte[] ByteArrayUtils.ReverseBytes(this byte[] bytes)
```

The `ReverseBytes` operation is endian agnostic.

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

### Big Endian


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