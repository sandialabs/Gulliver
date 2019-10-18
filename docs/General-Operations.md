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
/// <summary>
///     Example usage of <see cref="ByteArrayUtils.CreateByteArray" />
/// </summary>
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

```
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

To trim a big endian byte array use

```c#
public static byte[] ByteArrayUtils.TrimBigEndianLeadingZeroBytes(this byte[] input)
```

The following example 

```c#
/// <summary>
///     Example usage of <see cref="ByteArrayUtils.TrimBigEndianLeadingZeroBytes" />
/// </summary>
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

```
TrimBigEndianLeadingZeroBytes example
input:  00 00 2A 00
result: 2A 00
```

Not that the final `00` value was not removed as we were only trimming most significant zero values.


#### Little Endian

To trim a little endian byte array use

```c#
public static byte[] ByteArrayUtils.TrimLittleEndianLeadingZeroBytes(this byte[] input)
```
```c#
/// <summary>
///     Example usage of <see cref="ByteArrayUtils.TrimLittleEndianLeadingZeroBytes" />
/// </summary>
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
```
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
```

```
PadBigEndianMostSignificantBytes Short Example
input:  DE FA CE C0 DE
result: 00 DE FA CE C0 DE
```

#### Little Endian

```c#
public static byte[] ByteArrayUtils.PadLittleEndianMostSignificantBytes(this byte[] source, int finalLength, byte element = 0x00)
```

```c#
```

```
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
```

```
AppendBytes Example
input:  C0 C0 CA FE
result: C0 C0 CA FE 00 00 00 00
```

#### Append Shortest

```c#
public static (byte[] left, byte[] right) ByteArrayUtils.AppendShortest(byte[] left, byte[] right)
```

```c#
```

```
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
```

```
PrependBytes Example
input:  C0 C0 CA FE
result: 00 00 00 00 C0 C0 CA FE
```
#### Prepend Shortest

```c#
public static (byte[] left, byte[] right) ByteArrayUtils.PrependShortest(byte[] left, byte[] right)
```

```c#
```

```
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
```

```
ReverseBytes example
input:  C0 1D C0 FF EE
result: EE FF C0 1D C0
```

## Stringification

```c#
public static string ByteArrayUtils.ToString(this byte[] bytes, string format = "g", IFormatProvider formatProvider = null)
```

```c#
```

```
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
```

```
BigEndianEffectiveLength Example
input:  00 00 00 DA BD AD
result: 3
```

### Little Endian

```c#
public static int ByteArrayUtils.LittleEndianEffectiveLength(this byte[] input)
```

```c#
```

```
LittleEndianEffectiveLength Example
input:  DA B0 00 00 00 00
result: 2
```