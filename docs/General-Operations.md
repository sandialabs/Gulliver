# General Byte Array Operations

Unless otherwise stated the following methods are statically defined in the `ByteArrayUtils` class and do not modify their input.

## Byte Array Creation and Population

### Create

```c#
public static byte[] ByteArrayUtils.CreateByteArray(int length, byte element = 0x00)
```

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