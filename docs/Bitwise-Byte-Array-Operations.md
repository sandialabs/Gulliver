# Bitwise Byte Array Operations

The various Bitwise byte array operations provided by Gulliver implement the standard expected bitwise operations that should fit the needs of most developers. In some cases these methods are endian aware such that byte arrays of differing lengths may be appropriately lined up for operations.

Unless otherwise stated the following methods are statically defined in the `ByteArrayUtils` class and do not modify their input.

## Addressing

The various addressing methods allow for the easy retrieval of individual byte, or bit data within the given byte array.

### Byte Array as Bit Array

Byte arrays are great, but sometimes what we really need are bit (or `boolean`) arrays instead. `ByteArrayUtils.AddressBit` method takes an array of `bytes` treats it as if it were an array of bits instead returning the bit value at the given bit index `index`.

Keep in mind there are 8 bit in a byte.

```c#
public static bool ByteArrayUtils.AddressBit(this byte[] bytes, int index)
```

```c#
public static class Addressing
{
    #region AddressBit
    public static void AddressBitExample()
    {
        // Setup
        var input = new byte[] { 0xC0, 0x1D };
        var bitLength = input.Length * 8;
        // Act
        IEnumerable<string> result = Enumerable.Range(0, bitLength - 1)
                                                .Select(i =>
                                                {
                                                    var bit = input.AddressBit(i);
                                                    return (i, b: bit ? 1 : 0);
                                                })
                                                .Select(x => $"[{x.i}]:{x.b}")
                                                .Skip(4)
                                                .Take(bitLength - 8)
                                                .ToList();
        // Conclusion
        Console.WriteLine("AddressBit Example");
        Console.WriteLine($"input:\t{input.ToString("H")}");
        Console.WriteLine($"input:\t{input.ToString("b")}");
        Console.WriteLine($"result:\t{string.Join(", ", result)}");
        Console.WriteLine(string.Empty);
    }
    #endregion end: AddressBit
}
```

```none
AddressBit Example
input:  C0 1D
input:  11000000 00011101
result: [4]:0, [5]:0, [6]:1, [7]:1, [8]:1, [9]:0, [10]:1, [11]:1
```

## Boolean Operations

Boolean operations include the standard NOT, AND, OR, and XOR.

XNOR, and the remaining 11 truth functions were deemed unnecessary. But remember, as an exercise for the developer, a XNOR may be created by using a NOT on the result of an OR operation, and given the principal of [Functional Completeness](https://en.wikipedia.org/wiki/Functional_completeness) each of the [16 truth functions](https://en.wikipedia.org/wiki/Truth_table#Binary_operations) can be built using your newly created gate. 

### NOT

`ByteArrayUtils.BitwiseNot` will return the inverse of the provided `bytes`

Due to its unary nature the `ByteArrayUtils.BitwiseNot` operation is endian agnostic.

```c#
public static byte[] ByteArrayUtils.BitwiseNot(byte[] bytes)
```

```c#
public static void BitwiseNotExample()
{
    // Setup
    var input = new byte[] { 0x00, 0x11, 0xAC, 0xFF };
    // Act
    var result = ByteArrayUtils.BitwiseNot(input);
    // Conclusion
    Console.WriteLine("BitwiseNot Example");
    Console.WriteLine($"input:\t{input.ToString("H")}");
    Console.WriteLine($"result:\t{result.ToString("H")}");
    Console.WriteLine(string.Empty);
    Console.WriteLine($"input:\t{input.ToString("b")}");
    Console.WriteLine($"result:\t{result.ToString("b")}");
    Console.WriteLine(string.Empty);
}
```

```none
BitwiseNot Example
input:  00 11 AC FF
result: FF EE 53 00

input:  00000000 00010001 10101100 11111111
result: 11111111 11101110 01010011 00000000
```

### AND

`ByteArrayUtils.BitwiseAndBigEndian` and `ByteArrayUtils.BitwiseAndLittleEndian` will return the logical AND of the `left` and `right` byte arrays. In the case where the input byte arrays are not of the same length the shortest array will be padded by the appropriate count of `0x00` most significant bytes so that comparisons may appropriately take place.

#### Big Endian

```c#
public static byte[] ByteArrayUtils.BitwiseAndBigEndian(byte[] left, byte[] right)
```

```c#
public static void BitwiseAndBigEndianExample()
{
    // Setup
    var lhs = new byte[] { 0xC0, 0xDE };
    var rhs = new byte[] { 0xC0, 0xFF, 0xEE };
    // Act
    var result = ByteArrayUtils.BitwiseAndBigEndian(lhs, rhs);
    // Conclusion
    Console.WriteLine("BitwiseAndBigEndian Example");
    Console.WriteLine($"lhs:\t{lhs.ToString("H")}");
    Console.WriteLine($"rhs:\t{rhs.ToString("H")}");
    Console.WriteLine($"result:\t{result.ToString("H")}");
    Console.WriteLine(string.Empty);
    Console.WriteLine($"lhs:\t{lhs.ToString("b")}");
    Console.WriteLine($"rhs:\t{rhs.ToString("b")}");
    Console.WriteLine($"result:\t{result.ToString("b")}");
    Console.WriteLine(string.Empty);
}
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
public static void BitwiseAndLittleEndianExample()
{
    // Setup
    var lhs = new byte[] { 0xC0, 0xDE };
    var rhs = new byte[] { 0xC0, 0xFF, 0xEE };
    // Act
    var result = ByteArrayUtils.BitwiseAndLittleEndian(lhs, rhs);
    // Conclusion
    Console.WriteLine("BitwiseAndLittleEndian Example");
    Console.WriteLine($"lhs:\t{lhs.ToString("H")}");
    Console.WriteLine($"rhs:\t{rhs.ToString("H")}");
    Console.WriteLine($"result:\t{result.ToString("H")}");
    Console.WriteLine(string.Empty);
    Console.WriteLine($"lhs:\t{lhs.ToString("b")}");
    Console.WriteLine($"rhs:\t{rhs.ToString("b")}");
    Console.WriteLine($"result:\t{result.ToString("b")}");
    Console.WriteLine(string.Empty);
}
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

`ByteArrayUtils.BitwiseOrBigEndian` and `ByteArrayUtils.BitwiseOrLittleEndian`will return the logical OR of the `left` and `right` byte arrays. In the case where the input byte arrays are not of the same length the shortest array will be padded by the appropriate count of `0x00` most significant bytes so that comparisons may appropriately take place.

#### Big Endian

```c#
public static byte[] ByteArrayUtils.BitwiseOrBigEndian(byte[] left, byte[] right)
```

```c#
public static void BitwiseOrBigEndianExample()
{
    // Setup
    var lhs = new byte[] { 0xC0, 0xDE };
    var rhs = new byte[] { 0xC0, 0xFF, 0xEE };
    // Act
    var result = ByteArrayUtils.BitwiseOrBigEndian(lhs, rhs);
    // Conclusion
    Console.WriteLine("BitwiseOrBigEndian Example");
    Console.WriteLine($"lhs:\t{lhs.ToString("H")}");
    Console.WriteLine($"rhs:\t{rhs.ToString("H")}");
    Console.WriteLine($"result:\t{result.ToString("H")}");
    Console.WriteLine(string.Empty);
    Console.WriteLine($"lhs:\t{lhs.ToString("b")}");
    Console.WriteLine($"rhs:\t{rhs.ToString("b")}");
    Console.WriteLine($"result:\t{result.ToString("b")}");
    Console.WriteLine(string.Empty);
}
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
public static void BitwiseOrLittleEndianExample()
{
    // Setup
    var lhs = new byte[] { 0xC0, 0xDE };
    var rhs = new byte[] { 0xC0, 0xFF, 0xEE };
    // Act
    var result = ByteArrayUtils.BitwiseOrLittleEndian(lhs, rhs);
    // Conclusion
    Console.WriteLine("BitwiseOrLittleEndian Example");
    Console.WriteLine($"lhs:\t{lhs.ToString("H")}");
    Console.WriteLine($"rhs:\t{rhs.ToString("H")}");
    Console.WriteLine($"result:\t{result.ToString("H")}");
    Console.WriteLine(string.Empty);
    Console.WriteLine($"lhs:\t{lhs.ToString("b")}");
    Console.WriteLine($"rhs:\t{rhs.ToString("b")}");
    Console.WriteLine($"result:\t{result.ToString("b")}");
    Console.WriteLine(string.Empty);
}
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

`ByteArrayUtils.BitwiseXorBigEndian` and `ByteArrayUtils.BitwiseXorLittleEndian` will return the logical Exclusive Or of the `left` and `right` byte arrays. In the case where the input byte arrays are not of the same length the shortest array will be padded by the appropriate count of `0x00` most significant bytes so that comparisons may appropriately take place.

#### Big Endian

```c#
public static byte[] ByteArrayUtils.BitwiseXorBigEndian(byte[] left, byte[] right)
```

```c#
public static void BitwiseXorBigEndianExample()
{
    // Setup
    var lhs = new byte[] { 0xC0, 0xDE };
    var rhs = new byte[] { 0xC0, 0xFF, 0xEE };
    // Act
    var result = ByteArrayUtils.BitwiseXorBigEndian(lhs, rhs);
    // Conclusion
    Console.WriteLine("BitwiseXorBigEndian Example");
    Console.WriteLine($"lhs:\t{lhs.ToString("H")}");
    Console.WriteLine($"rhs:\t{rhs.ToString("H")}");
    Console.WriteLine($"result:\t{result.ToString("H")}");
    Console.WriteLine(string.Empty);
    Console.WriteLine($"lhs:\t{lhs.ToString("b")}");
    Console.WriteLine($"rhs:\t{rhs.ToString("b")}");
    Console.WriteLine($"result:\t{result.ToString("b")}");
    Console.WriteLine(string.Empty);
}
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
public static void BitwiseXorLittleEndianExample()
{
    // Setup
    var lhs = new byte[] { 0xC0, 0xDE };
    var rhs = new byte[] { 0xC0, 0xFF, 0xEE };
    // Act
    var result = ByteArrayUtils.BitwiseXorLittleEndian(lhs, rhs);
    // Conclusion
    Console.WriteLine("BitwiseXorLittleEndian Example");
    Console.WriteLine($"lhs:\t{lhs.ToString("H")}");
    Console.WriteLine($"rhs:\t{rhs.ToString("H")}");
    Console.WriteLine($"result:\t{result.ToString("H")}");
    Console.WriteLine(string.Empty);
    Console.WriteLine($"lhs:\t{lhs.ToString("b")}");
    Console.WriteLine($"rhs:\t{rhs.ToString("b")}");
    Console.WriteLine($"result:\t{result.ToString("b")}");
    Console.WriteLine(string.Empty);
}
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

Bitshifting allows for the shifting of the underlying bit values of bytes in the desired direction. 

Bitshifting operations are endian agnostic.

### Shift Right

`ByteArrayUtils.ShiftBitsRight` is an arithmetic bit shift that returns the value of `bytes` with its underlying bits shifted `shift` indexes to the right. If the `carry` value is desired there exists an overload, shown below, that outs the result.

```c#
public static byte[] ByteArrayUtils.ShiftBitsRight(this byte[] bytes, int shift)
```

```c#
public static void ShiftBitsRightExample()
{
    // Setup
    var input = new byte[] { 0xAD, 0x0B, 0xEC, 0x0F, 0xFE, 0xE0 };
    const int shift = 5;

    // Act
    var result = input.ShiftBitsRight(shift);

    // Conclusion
    Console.WriteLine("ShiftBitsRight Example");
    Console.WriteLine($"shift:\t{shift}");
    Console.WriteLine($"input:\t{input.ToString("H")}");
    Console.WriteLine($"result:\t{result.ToString("H")}");
    Console.WriteLine(string.Empty);
    Console.WriteLine($"input:\t{input.ToString("b")}");
    Console.WriteLine($"result:\t{result.ToString("b")}");
    Console.WriteLine(string.Empty);
}

```


```none
ShiftBitsRight Example
shift:  5
input:  AD 0B EC 0F FE E0
result: 05 68 5F 60 7F F7

input:  10101101 00001011 11101100 00001111 11111110 11100000
result: 00000101 01101000 01011111 01100000 01111111 11110111
```

#### With Carry

An overload to the above `ByteArrayUtils.ShiftBitsRight` that provides the `carry` result of the operation.

```c#
public static byte[] ByteArrayUtils.ShiftBitsRight(this byte[] bytes, int shift, out byte[] carry)
```

```c#
public static void ShiftBitsRightCarryExample()
{
    // Setup
    var input = new byte[] { 0xAD, 0x0B, 0xEC, 0x0F, 0xFE, 0xE0 };
    const int shift = 5;

    // Act
    var result = input.ShiftBitsRight(shift, out var carry);

    // Conclusion
    Console.WriteLine("ShiftBitsRight Carry Example");
    Console.WriteLine($"input:\t{input.ToString("H")}");
    Console.WriteLine($"shift:\t{shift}");
    Console.WriteLine($"result:\t{result.ToString("H")}");
    Console.WriteLine($"carry:\t{carry.ToString("H")}");
    Console.WriteLine(string.Empty);
    Console.WriteLine($"input:\t{input.ToString("b")}");
    Console.WriteLine($"result:\t{result.ToString("b")}");
    Console.WriteLine($"carry:\t{carry.ToString("b")}");
    Console.WriteLine(string.Empty);
}
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

`ByteArrayUtils.ShiftBitsLeft` is an arithmetic bit shift that returns the value of `bytes` with its underlying bits shifted `shift` indexes to the left. If the `carry` value is desired there exists an overload, shown below, that outs the result.

```c#
public static byte[] ByteArrayUtils.ShiftBitsLeft(this byte[] bytes, int shift)
```

```c#
public static void ShiftBitsLeftExample()
{
    // Setup
    var input = new byte[] { 0xAD, 0x0B, 0xEC, 0x0F, 0xFE, 0xE0 };
    const int shift = 5;
    // Act
    var result = input.ShiftBitsLeft(shift);
    // Conclusion
    Console.WriteLine("ShiftBitsLeft Example");
    Console.WriteLine($"input:\t{input.ToString("H")}");
    Console.WriteLine($"shift:\t{shift}");
    Console.WriteLine($"result:\t{result.ToString("H")}");
    Console.WriteLine(string.Empty);
    Console.WriteLine($"input:\t{input.ToString("b")}");
    Console.WriteLine($"result:\t{result.ToString("b")}");
    Console.WriteLine(string.Empty);
}
```

```none
ShiftBitsLeft Example
input:  AD 0B EC 0F FE E0
shift:  5
result: A1 7D 81 FF DC 00

input:  10101101 00001011 11101100 00001111 11111110 11100000
result: 10100001 01111101 10000001 11111111 11011100 00000000
```

#### With Carry

An overload to the above `ByteArrayUtils.ShiftBitsLeft` that provides the `carry` result of the operation.

```c#
public static byte[] ByteArrayUtils.ShiftBitsLeft(this byte[] bytes, int shift, out byte[] carry)
```

```c#
public static void ShiftBitsLeftCarryExample()
{
    // Setup
    var input = new byte[] { 0xAD, 0x0B, 0xEC, 0x0F, 0xFE, 0xE0 };
    const int shift = 5;
    // Act
    var result = input.ShiftBitsLeft(shift, out var carry);
    // Conclusion
    Console.WriteLine("ShiftBitsLeft Carry Example");
    Console.WriteLine($"input:\t{input.ToString("H")}");
    Console.WriteLine($"shift:\t{shift}");
    Console.WriteLine($"result:\t{result.ToString("H")}");
    Console.WriteLine($"carry:\t{carry.ToString("H")}");
    Console.WriteLine(string.Empty);
    Console.WriteLine($"input:\t{input.ToString("b")}");
    Console.WriteLine($"result:\t{result.ToString("b")}");
    Console.WriteLine($"carry:\t{carry.ToString("b")}");
    Console.WriteLine(string.Empty);
}
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
