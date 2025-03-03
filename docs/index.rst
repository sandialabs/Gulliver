Gulliver
########

.. image:: https://img.shields.io/github/actions/workflow/status/sandialabs/Gulliver/build.yml?branch=main)
   :alt: GitHub Actions Workflow Status
.. image:: https://img.shields.io/nuget/v/Gulliver
   :alt: nuget Version
   :target: https://www.nuget.org/packages/Gulliver/
.. image:: https://img.shields.io/github/v/release/sandialabs/Gulliver
   :alt: GitHub Release
   :target: https://github.com/sandialabs/Gulliver/releases
.. image:: https://img.shields.io/github/v/tag/sandialabs/Gulliver
   :alt: GitHub Tag
   :target: https://github.com/sandialabs/Gulliver/tags
.. image:: https://img.shields.io/badge/.NET%20Standard%202.0%20|%20.NET%208.0%20|%20.NET%209.0-blue?logo=.net
   :alt: Targets .NET Standard 2.0, .NET 8, and .NET 9
.. image:: https://img.shields.io/github/license/sandialabs/Gulliver?logo=apache
   :alt: Apache 2.0 License
   :target: https://github.com/sandialabs/Gulliver/blob/main/LICENSE

Gulliver is a C# utility package and library engineered for the manipulation of arbitrary sized byte arrays accounting for appropriate endianness and jagged byte length. Functionality includes the as previously unsupported in .NET standard set of boolean algebraic operations, bitwise shifting, and unsigned endian aware mathematical addition, subtraction, and comparison. Gulliver exists to free developers from managing byte ordering and operations at the low level as was previously required by the standard C# language distributions.

Gulliver was of course named for the titular character in `Gulliver's Travels <https://www.gutenberg.org/ebooks/829>`_, a.k.a. "**Travels into Several Remote Nations of the World. In Four Parts. By Lemuel Gulliver, First a Surgeon, and then a Captain of Several Ships**" by Jonathan Swift, a book that the library author has admittedly not yet read but was pulled from the Computer Science zeitgeist referring to the big-endian versus little-endian nature of byte ordering.

Gulliver originally came to be for the sake of `Arcus <https://github.com/sandialabs/arcus>`_, a C# library for calculating, parsing, formatting, converting and comparing both IPv4 and IPv6 addresses and subnets. Inherently, by its nature, Arcus needed to do a great deal of byte manipulation. Eventually Gulliver came into a life of its own and it was decided that it should be broken off into its own library.

The API surfaced by Gulliver is not outwardly complex, and the intention is to keep the most common functionality as simple as possible. That simplicity is Gulliver’s great strength. Each simple block can be put together to achieve more complex and powerful functionality. We did it so other developers didn’t have to. Of note, while we don’t believe Gulliver is inefficient, it is certainly not necessarily the most efficient code solution depending on the task at hand. We opted for readability and understanding when developing Gulliver, and elected not to write code that was enigmatic for the sake of efficiency. For example we decided check for invalid method input in an overly optimistic defensive manner, chose enumerable abstractions for the ease of iteration, and chose not to work with C# spans as the added speed would cost us compatibility for older language versions.

.. toctree::
   :maxdepth: 2
   :caption: Getting Started

   What-is-Endianness
   FAQ

.. toctree::
   :maxdepth: 2
   :caption: Features & Functionality

   General-Operations
   Stringification
   Bitwise-Byte-Array-Operations
   Unsigned-Arithmetic-Operations
   Endian-Byte-Enumerables
   Concurent-Endian-Byte-Enumerables
   FixedBytes

.. toctree::
   :maxdepth: 2
   :caption: Development

   Community
   Building
   Acknowledgements
