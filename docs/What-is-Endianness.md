# What is Endianness

Endianness, at least as far as computing is concerned, is the ordering of bytes within a binary representation of data. The most common representations of endianness are [Big-Endian](#Big-Endian) and [Little-Endian](#Little-Endian). There exist other forms, such as Middle-Endian, but those are beyond the scope of this document.

## Entomology

The Computer Science terms Big-Endian and Little-Endian were introduced by [Danny Cohen]( https://en.wikipedia.org/wiki/Danny_Cohen_(computer_scientist)) in 1980. The key term *endian* has its roots in the novel [Gulliver’s Travels]( https://en.wikipedia.org/wiki/Gulliver%27s_Travels) by [Jonathan Swift]( https://en.wikipedia.org/wiki/Jonathan_Swift) where within a war occurs between two factions who are fighting over which end of a boiled egg should be opened for eating. The big end or the little end. Unsurprisingly, the same said book was the inspiration for the naming of the [Gulliver]( https://github.com/sandialabs/gulliver) library. 

## Big-Endian 

In Big-Endian, often referred to as *Network Byte Order*, ordering is left-to-right. Given the representation of an unsigned number in bytes the further to the left that a byte exists the more significant it is.

For example, the decimal value of the unsigned integer `6060842` may be represented as `0x5C7B2A` in hexadecimal. This hexadecimal value is composed of the three bytes `0x5C`, `0x7B`, and `0x28`. As such the value `6060842` may be represented in Big-Endian as a byte stream of `[0x5C, 0x7B, 0x2A]`. 

Big-Endian integer representation likely comes as second nature to developers familiar with right-to-left [Arabic numerals]( https://en.wikipedia.org/wiki/Arabic_numerals) representation 

## Little-Endian

In Little-Endian ordering is right-to-left. Given the representation of an unsigned number in bytes the further to the right a byte exists the more significant it is.

For example, the decimal value of the unsigned integer `8675309` may be represented as `0x85BF7D` in hexadecimal. This hexadecimal value is composed of the three bytes `0x85`, `0xBF`, and `0x7D`. As such the value `8675309` may be represented in Little-Endian as a byte stream of `[0x7D, 0xBFm 0x85]`. 

To some developers most affiliated with right-to-left natural languages Little-Endian may be seen as backwards.

## Citations

Wikipedia contributors. (2019, October 10). Endianness. In Wikipedia, The Free Encyclopedia. Retrieved 18:31, October 13, 2019, from [https://en.wikipedia.org/w/index.php?title=Endianness&oldid=920566520](https://en.wikipedia.org/w/index.php?title=Endianness&oldid=920566520).

