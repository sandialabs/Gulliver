# ![Gulliver](src/Gulliver/icon.png) Gulliver

![GitHub Actions Workflow Status](https://img.shields.io/github/actions/workflow/status/sandialabs/Gulliver/build.yml?branch=main)
[![nuget Version](https://img.shields.io/nuget/v/Gulliver)](https://www.nuget.org/packages/Gulliver)
[![GitHub Release](https://img.shields.io/github/v/release/sandialabs/Gulliver)](https://github.com/sandialabs/Gulliver/releases)
[![GitHub Tag](https://img.shields.io/github/v/tag/sandialabs/Gulliver)](https://github.com/sandialabs/Gulliver/tags)
![Targets](https://img.shields.io/badge/.NET%20Standard%202.0%20|%20.NET%208.0%20|%20.NET%209.0-blue?logo=.net)
[![Apache 2.0 License](https://img.shields.io/github/license/sandialabs/Gulliver?logo=apache)](https://github.com/sandialabs/Gulliver/blob/main/LICENSE)

## About the Project

Gulliver is a C# utility package and library engineered for the manipulation of arbitrary sized byte arrays accounting for appropriate endianness and jagged byte length. Functionality includes the as previously unsupported standard set of boolean algebraic operations, bitwise shifting, and unsigned endian aware mathematical addition, subtraction, and comparison. Gulliver exist to free developers from managing byte ordering and operations at the low level as was previously required by the standard C# language distributions.

Gulliver was of course named for the titular character in "**[Gulliver's Travels](https://www.gutenberg.org/ebooks/829)**", a.k.a. "**Travels into Several Remote Nations of the World. In Four Parts. By Lemuel Gulliver, First a Surgeon, and then a Captain of Several Ships**" by Jonathan Swift, a book that the library author has admittedly not yet read but was pulled from the Computer Science zeitgeist referring to the big-endian versus little-endian nature of byte ordering.

Gulliver originally came to be for the sake of [Arcus](https://github.com/sandialabs/arcus), a C# library for calculating, parsing, formatting, converting and comparing both IPv4 and IPv6 addresses and subnets. Inherently, by its nature, Arcus needed to do a great deal of byte manipulation. Eventually Gulliver came into a life of its own and it was decided that it should be broken off into its own library.

## Getting Started

Gulliver is available via a [NuGet](https://www.nuget.org/packages/Gulliver/).

Documentation can be found on [Gulliver's Read the Docs site](https://gulliver.readthedocs.io/en/latest/).

### Usage

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

The `FixedBytes` class brings many of these operations together allowing developers to treat a `byte[]` as a more complex object without the need to explicitly call helper or extension methods.

The `LittleEndianByteEnumerable` and `BigEndianByteEnumerable` gives access to more cleanly treat little-endian and big-endian byte arrays as enumerables in an expected indexable manner regardless of the underlying endianness ignoring `0x00` valued most significant bytes and managing indexing of the most significant byte at the 0th index.

`ConcurrentBigEndianByteEnumerable` and `ConcurrentLittleEndianByteEnumerable` allows for ease in parallel indexing a pair of byte arrays, that may not be of the same length, in the desired endianness. This comes in particularly useful when running bitwise or mathematical operations.

### Developer Notes

## Built With

This project was built with the aid of:

- [CSharpier](https://csharpier.com/)
- [dotnet-outdated](https://github.com/dotnet-outdated/dotnet-outdated)
- [Husky.Net](https://alirezanet.github.io/Husky.Net/)
- [Roslynator](https://josefpihrt.github.io/docs/roslynator/)
- [SonarAnalyzer](https://www.sonarsource.com/products/sonarlint/features/visual-studio/)
- [StyleCop.Analyzers](https://github.com/DotNetAnalyzers/StyleCopAnalyzers)
- [xUnit.net](https://xunit.net/)

### Versioning

This project uses [Semantic Versioning](https://semver.org/)

### Targeting

The project targets [.NET Standard 2.0](https://learn.microsoft.com/en-us/dotnet/standard/net-standard?tabs=net-standard-2-0), [.NET 8](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-8), and [.NET 9](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-9/overview). The test project similarly targets .NET 8, .NET 9, but targets [.NET Framework 4.8](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net48) for the .NET Standard 2.0 tests.

### Commit Hook

The project itself has a configured pre-commit git hook, via [Husky.Net](https://alirezanet.github.io/Husky.Net/) that automatically lints and formats code via [dotnet format](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-format) and [csharpier](https://csharpier.com/).

#### Disable husky in CI/CD pipelines

Per the [Husky.Net instructions](https://alirezanet.github.io/Husky.Net/guide/automate.html#disable-husky-in-ci-cd-pipelines)

> You can set the `HUSKY` environment variable to `0` in order to disable husky in CI/CD pipelines.

#### Manual Linting and Formatting

On occasion a manual run is desired it may be done so via the `src` directory and with the command

```shell
dotnet format style; dotnet format analyzers; dotnet csharpier format .
```

These commands may be called independently, but order may matter.

#### Testing

After making changes tests should be run that include all targets

## Acknowledgments

This project was built by the Production Tools Team at Sandia National Laboratories. Special thanks to all contributors and reviewers who helped shape and improve this library.

Including, but not limited to:

- **Robert H. Engelhardt** - _Primary Developer, Source of Ideas Good and Bad_ - [rheone](https://github.com/rheone)
- **Andrew Steele** - _Code Review and Suggestions_ - [ahsteele](https://github.com/ahsteele)
- **Nick Bachicha** - _Git Wrangler and DevOps Extraordinaire_ - [nicksterx](https://github.com/nicksterx)

## Copyright

> Copyright 2025 National Technology & Engineering Solutions of Sandia, LLC (NTESS). Under the terms of Contract DE-NA0003525 with NTESS, the U.S. Government retains certain rights in this software.

## License

> Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License. You may obtain a copy of the License at
>
> http://www.apache.org/licenses/LICENSE-2.0
>
> Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License.
