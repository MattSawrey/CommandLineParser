# CommandLineParser

A .Net Core tool for parsing CLI commands and arguments.

# Why

I wanted a simple, easy to use command line command and argument parsing solution for use in my own .Net based CLI projects.

# State

CommandLineParser is still in active development and is not ready to use. Feel free to contribute if you want to though :)

# Constraints

For simplicity, CommandLineParser assumes the following input structure for command-line arguments:

{{commandName}} {{argumentFlagPrefix + argumentFlagName/argumentFlagIdentifier}} {{argumentValue}}

An example of this would be:

new --type test --number 25

CommandLineParser is intended to support parsing of the following datatypes to argument values:

- String
- Char
- Integer
- Float
- Double
- Decimal
- Boolean

# Definitions:

- Command: the called CLI action.
- Argument: the properties and their values on the command object.

# Rules:

- string values that include spaces must be wrapped by double quotes ("test string").
