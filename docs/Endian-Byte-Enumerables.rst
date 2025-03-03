Endian Byte Enumerables
#######################

The ``LittleEndianByteEnumerable`` and ``BigEndianByteEnumerable`` gives access to more cleanly treat little-endian and big-endian byte arrays as enumerables in an expected indexable manner regardless of the underlying endianness ignoring ``0x00`` valued most significant bytes and managing indexing of the most significant byte at the 0th index.

.. warning:: This topic will be further expounded upon at a later date. In the meantime please feel free to browse the source code available:

   - `GitHub/Gulliver <https://github.com/sandialabs/gulliver>`_

      - `IByteEnumerable <https://github.com/sandialabs/Gulliver/blob/main/src/Gulliver/Enumerables/IByteEnumerable.cs>`_

        - `AbstractByteEnumerable <https://github.com/sandialabs/Gulliver/blob/main/src/Gulliver/Enumerables/AbstractByteEnumerable.cs>`_

           - `BigEndianByteEnumerable <https://github.com/sandialabs/Gulliver/blob/main/src/Gulliver/Enumerables/BigEndianByteEnumerable.cs>`_
           - `LittleEndianByteEnumerable <https://github.com/sandialabs/Gulliver/blob/main/src/Gulliver/Enumerables/LittleEndianByteEnumerable.cs>`_
