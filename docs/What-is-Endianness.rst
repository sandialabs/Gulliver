What is Endianness
##################

Endianness [#Endianness]_, at least as far as computing is concerned, is the ordering of bytes within a binary representation of data. The most common representations of endianness are :ref:`big-endian` and :ref:`little-endian`. There exist other forms, such as Middle-Endian, but those are beyond the scope of this document.

.. figure:: /img/endianpig.png
    :align: center
    :alt: alternate text
    :target: http://geek-and-poke.com/geekandpoke/2011/9/7/simply-explained.html

    `"Simply Explained" <http://geek-and-poke.com/geekandpoke/2011/9/7/simply-explained.html>`_

    Comic by `Oliver Widder <http://geek-and-poke.com/>`_ Released under `Attribution 3.0 Unported (CC BY 3.0) <https://creativecommons.org/licenses/by/3.0/>`_.

Entomology
**********

The Computer Science terms Big-Endian and Little-Endian were introduced by Danny Cohen [#CohenDanny]_ in 1980. The key term *endian* has its roots in the novel Gulliver’s Travels [#GulliversTravels]_ by Jonathan Swift [#SwiftJonathan]_ where within a war occurs between two factions who are fighting over which end of a boiled egg should be opened for eating. The big end or the little end. Unsurprisingly, the same said book was the inspiration for the naming of the `Gulliver <https://github.com/sandialabs/gulliver>`_ library.

.. _big-endian:

Big-Endian
**********

Big-Endian, often referred to as *Network Byte Order*, ordering is left-to-right. Given the representation of an unsigned number in bytes the further to the left that a byte exists the more significant it is.

For example, the decimal value of the unsigned integer :math:`8675309_{10}` may be represented as :math:`0x845FED_{16}` in hexadecimal. This hexadecimal value is composed of the three bytes :math:`0x84_{16}`, :math:`0x5F_{16}`, and :math:`0xED_{16}`. As such the value :math:`8675309_{10}` may be represented in Big-Endian as a byte stream of :math:`[0x5C_{16}, 0x7B_{16}, 0x2A_{16}]`.

Big-Endian integer representation likely comes as second nature to developers familiar with right-to-left Arabic numerals [#ArabicNumerals]_ representation.

.. _little-endian:

Little-Endian
*************

Little-Endian ordering is right-to-left. Given the representation of an unsigned number in bytes the further to the right a byte exists the more significant it is.

For example, the decimal value of the unsigned integer :math:`8675309_{10}` may be represented as :math:`0x845FED_{16}` in hexadecimal. This hexadecimal value is composed of the three bytes :math:`0x84_{16}`, :math:`0x5F_{16}`, and :math:`0xED_{16}`. But because little-endian byte order is left to right :math:`8675309_{10}` may be represented in Little-Endian as a byte stream of :math:`[0xED_{16}, 0x5F_{16}, 0x84_{16}]`.

To developers most affiliated with right-to-left natural languages Little-Endian may seem backwards.

.. rubric:: Footnotes

.. [#Endianness] Wikipedia contributors. (2019, October 10). Endianness. In Wikipedia, The Free Encyclopedia. Retrieved 18:31, October 13, 2019, from `https://en.wikipedia.org/w/index.php?title=Endianness&oldid=920566520 <https://en.wikipedia.org/w/index.php?title=Endianness&oldid=920566520>`_.

.. [#CohenDanny] `Cohen, Danny <https://en.wikipedia.org/wiki/Danny_Cohen_(computer_scientist)>`_ the computer scientist that termed the phrase "endian" referring to byte order.

.. [#GulliversTravels] `Gulliver's Travels <https://en.wikipedia.org/wiki/Gulliver%27s_Travels>`_ a book.

.. [#SwiftJonathan] `Swift, Jonathan <https://en.wikipedia.org/wiki/Jonathan_Swift>`_, author of Gulliver's Travels.

.. [#ArabicNumerals] `Arabic numerals <https://en.wikipedia.org/wiki/Arabic_numerals>`_ are the typical digital number format used in the English language.
