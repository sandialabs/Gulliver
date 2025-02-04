FixedBytes
##########

The ``FixedBytes`` class brings many of these operations together allowing developers to treat a ``byte[]`` as a more complex object without the need to explicitly call helper or extension methods. It acts as a wrapper around an array of bytes in BigEndian byte order.

Implements
**********

- ``IFormattable``
- ``IReadOnlyCollection<byte>``
- ``IEquatable<FixedBytes>``
- ``IEquatable<IEnumerable<byte>>``
- ``IComparable<FixedBytes>``
- ``IComparable<IEnumerable<byte>>``
- ``IComparable``

Operators
*********

.. note::  Operators, where pertinent, treat ``FixedBytes`` as unsigned big-endian integers

Bitwise
=======

- ``|`` - OR
- ``&`` - AND
- ``^`` - XOR
- ``!`` - NOT
- ``<<`` - Shift Left
- ``>>`` - Shift Right

Mathematical
============

- ``+`` - Addition
- ``-`` - Subtraction

Comparison
==========

- ``>`` - Greater Than
- ``<`` - Less Than
- ``>=`` - Greater Than or Equal
- ``<=`` - Less Than or Equal
- ``==`` - Equals
- ``!=`` - Not Equals

Explicit Conversion (cast from)
===============================

- ``byte[]``
- ``List<byte>``

Implicit Conversion (cast to)
=============================

- ``byte[]``

.. warning:: This topic will be further expounded upon at a later date. In the mean time please feel free to browse the source code available

   - `GitHub/Gulliver <https://github.com/sandialabs/gulliver>`_

      - `FixedBytes <https://github.com/sandialabs/Gulliver/blob/main/src/Gulliver/FixedBytes.cs>`_
