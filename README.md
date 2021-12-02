# C# Fundamentals

In this module you are introduced to the basic concepts of the C# programming language. Through simple tutorials & basic examples you will discover the internals of C#.

## Prerequisites

You'll need to set up your machine to run .NET, including the C# 10.0 compiler. [Download](https://dotnet.microsoft.com/download/dotnet/6.0) & install the .NET 6 SDK. 

You will need a C# IDE:
- Visual Studio [Win](https://visualstudio.microsoft.com/) / [Mac](https://visualstudio.microsoft.com/vs/mac/)
- [Rider](https://www.jetbrains.com/rider/)
- [Visual Studio Code](https://code.visualstudio.com/)

It is recommend you read through
[A tour of the C# language](https://docs.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/) and explore the language through [interactive examples](https://docs.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/tutorials/). 

Then fork this repository  & clone it to your development environment. 

Additional knowledge resources:
 - https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/
 - https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/

## Topics
 - [Types](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/built-in-types)
 - [String interpolation](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/tokens/interpolated)
 - [Enums](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/enum) & [Flagged Enums](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/enum#enumeration-types-as-bit-flags)
 - [Collections](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/collections) & [Thread-safe collections](https://docs.microsoft.com/en-us/dotnet/standard/collections/thread-safe/)
 - [Arrays](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/arrays/)
 - [LINQ](https://docs.microsoft.com/en-us/dotnet/csharp/linq/)
 - Unit testing with [NUnit](https://nunit.org/)

## Assignments

Each project in this solution contains a number of unit tests that demonstrate common features of the C# programming language. This repository has 2 branches: `main` & `solution`. The `main` branch contains incomplete and failing tests, and will probably not build. 

In case you get stuck you can compare the `main` branch file to the `solution` branch file for a hint at the solution.

### Your assignment: 
- Make the code build & run
- Make the tests pass, without modifying any of the `Assert` statements (more below).

### Guidelines
Each unit test method includes at least one `Assert` statement, preceded by an `expected` (or `actual`) variable.
```c#
string value = "True";
var expected = false;
Assert.AreEqual(expected, bool.Parse(value));
```

```c#
var string1 = "123.456";
float floatValue = float.Parse(string1);
var actualFloatValue = 123.456;
Assert.AreEqual(floatValue, actualFloatValue);
```

As a general rule, you should make the test pass <u>without</u> modifying the `Assert` statement itself. In almost most cases you'll only have to edit the `expected` (or `actual`) variable value.

```c#
string value = "True";
var expected = true; // was false
Assert.AreEqual(expected, bool.Parse(value));
```

```c#
var string1 = "123.456";
float floatValue = float.Parse(string1);
var actualFloatValue = 123.456f; // assertion fails with 123.456
Assert.AreEqual(floatValue, actualFloatValue);
```
## Recommended paths
As you progress through the exercises in this module, the difficulty will increase slowly.
While you are free to complete the exercises in the order of your choice, below is the order we recommend:

1. Types: SystemBoolean > SystemInt32 > FloatingPointTypes > SystemString
2. Enums: Enums > FlaggedEnums
3. Collections: SystemCollectionsGeneric > SystemCollectionsConcurrent
4. Arrays: SingleDimensionalArrays
5. Linq: SystemLinq