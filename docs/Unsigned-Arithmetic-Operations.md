# Unsigned Arithmetic Operations

For now arithmetic operations within Gulliver have been limited to those involving the treatment of byte arrays as unsigned integer values. Negative numbers and floating point values are out of scope for the needs of this library at the moment.

Unless otherwise stated the following methods are statically defined in the `ByteArrayUtils` class and do not modify their input.

## Addition 

Both the `ByteArrayUtils.AddUnsignedBigEndian` and `ByteArrayUtils.AddUnsignedLittleEndian` methods return the addition result of the provided `right` and `left` byte arrays accounting for the appropriate endiness of the input.

### Big Endian

```c#
public static byte[] ByteArrayUtils.AddUnsignedBigEndian(byte[] right, byte[] left)
```

```c#
public static void AddUnsignedBigEndianExample()
{
    // Setup
    var lhs = new byte[] { 0xAD, 0xDE, 0xD0 };
    var rhs = new byte[] { 0xC0, 0xDE };
    // Act
    var result = ByteArrayUtils.AddUnsignedBigEndian(lhs, rhs);
    // Conclusion
    Console.WriteLine("AddUnsignedBigEndian Example");
    Console.WriteLine($"lhs:\t{lhs.ToString("H")}");
    Console.WriteLine($"rhs:\t{rhs.ToString("H")}");
    Console.WriteLine($"result:\t{result.ToString("H")}");
    Console.WriteLine(string.Empty);
    Console.WriteLine($"lhs:\t{lhs.ToString("IBE")}");
    Console.WriteLine($"rhs:\t{rhs.ToString("IBE")}");
    Console.WriteLine($"result:\t{result.ToString("IBE")}");
    Console.WriteLine(string.Empty);
}
```

```none
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
public static void AddUnsignedLittleEndianExample()
{
    // Setup
    var lhs = new byte[] { 0xAD, 0xDE, 0xD0 };
    var rhs = new byte[] { 0xC0, 0xDE };
    // Act
    var result = ByteArrayUtils.AddUnsignedLittleEndian(lhs, rhs);
    // Conclusion
    Console.WriteLine("AddUnsignedLittleEndian Example");
    Console.WriteLine($"lhs:\t{lhs.ToString("H")}");
    Console.WriteLine($"rhs:\t{rhs.ToString("H")}");
    Console.WriteLine($"result:\t{result.ToString("H")}");
    Console.WriteLine(string.Empty);
    Console.WriteLine($"lhs:\t{lhs.ToString("ILE")}");
    Console.WriteLine($"rhs:\t{rhs.ToString("ILE")}");
    Console.WriteLine($"result:\t{result.ToString("ILE")}");
    Console.WriteLine(string.Empty);
}
```

```none
AddUnsignedLittleEndian Example
lhs:    AD DE D0
rhs:    C0 DE
result: 6D BD D1

lhs:    13688493
rhs:    57024
result: 13745517
```

## Subtraction

Both the `ByteArrayUtils.SubtractUnsignedBigEndian` and `ByteArrayUtils.SubtractUnsignedLittleEndian` methods return the subtraction result of the provided `right` (minuend) and `left` (subtrahend) byte arrays accounting for the appropriate endiness of the input.

If the operation would result in a negative value, given we're only dealing with unsigned integer operations, the execution will throw an `InvalidOperationException`. 

### Big Endian

```c#
public static byte[] ByteArrayUtils.SubtractUnsignedBigEndian(byte[] left, byte[] right)
```

```c#
public static void SubtractUnsignedBigEndianExample()
{
    // Setup
    var lhs = new byte[] { 0xDE, 0x1E, 0x7E, 0xD0 };
    var rhs = new byte[] { 0xC0, 0xDE };
    // Act
    var result = ByteArrayUtils.SubtractUnsignedBigEndian(lhs, rhs);
    // Conclusion
    Console.WriteLine("SubtractUnsignedBigEndian Example");
    Console.WriteLine($"lhs:\t{lhs.ToString("H")}");
    Console.WriteLine($"rhs:\t{rhs.ToString("H")}");
    Console.WriteLine($"result:\t{result.ToString("H")}");
    Console.WriteLine(string.Empty);
    Console.WriteLine($"lhs:\t{lhs.ToString("IBE")}");
    Console.WriteLine($"rhs:\t{rhs.ToString("IBE")}");
    Console.WriteLine($"result:\t{result.ToString("IBE")}");
    Console.WriteLine(string.Empty);
}
```

```none
SubtractUnsignedBigEndian Example
lhs:    DE 1E 7E D0
rhs:    C0 DE
result: DE 1D BD F2

lhs:    3726540496
rhs:    49374
result: 3726491122
```

### Little Endian

```c#
public static byte[] ByteArrayUtils.SubtractUnsignedLittleEndian(byte[] left, byte[] right)
```

```c#
public static void SubtractUnsignedLittleEndianExample()
{
    // Setup
    var lhs = new byte[] { 0xDE, 0x1E, 0x7E, 0xD0 };
    var rhs = new byte[] { 0xC0, 0xDE };
    // Act
    var result = ByteArrayUtils.SubtractUnsignedLittleEndian(lhs, rhs);
    // Conclusion
    Console.WriteLine("SubtractUnsignedLittleEndian Example");
    Console.WriteLine($"lhs:\t{lhs.ToString("H")}");
    Console.WriteLine($"rhs:\t{rhs.ToString("H")}");
    Console.WriteLine($"result:\t{result.ToString("H")}");
    Console.WriteLine(string.Empty);
    Console.WriteLine($"lhs:\t{lhs.ToString("ILE")}");
    Console.WriteLine($"rhs:\t{rhs.ToString("ILE")}");
    Console.WriteLine($"result:\t{result.ToString("ILE")}");
    Console.WriteLine(string.Empty);
```

```none
SubtractUnsignedLittleEndian Example
lhs:    DE 1E 7E D0
rhs:    C0 DE
result: 1E 40 7D D0

lhs:    3497926366
rhs:    57024
result: 3497869342
```

## Safe Summation

Safe summation allows for the the safe addition or subtraction of a `long` values `delta` from the given `source` byte array input. This is useful for iterating or decrementing byte arrays. 

Both the `ByteArrayUtils.TrySumBigEndian` and `ByteArrayUtils.TrySumLittleEndian` methods return a `bool` stating if the operation was successful, and will out a non-null value of `result` on success. 

### Big Endian

```c#
public static bool ByteArrayUtils.TrySumBigEndian(byte[] source, long delta, out byte[] result)
```
```c#
public static void TrySumBigEndianExample()
{
    // Setup
    var bytes = new byte[] { 0xAD, 0xDE, 0xD0 };
    const long delta = 42L;
    // Act
    var success = ByteArrayUtils.TrySumBigEndian(bytes, delta, out var result);
    // Conclusion
    Console.WriteLine("TrySumBigEndian Example");
    Console.WriteLine($"bytes:\t{bytes.ToString("H")}");
    Console.WriteLine($"delta:\t{delta}");
    Console.WriteLine($"success:\t{success}");
    Console.WriteLine($"result:\t{result.ToString("H")}");
    Console.WriteLine(string.Empty);
    Console.WriteLine($"bytes:\t{bytes.ToString("IBE")}");
    Console.WriteLine($"result:\t{result.ToString("IBE")}");
    Console.WriteLine(string.Empty);
}
```

```none
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
public static void TrySumLittleEndianExample()
{
    // Setup
    var bytes = new byte[] { 0xAD, 0xDE, 0xD0 };
    const long delta = 42L;
    // Act
    var success = ByteArrayUtils.TrySumLittleEndian(bytes, delta, out var result);
    // Conclusion
    Console.WriteLine("TryLittleEndian Subtraction Example");
    Console.WriteLine($"bytes:\t{bytes.ToString("H")}");
    Console.WriteLine($"delta:\t{delta}");
    Console.WriteLine($"success:\t{success}");
    Console.WriteLine($"result:\t{result.ToString("H")}");
    Console.WriteLine(string.Empty);
    Console.WriteLine($"bytes:\t{bytes.ToString("ILE")}");
    Console.WriteLine($"result:\t{result.ToString("ILE")}");
    Console.WriteLine(string.Empty);
}
```

```none
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

It is often not enough to simply compare the lengths of two arbitrary byte arrays to determine the equality or the largest / smallest unsigned integer value encoded as bytes.

Both `ByteArrayUtils.CompareUnsignedBigEndian` and `ByteArrayUtils.CompareUnsignedLittleEndian` provide the ability to easily compare byte arrays as their unsigned integer values.

The result is similar to that of `IComparer.Compare(left, right)`. The signed integer indicates the relative values of `left` and `right`:
- If 0, `left` equals `right`
- If less than 0, `left` is less than 'right'
- If greater than 0, `right` is greater than `left`

### Big Endian

```c#
public static int ByteArrayUtils.CompareUnsignedBigEndian(byte[] left, byte[] right)
```

```c#
public static void CompareUnsignedBigEndianExample()
{
    // Setup
    var lhs = new byte[] { 0xB1, 0x66, 0xE5, 0x70 };
    var rhs = new byte[] { 0x5A, 0x11 };
    // Act
    var result = ByteArrayUtils.CompareUnsignedBigEndian(lhs, rhs);
    // Conclusion
    Console.WriteLine("CompareUnsignedBigEndian Example");
    Console.WriteLine($"lhs:\t{lhs.ToString("H")}");
    Console.WriteLine($"rhs:\t{rhs.ToString("H")}");
    Console.WriteLine($"result:\t{result}");
    Console.WriteLine(string.Empty);
    Console.WriteLine($"lhs:\t{lhs.ToString("IBE")}");
    Console.WriteLine($"rhs:\t{rhs.ToString("IBE")}");
    Console.WriteLine($"result:\t{result}");
    Console.WriteLine(string.Empty);
}
```

```none
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
public static void CompareUnsignedLittleEndianExample()
{
    // Setup
    var lhs = new byte[] { 0xB1, 0x66, 0xE5, 0x70 };
    var rhs = new byte[] { 0x5A, 0x11 };
    // Act
    var result = ByteArrayUtils.CompareUnsignedLittleEndian(lhs, rhs);
    // Conclusion
    Console.WriteLine("CompareUnsignedLittleEndian Example");
    Console.WriteLine($"lhs:\t{lhs.ToString("H")}");
    Console.WriteLine($"rhs:\t{rhs.ToString("H")}");
    Console.WriteLine($"result:\t{result}");
    Console.WriteLine(string.Empty);
    Console.WriteLine($"lhs:\t{lhs.ToString("ILE")}");
    Console.WriteLine($"rhs:\t{rhs.ToString("ILE")}");
    Console.WriteLine($"result:\t{result}");
    Console.WriteLine(string.Empty);
}
```

```none
CompareUnsignedLittleEndian Example
lhs:    B1 66 E5 70
rhs:    5A 11
result: 1

lhs:    1894082225
rhs:    4442
result: 1
```
