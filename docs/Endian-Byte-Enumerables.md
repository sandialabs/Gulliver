# Endian Byte Enumerables

The `LittleEndianByteEnumerable` and `BigEndianByteEnumerable` gives access to more cleanly treat little-endian and big-endian byte arrays as enumerables in an expected indexable manner regardless of the underlying endianness ignoring `0x00` valued most significant bytes and managing indexing of the most significant byte at the 0th index.