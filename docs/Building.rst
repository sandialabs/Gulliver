How to Build Gulliver
#####################

.. note:: Make sure you clone the latest version of Gulliver's main branch from Github `https://github.com/sandialabs/Gulliver.git <https://github.com/sandialabs/Gulliver.git>`_.

C# Code
*******

Setup Your C# Environment
=========================

Gulliver is built with C#, if you want to build Gulliver you'll want to set yourself up to build C# code.

#. Install the `.NET SDK <https://dotnet.microsoft.com/download>`_ appropriate for your environment.
#. Consider installing `Visual Studio <https://visualstudio.microsoft.com/vs/>`_ and/or `Visual Studio Code <https://code.visualstudio.com/>`_.
#. Everything you want to do here on out can be done with `dotnet <https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet?tabs=netcore21>`_ command line tool.

   - To build

      .. code-block:: PowerShell

         dotnet build path\to\gulliver\src\Gulliver.sln

   - To test

      .. code-block:: PowerShell

         dotnet test path\to\gulliver\src\Gulliver.Tests\Gulliver.Tests.csproj

   - To package

      .. code-block:: PowerShell

         dotnet pack path\to\gulliver\src\Gulliver\Gulliver.csproj

Build The Documentation
***********************

First Things First
==================

.. note:: Before you can run, first you must walk. Likewise, before you can build docs fist you must do some Python stuff.

The documentation relies on `Sphinx <https://www.sphinx-doc.org/en/master/index.html>`_ and `Python <https://www.python.org/>`_ and a number of Python packages.

#. Install `Python 3.7+ <https://www.python.org/downloads/>`_
#. Install Sphinx 2.2.0+ via the Python package manager *pip*

   .. code-block:: PowerShell

      pip install sphinx

#. Install the `Sphinx RTD Theme <https://sphinx-rtd-theme.readthedocs.io/en/stable/>`_ via *pip*

   .. code-block:: PowerShell

      pip install sphinx_rtd_theme

Build
=====

Once you have all the perquisites in place building the documentation, as HTML [#SphinxBuilds]_, is as simple as locating the ``make.bat`` in the Gulliver ``docs`` folder. Then simply execute

   .. code-block:: PowerShell

      path\to\gulliver\docs\make.bat html

Once complete the documentation will be present in the ``_build`` sub folder of ``docs``.

.. rubric:: footnotes

.. [#SphinxBuilds] You don't have to stick with HTML builds, Sphinx provides other artifacts types as well, take a look at their `Invocation of sphinx-build <https://www.sphinx-doc.org/en/1.5/invocation.html#invocation-of-sphinx-build>`_ for other options.
