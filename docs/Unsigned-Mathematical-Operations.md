# Unsigned Mathematical Operations

Unless otherwise stated the following methods are statically defined in the `ByteArrayUtils` class and do not modify their input.

## Addition 

### Big Endian

```c#
public static byte[] ByteArrayUtils.AddUnsignedBigEndian(byte[] right, byte[] left)
```

```c#
```

```
AddUnsignedBigEndian Example
lhs:    AD DE D0
rhs:    C0 DE
result: AE 9F AE

lhs:    11394768
rhs:    49374
result: 11444142

```

### Little Endian

```c#
public static byte[] ByteArrayUtils.AddUnsignedLittleEndian(byte[] left, byte[] right)
```

```c#
```

```
AddUnsignedLittleEndian Example
lhs:    AD DE D0
rhs:    C0 DE
result: 6D BD D1

lhs:    13688493
rhs:    57024
result: 13745517
```

## Subtraction

### Big Endian

```c#
public static byte[] ByteArrayUtils.SubtractUnsignedBigEndian(byte[] left, byte[] right)

```

```c#
```

```
```

### Little Endian

```c#
public static byte[] ByteArrayUtils.SubtractUnsignedLittleEndian(byte[] left, byte[] right)
```

```c#
```

```
```

## Safe Summation

### Big Endian

```c#
public static bool ByteArrayUtils.TrySumBigEndian(byte[] source, long delta, out byte[] result)
```
```c#
```

```
TrySumBigEndian Example
bytes:  AD DE D0
delta:  42
success:        True
result: AD DE FA

bytes:  11394768
result: 11394810
```

### Little Endian

```c#
public static bool ByteArrayUtils.TrySumLittleEndian(byte[] source, long delta, out byte[] result)
```

```c#
```

```
TrySumLittleEndian Example
bytes:  AD DE D0
delta:  -42
success:        True
result: 83 DE D0

bytes:  13688493
result: 13688451
```

#### Safe Subtraction

Seemingly conspicuously absent are the `TrySubtractBigEndian` and `TrySubtractLittleEndian` equivalents of the `TrySumBigEndian` and `TrySumLittleEndian` methods. In actuality the various TrySum methods allow for a negative `delta` and therefore are functionally equivalent for safe subtraction. 

## Comparison

### Big Endian

```c#
public static int ByteArrayUtils.CompareUnsignedBigEndian(byte[] left, byte[] right)
```

```c#
```

```
lhs:    B1 66 E5 70
rhs:    5A 11
result: 1

lhs:    2976310640
rhs:    23057
result: 1
```

### Little Endian

```c#
public static int ByteArrayUtils.CompareUnsignedLittleEndian(byte[] left, byte[] right)
```

```c#
```

```
CompareUnsignedLittleEndian Example
lhs:    B1 66 E5 70
rhs:    5A 11
result: 1

lhs:    1894082225
rhs:    4442
result: 1
```