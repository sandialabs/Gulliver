Concurrent Endian Byte Enumerables
##################################

``ConcurrentBigEndianByteEnumerable`` and ``ConcurrentLittleEndianByteEnumerable`` allows for ease in parallel indexing a pair of byte arrays, that may not be of the same length, in the desired endianness. This comes in particularly useful when running bitwise or mathematical operations.

.. warning:: This topic will be further expounded upon at a later date. In the meantime please feel free to browse the source code available:

   - `GitHub/Gulliver <https://github.com/sandialabs/gulliver>`_

      - `IConcurrentByteEnumerable <https://github.com/sandialabs/Gulliver/blob/main/src/Gulliver/Enumerables/IConcurrentByteEnumerable.cs>`_

         - `AbstractConcurrentByteEnumerable <https://github.com/sandialabs/Gulliver/blob/main/src/Gulliver/Enumerables/AbstractConcurrentByteEnumerable.cs>`_

            - `ConcurrentBigEndianByteEnumerable <https://github.com/sandialabs/Gulliver/blob/main/src/Gulliver/Enumerables/ConcurrentBigEndianByteEnumerable.cs>`_
            - `ConcurrentLittleEndianByteEnumerable <https://github.com/sandialabs/Gulliver/blob/main/src/Gulliver/Enumerables/ConcurrentLittleEndianByteEnumerable.cs>`_
