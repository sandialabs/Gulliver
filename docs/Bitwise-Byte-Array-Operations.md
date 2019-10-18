# Bitwise Byte Array Operations

Unless otherwise stated the following methods are statically defined in the `ByteArrayUtils` class and do not modify their input.

## Addressing

### Byte Array as Bit Array

```c#
public static bool ByteArrayUtils.AddressBit(this byte[] bytes, int index)
```

```c#
```

```none
AddressBit Example
input:  C0 1D
input:  11000000 00011101
result: [4]:0, [5]:0, [6]:1, [7]:1, [8]:1, [9]:0, [10]:1, [11]:1
```

## Boolean Operations

### NOT

The `BitwiseNot` operation is endian agnostic.

```c#
public static byte[] ByteArrayUtils.BitwiseNot(byte[] bytes)
```

```c#
```

```none
BitwiseNot Example
input:  00 11 AC FF
result: 00 11 AC FF

input:  00000000 00010001 10101100 11111111
result: 00000000 00010001 10101100 11111111
```

### AND

#### Big Endian

```c#
public static byte[] ByteArrayUtils.BitwiseAndBigEndian(byte[] left, byte[] right)
```

```c#
```

```none
BitwiseAndBigEndian Example
lhs:    C0 DE
rhs:    C0 FF EE
result: 00 C0 CE

lhs:    11000000 11011110
rhs:    11000000 11111111 11101110
result: 00000000 11000000 11001110
```

#### Little Endian

```c#
public static byte[] ByteArrayUtils.BitwiseAndLittleEndian(byte[] left, byte[] right)
```

```c#
```

```none
BitwiseAndLittleEndian Example
lhs:    C0 DE
rhs:    C0 FF EE
result: C0 DE 00

lhs:    11000000 11011110
rhs:    11000000 11111111 11101110
result: 11000000 11011110 00000000
```

### OR

#### Big Endian

```c#
public static byte[] ByteArrayUtils.BitwiseOrBigEndian(byte[] left, byte[] right)
```

```c#
```

```none
BitwiseOrBigEndian Example
lhs:    C0 DE
rhs:    C0 FF EE
result: C0 FF FE

lhs:    11000000 11011110
rhs:    11000000 11111111 11101110
result: 11000000 11111111 11111110
```

#### Little Endian

```c#
public static byte[] ByteArrayUtils.BitwiseOrLittleEndian(byte[] left, byte[] right)
```

```c#
```

```none
BitwiseOrLittleEndian Example
lhs:    C0 DE
rhs:    C0 FF EE
result: C0 FF EE

lhs:    11000000 11011110
rhs:    11000000 11111111 11101110
result: 11000000 11111111 11101110
```

### XOR

#### Big Endian

```c#
public static byte[] ByteArrayUtils.BitwiseXorBigEndian(byte[] left, byte[] right)
```

```c#
```

```none
BitwiseXorBigEndian Example
lhs:    C0 DE
rhs:    C0 FF EE
result: C0 3F 30

lhs:    11000000 11011110
rhs:    11000000 11111111 11101110
result: 11000000 00111111 00110000
```

#### Little Endian

```c#
public static byte[] ByteArrayUtils.BitwiseXorLittleEndian(byte[] left, byte[] right)
```

```c#
```

```none
BitwiseXorLittleEndian Example
lhs:    C0 DE
rhs:    C0 FF EE
result: 00 21 EE

lhs:    11000000 11011110
rhs:    11000000 11111111 11101110
result: 00000000 00100001 11101110
```

## Bitshifting

Bitshifting operations are endian agnostic.

### Shift Right

```c#
public static byte[] ByteArrayUtils.ShiftBitsRight(this byte[] bytes, int shift)
```

```c#
```

```none
ShiftBitsRight Example
shift:  5
input:  AD 0B EC 0F FE E0
result: 05 68 5F 60 7F F7

input:  10101101 00001011 11101100 00001111 11111110 11100000
result: 00000101 01101000 01011111 01100000 01111111 11110111
```

```c#
```

#### With Carry

```c#
public static byte[] ByteArrayUtils.ShiftBitsRight(this byte[] bytes, int shift, out byte[] carry)
```

```c#
```

```none
ShiftBitsRight Carry Example
input:  AD 0B EC 0F FE E0
shift:  5
result: 05 68 5F 60 7F F7
carry:  00

input:  10101101 00001011 11101100 00001111 11111110 11100000
result: 00000101 01101000 01011111 01100000 01111111 11110111
carry:  00000000
```

### Shift Left

```c#
public static byte[] ByteArrayUtils.ShiftBitsLeft(this byte[] bytes, int shift)
```

```c#
```

```none
ShiftBitsLeft Example
input:  AD 0B EC 0F FE E0
shift:  5
result: A1 7D 81 FF DC 00

input:  10101101 00001011 11101100 00001111 11111110 11100000
result: 10100001 01111101 10000001 11111111 11011100 00000000
```

```c#
```

#### With Carry

```c#
public static byte[] ByteArrayUtils.ShiftBitsLeft(this byte[] bytes, int shift, out byte[] carry)
```

```none
ShiftBitsLeft Carry Example
input:  AD 0B EC 0F FE E0
shift:  5
result: A1 7D 81 FF DC 00
carry:  15

input:  10101101 00001011 11101100 00001111 11111110 11100000
result: 10100001 01111101 10000001 11111111 11011100 00000000
carry:  00010101
```