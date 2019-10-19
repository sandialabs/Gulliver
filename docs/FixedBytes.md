# FixedBytes

The `FixedBytes` class brings many of these operations together allowing developers to treat a `byte[]` as a more complex object without the need to explicitly call helper or extension methods. It acts as a wrapper around an array of bytes in BigEndian byte order.

`FixedBytes` implements 
 - `IFormattable,`
 - `IReadOnlyCollection<byte>`
 - `IEquatable<FixedBytes>`
 - `IEquatable<IEnumerable<byte>>`
 - `IComparable<FixedBytes>`
 - `IComparable<IEnumerable<byte>>`
 - `IComparable`

`FixedBytes` Overloads the operators
 - For bitwise operations
   - `|` - OR
   - `&` - AND
   - `^` - XOR
   - `!` - NOT
   - `<<` - Shift Left
   - `>>` - Shift Right
- Mathematical Operations (Unsigned by defined endianness)
  - `+` - Addition
  - `-` - Subtraction
  - `>` - Greater Than
  - `<` - Less Than
  - `>=` - Greater Than or Equal
  - `<=` - Less Than or Equal
  - `==` - Equals
  - `!=` - Not Equals
- Explicit Conversion (cast from)
  - `byte[]`
  - `List<byte>`
- Implicit Conversion (cast to)
  - `byte[]`

This topic will be further expounded upon at a later date. In the mean time please feel free to browse the source code available 
  - [GitHub/Gulliver](https://github.com/sandialabs/gulliver)
    - [FixedBytes](https://github.com/sandialabs/Gulliver/blob/master/src/Gulliver/FixedBytes.cs)