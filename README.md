# ![Gulliver](resources/images/icon_64x64.png) Gulliver

[![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Gulliver?logo=nuget)](https://www.nuget.org/packages/Gulliver/)
[![Documentation on ReadTheDocs](https://img.shields.io/badge/Read%20the%20Docs-Gulliver-lightgrey?logo=read%20the%20docs)](https://gulliver.readthedocs.io)
![Apache 2.0 license](https://img.shields.io/github/license/sandialabs/gulliver?logo=apache)
![.NetStandard 1.3](https://img.shields.io/badge/targets-.NETStandard%201.3-5C2D91?logo=.net)

Gulliver is a C# utility package and library engineered for the manipulation of arbitrary sized byte arrays accounting for appropriate endianness and jagged byte length. Functionality includes the as previously unsupported standard set of boolean algebraic operations, bitwise shifting, and unsigned endian aware mathematical addition, subtraction, and comparison. Gulliver exist to free developers from managing byte ordering and operations at the low level as was previously required by the standard C# language distributions.

Gulliver was of course named for the titular character in "**[Gulliver's Travels](https://www.gutenberg.org/ebooks/829)**", a.k.a. "**Travels into Several Remote Nations of the World. In Four Parts. By Lemuel Gulliver, First a Surgeon, and then a Captain of Several Ships**" by Jonathan Swift, a book that the library author has admittedly not yet read but was pulled from the Computer Science zeitgeist referring to the big-endian versus little-endian nature of byte ordering.

Gulliver originally came to be for the sake of [Arcus]( https://github.com/sandialabs/arcus), a C# library for calculating, parsing, formatting, converting and comparing both IPv4 and IPv6 addresses and subnets. Inherently, by its nature, Arcus needed to do a great deal of byte manipulation. Eventually Gulliver came into a life of its own and it was decided that it should be broken off into its own library.

## Getting Started

Gulliver is available via a [NuGet](https://www.nuget.org/packages/Gulliver/). Our releases use [SemVer](http://semver.org/) for versioning.

Documentation can be found on [Gulliver's Read the Docs site](https://arcus.readthedocs.io/en/latest/).

## Usage

Bit-curious developers will likely gain the most use out of the static `ByteArrayUtils` (multi-part) class. It is filled with a plethora of static helper methods and extension methods that work with byte arrays (`byte[]`) and related types. Here in you will be able to modify and manage arbitrary byte arrays of arbitrary length.

Operations are broken down in several fundamental categories, with appropriate considerations made for the endianness of the byte array when appropriate.

- General byte array operations
  - Byte array creation and population
  - Bytes as bit array addressing
  - Byte array trimming, padding, and appending
  - MSB length counting 
  - MSB 0 value trimming
  - Stringification
- Bitwise byte array operations
  - AND / OR / XOR / NOT
  - Bitshifting
- Unsigned Mathematical Operations
  - Addition / Subtraction / Increment / Decrement 
  - Comparison


The `FixedBytes` class brings many of these operations togheter allowing developers to treat a `byte[]` as a more complex object without the need to explicitly call helper or extension methods.


The `LittleEndianByteEnumerable` and `BigEndianByteEnumerable` gives access to more cleanly treat little-endian and big-endian byte arrays as enumerables in an expected indexable manner regardless of the underlying endianness ignoring `0x00` valued most significant bytes and managing indexing of the most significant byte at the 0th index.


`ConcurrentBigEndianByteEnumerable` and `ConcurrentLittleEndianByteEnumerable` allows for ease in parallel indexing a pair of byte arrays, that may not be of the same length, in the desired endianness. This comes in particularly useful when running bitwise or mathematical operations.

## Built With

* [JetBrains.Annotations](https://www.jetbrains.com/help/resharper/10.0/Code_Analysis__Code_Annotations.html) - Used to keep developers honest
* [NuGet](https://www.nuget.org/) - Dependency Management
* [Stackoverflow](https://stackoverflow.com/) - Because who really remembers how to code
* [xUnit.net](https://xunit.net/) - Testing, testing, 1, 2, 3...

## Primary Authors and Contributors

* **Robert H. Engelhardt** - *Primary Developer, Source of Ideas Good and Bad* - [@rheone]( https://twitter.com/rheone)
* **Andrew Steele** - *Code Review and Suggestions* - [@ahsteele]( https://twitter.com/ahsteele)
* **Nick Bachicha** - *Git Wrangler and DevOps Extraordinaire* - [@nicksterx](https://twitter.com/nicksterx)

## Copyright

> Copyright 2019 National Technology & Engineering Solutions of Sandia, LLC (NTESS). Under the terms of Contract DE-NA0003525 with NTESS, the U.S. Government retains certain rights in this software.

## License

 >   Licensed under the Apache License, Version 2.0 (the "License");
 >   you may not use this file except in compliance with the License.
 >   You may obtain a copy of the License at
 >
 >       http://www.apache.org/licenses/LICENSE-2.0
 >
 >   Unless required by applicable law or agreed to in writing, software
 >   distributed under the License is distributed on an "AS IS" BASIS,
 >   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 >   See the License for the specific language governing permissions and
 >   limitations under the License.
