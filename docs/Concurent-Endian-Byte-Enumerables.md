# Concurent Endian Byte Enumerables

`ConcurrentBigEndianByteEnumerable` and `ConcurrentLittleEndianByteEnumerable` allows for ease in parallel indexing a pair of byte arrays, that may not be of the same length, in the desired endianness. This comes in particularly useful when running bitwise or mathematical operations.