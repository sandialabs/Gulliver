# General Byte Array Operations

Unless otherwise stated the following methods are statically defined in the `ByteArrayUtils` class and do not modify their input.

## Byte Array Creation and Population

### Create

Sometimes it is necessary to create a byte array filled with a known value. In this case `ByteArrayUtils.CreateByteArray` can be used to create a byte array of a given `length` filled with an option `element` value.

```c#
public static byte[] ByteArrayUtils.CreateByteArray(int length, byte element = 0x00)
```

For example the following code

```c#

   /// <summary>
   ///     Example usage of <see cref="ByteArrayUtils.CreateByteArray"/>
   /// </summary>
   public static void CreateByteArrayExample()
   {
      // Setup
      const int length = 10;
      const byte element = 0x42;  // optional, defaults to 0x00
   
      // Act
      var resultBytes = ByteArrayUtils.CreateByteArray(length, element);  // creates a byte array of length 10, filled with bytes of 0x42
   
      // Conclusion
      var asString = string.Join(", ", resultBytes.Select(b => $"0x{b:x2}"));
      Console.WriteLine($"[{asString}]"); // outputs "[0x42, 0x42, 0x42, 0x42, 0x42, 0x42, 0x42, 0x42, 0x42, 0x42]"
   }

```

Creates a byte array `resultBytes` with a value of `[0x42, 0x42, 0x42, 0x42, 0x42, 0x42, 0x42, 0x42, 0x42, 0x42]`


## Byte Array Mutation

### Trimming

```c#
public static byte[] ByteArrayUtils.TrimBigEndianLeadingZeroBytes(this byte[] input)
```

```c#
public static byte[] ByteArrayUtils.TrimLittleEndianLeadingZeroBytes(this byte[] input)
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

## Miscellaneous

### Effective Length

```c#
public static int ByteArrayUtils.BigEndianEffectiveLength(this byte[] input)
```

```c#
public static int ByteArrayUtils.LittleEndianEffectiveLength(this byte[] input)
```