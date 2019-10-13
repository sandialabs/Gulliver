Gulliver
========
Gulliver is a C# utility package and library engineered for the manipulation of arbitrary sized byte arrays accounting for appropriate endianness and jagged byte length. Functionality includes the as previously unsupported standard set of boolean algebraic operations, bitwise shifting, and unsigned endian aware mathematical addition, subtraction, and comparison. Gulliver exist to free developers from managing byte ordering and operations at the low level as was previously required by the standard C# language distributions.

Gulliver was of course named for the titular character in `Gulliver's Travels <https://www.gutenberg.org/ebooks/829>`_, a.k.a. "**Travels into Several Remote Nations of the World. In Four Parts. By Lemuel Gulliver, First a Surgeon, and then a Captain of Several Ships**" by Jonathan Swift, a book that the library author has admittedly not yet read but was pulled from the Computer Science zeitgeist referring to the big-endian versus little-endian nature of byte ordering.

Gulliver originally came to be for the sake of `Arcus <https://github.com/sandialabs/arcus>`_, a C# library for calculating, parsing, formatting, converting and comparing both IPv4 and IPv6 addresses and subnets. Inherently, by its nature, Arcus needed to do a great deal of byte manipulation. Eventually Gulliver came into a life of its own and it was decided that it should be broken off into its own library.


.. toctree::
   :maxdepth: 2
   :caption: Getting Started

   What-is-Endianness

.. toctree::
   :maxdepth: 2
   :caption: Features & Functionality

   General-Operations
   Bitwise-Byte-Array-Operations
   Unsigned-Mathematical-Operations
   Endian-Byte-Enumerables
   Concurent-Endian-Byte-Enumerables
   FixedBytes

Contribute
----------

- `Issue Tracker <https://github.com/sandialabs/Gulliver/issues>`_
- `Source Code <https://github.com/sandialabs/Gulliver>`_