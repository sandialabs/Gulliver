# Unsigned Mathematical Operations

Unless otherwise stated the following methods are statically defined in the `ByteArrayUtils` class and do not modify their input.

## Addition 

```c#
public static byte[] ByteArrayUtils.AddUnsignedBigEndian(byte[] right, byte[] left)
```

```c#
public static byte[] ByteArrayUtils.AddUnsignedLittleEndian(byte[] left, byte[] right)
```
### Safe Addition

```c#
public static bool ByteArrayUtils.TrySumBigEndian(byte[] source, long delta, out byte[] result)
```

```c#
public static bool ByteArrayUtils.TrySumLittleEndian(byte[] source, long delta, out byte[] result)
```

## Subtraction

```c#
public static byte[] ByteArrayUtils.SubtractUnsignedBigEndian(byte[] left, byte[] right)

```

```c#
public static byte[] ByteArrayUtils.SubtractUnsignedLittleEndian(byte[] left, byte[] right)
```

### Safe Subtraction

Seemingly conspicuously absent are the `TrySubtractBigEndian` and `TrySubtractLittleEndian` equivalents of the `TrySumBigEndian` and `TrySumLittleEndian` methods. In actuality the various TrySum method allow for a negative `delta` and therefore are functionally equivalent for safe subtraction. 

## Comparison

```c#
public static int ByteArrayUtils.CompareUnsignedBigEndian(byte[] left, byte[] right)
```

```c#
public static int ByteArrayUtils.CompareUnsignedLittleEndian(byte[] left, byte[] right)
```