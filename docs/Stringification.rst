Stringification
===============

What can be better than making byte arrays slightly more human readable!? We provide a number of ways to format a byte array that will hopefully meet your needs. Because ``byte[]`` don't implement ``IFormattable`` you have to be explicit about calling ``ByteArrayUtils.ToString`` and cannot rely on  string interpolation or ``string.Format`` and the typical format provider.

.. code-block:: c#

   public static string ByteArrayUtils.ToString(this byte[] bytes, string format = "g", IFormatProvider formatProvider = null)

Implemented ``format`` values

.. csv-table:: Implemented format values
   :file: stringification-formats.csv
   :header-rows: 1

.. code-block:: c#

   public static void StringificationExample()
   {
      // Setup
      var input = new byte[] { 0xC0, 0xFF, 0xEE, 0xC0, 0xDE };
   
      // Conclusion
      Console.WriteLine("Stringification Example");
      Console.WriteLine($"input:\t{input.ToString("H")}");
   
      Console.WriteLine("Hexadecimal Formats");
      Console.WriteLine($"H:\t\"{input.ToString("H")}\"");
      Console.WriteLine($"h:\t\"{input.ToString("h")}\"");
      Console.WriteLine($"HC:\t\"{input.ToString("HC")}\"");
      Console.WriteLine($"hc:\t\"{input.ToString("hc")}\"");
   
      Console.WriteLine("Binary Formats");
      Console.WriteLine($"b:\t\"{input.ToString("b")}\"");
      Console.WriteLine($"bc:\t\"{input.ToString("bc")}\"");
   
      Console.WriteLine("Integer Formats");
      Console.WriteLine($"d:\t\"{input.ToString("d")}\"");
      Console.WriteLine($"IBE:\t\"{input.ToString("IBE")}\"");
      Console.WriteLine($"ILE:\t\"{input.ToString("ILE")}\"");
   
      Console.WriteLine(string.Empty);
   }

.. code-block:: none

   Stringification Example
   input:  C0 FF EE C0 DE
   Hexadecimal Formats
   H:      "C0 FF EE C0 DE"
   h:      "c0 ff ee c0 de"
   HC:     "C0FFEEC0DE"
   hc:     "c0ffeec0de"
   Binary Formats
   b:      "11000000 11111111 11101110 11000000 11011110"
   bc:     "1100000011111111111011101100000011011110"
   Integer Formats
   d:      "192 255 238 192 222"
   IBE:    "828927557854"
   ILE:    "956719628224"