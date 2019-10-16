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

```c#
public static byte[] ByteArrayUtils.PadBigEndianMostSignificantBytes(this byte[] source, int finalLength, byte element = 0x00)
```

```c#
public static byte[] ByteArrayUtils.PadLittleEndianMostSignificantBytes(this byte[] source, int finalLength, byte element = 0x00)
```

### Appending

Appending operations are endian agnostic.

```c#
public static byte[] ByteArrayUtils.AppendBytes(this byte[] source, int count, byte element = 0x00)
```

```c#
public static (byte[] left, byte[] right) ByteArrayUtils.AppendShortest(byte[] left, byte[] right)
```

### Prepend

Prepending operations are endian agnostic.

```c#
public static byte[] ByteArrayUtils.PrependBytes(this byte[] source, int count, byte element = 0x00)
```

```c#
public static (byte[] left, byte[] right) ByteArrayUtils.PrependShortest(byte[] left, byte[] right)
```

### Reversing

```c#
public static byte[] ByteArrayUtils.ReverseBytes(this byte[] bytes)
```

The `ReverseBytes` operation is endian agnostic.

## Stringification

```c#
public static string ByteArrayUtils.ToString(this byte[] bytes, string format = "g", IFormatProvider formatProvider = null)
```

## Effective Length

```c#
public static int ByteArrayUtils.BigEndianEffectiveLength(this byte[] input)
```

```c#
public static int ByteArrayUtils.LittleEndianEffectiveLength(this byte[] input)
```