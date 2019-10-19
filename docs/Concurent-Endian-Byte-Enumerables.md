# Concurent Endian Byte Enumerables

`ConcurrentBigEndianByteEnumerable` and `ConcurrentLittleEndianByteEnumerable` allows for ease in parallel indexing a pair of byte arrays, that may not be of the same length, in the desired endianness. This comes in particularly useful when running bitwise or mathematical operations.

This topic will be further expounded upon at a later date. In the mean time please feel free to browse the source code available 
  - [GitHub/Gulliver](https://github.com/sandialabs/gulliver)
    - [IConcurrentByteEnumerable](https://github.com/sandialabs/Gulliver/blob/master/src/Gulliver/Enumerables/IConcurrentByteEnumerable.cs)  
      - [AbstractConcurrentByteEnumerable](https://github.com/sandialabs/Gulliver/blob/master/src/Gulliver/Enumerables/AbstractConcurrentByteEnumerable.cs)
        - [ConcurrentBigEndianByteEnumerable](https://github.com/sandialabs/Gulliver/blob/master/src/Gulliver/Enumerables/ConcurrentBigEndianByteEnumerable.cs)
        - [ConcurrentLittleEndianByteEnumerable](https://github.com/sandialabs/Gulliver/blob/master/src/Gulliver/Enumerables/ConcurrentLittleEndianByteEnumerable.cs)