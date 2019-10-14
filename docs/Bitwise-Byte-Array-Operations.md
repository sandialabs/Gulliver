# Bitwise Byte Array Operations

Unless otherwise stated the following methods are statically defined in the `ByteArrayUtils` class and do not modify their input.

## Addressing

### Byte Array as Bit Array

```c#
public static bool ByteArrayUtils.AddressBit(this byte[] bytes, int index)
```

## Boolean Operations

### NOT

The `BitwiseNot` operation is endian agnostic.

```c#
public static byte[] ByteArrayUtils.BitwiseNot(byte[] bytes)
```

### AND

```c#
public static byte[] ByteArrayUtils.BitwiseAndBigEndian(byte[] left, byte[] right)
```

```c#
public static byte[] ByteArrayUtils.BitwiseAndLittleEndian(byte[] left, byte[] right)
```

### OR

```c#
public static byte[] ByteArrayUtils.BitwiseOrBigEndian(byte[] left, byte[] right)
```

```c#
public static byte[] ByteArrayUtils.BitwiseOrLittleEndian(byte[] left, byte[] right)
```

### XOR

```c#
public static byte[] ByteArrayUtils.BitwiseXorBigEndian(byte[] left, byte[] right)
```

```c#
public static byte[] ByteArrayUtils.BitwiseXorLittleEndian(byte[] left, byte[] right)
```

## Bitshifting

Bitshifting operations are endian agnostic.

### Shift Right

```c#
public static byte[] ByteArrayUtils.ShiftBitsRight(this byte[] bytes, int shift)
```

```c#
public static byte[] ByteArrayUtils.ShiftBitsRight(this byte[] bytes, int shift, out byte[] carry)
```

### Shift Left

```c#
public static byte[] ByteArrayUtils.ShiftBitsLeft(this byte[] bytes, int shift)
```

```c#
public static byte[] ByteArrayUtils.ShiftBitsLeft(this byte[] bytes, int shift, out byte[] carry)
```