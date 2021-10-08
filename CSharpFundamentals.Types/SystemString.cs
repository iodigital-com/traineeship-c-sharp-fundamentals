#pragma warning disable 649
#pragma warning disable 8618

namespace CSharpFundamentals.Types;

/// <summary>
/// A String object is a sequential collection of System.Char objects that represent a string;
/// a System.Char object corresponds to a UTF-16 code unit.
/// The value of the String object is the content of the sequential collection of System.Char objects,
/// and that value is immutable (that is, it is read-only).
/// Each time a string type object is modified, a new string object is created.
/// </summary>
public class SystemString
{
    // Declaring the field automatically initializes it with the default value (== null)
    // Notice we do not assign any value here;
    private readonly string defaultValue;
    
    [Test]
    public void DefaultValue()
    {
        // The default value of the string type is null
        Assert.IsNull(defaultValue);
    }
    
    [Test]
    public void EmptyValue()
    {
        // To assign an empty value to a string object, use string.Empty (== the zero-length string "")
        string string1 = string.Empty;
        string string2 = "";
        Assert.AreSame(string1, string2);
        Assert.AreSame("", string.Empty);
    }

    [Test]
    public void MaximumValue()
    {
        // The theoretical maximum size of a String object in memory is 2-GB, or about 1 billion characters.
        // In practice, an OutOfMemoryException will be thrown well before reaching a 2GB memory allocation.
        Assert.Throws<OutOfMemoryException>(() =>
        {
            string maximum = new('a', Int32.MaxValue);
        });
    }
    
    [Test]
    public void Length()
    {
        // The Length property returns the number of Char objects in this instance, not the number of Unicode characters.
        // The reason is that a Unicode character might be represented by more than one Char. 
        string string1 = "Hello World!";
        
        Assert.AreEqual(12, string1.Length);
        
        // In .NET, a null character can be embedded in a string.
        // When a string includes one or more null characters, they are included in the length of the total string.
        string string2 = "Hello World!\u0000";
        Assert.AreNotEqual(12, string2.Length);
        Assert.AreEqual(13, string2.Length);
    }

    #region StringInterpolation
    [Test]
    public void StringInterpolation()
    {
        // The $ special character identifies a string literal as an interpolated string.
        // An interpolated string is a string literal that might contain interpolation expressions.
        // When an interpolated string is resolved to a result string, items with interpolation expressions
        // are replaced by the string representations of the expression results.
        // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/tokens/interpolated
        
        string string1 = "Hello";
        string string2 = "World";

        string string3 = $"{string1} {string2}!";
        Assert.AreEqual("Hello World!", string3);
        
        // If the interpolation expression evaluates to null, an empty string ("", or String.Empty) is used.
        Assert.IsNull(defaultValue);
        Assert.DoesNotThrow(() =>
        { 
            string3 = $"{string1} {defaultValue}!";
        });
        
        Assert.AreEqual("Hello !", string3);
        
        // If the interpolation expression doesn't evaluate to null, typically the ToString method of the result expression is called.
        var date = DateTime.UnixEpoch;

        string3 = $"{date}";
        Assert.AreEqual(date.ToString(), string3);
    }
    
    // You specify a format string by following the interpolation expression with a colon (":") and the format string.
    // https://docs.microsoft.com/en-us/dotnet/standard/base-types/formatting-types#format-strings-and-net-types
    
    [Test]
    public void StringInterpolationDateTimeFormats()
    {
        // https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings
        
        var date = DateTime.UnixEpoch;

        // "d": Short date pattern. https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings#ShortDate
        var string1 = $"{date:d}";
        Assert.AreEqual("01/01/1970", string1);
        Assert.AreEqual(date.ToString("d"), string1);
        
        // "M", "m": Month/day pattern. https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings#MonthDay
        string1 = $"{date:m}";
        Assert.AreEqual("January 01", string1);
        Assert.AreEqual(date.ToString("m"), string1);
        
        // "Y", "y": Year month pattern. https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings#YearMonth
        string1 = $"{date:y}";
        Assert.AreEqual("1970 January", string1);
        Assert.AreEqual(date.ToString("y"), string1);
    }
    
    // https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings
    // Standard numeric format strings are used to format common numeric types.
    // A standard numeric format string takes the form [format specifier][precision specifier]
    // Format specifier is a single alphabetic character that specifies the type of number format, for example, currency or percent
    // Precision specifier is an optional integer that affects the number of digits in the resulting string.
    // In .NET 6 and later versions, the maximum precision value is Int32.MaxValue.
    
    [Test]
    public void CurrencyFormat()
    {
        // "C", "c" : The "C" (or currency) format specifier converts a number to a string that represents a currency amount.
        // https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings#CFormatString
        float number = 1234f;
        
        // The result string is affected by the formatting information of the current NumberFormatInfo object.
        CultureInfo.CurrentCulture = new CultureInfo("en-us");
        var string1 = $"{number:c}";
        Assert.AreEqual("$1,234.00", string1);
        Assert.AreEqual(number.ToString("c"), string1);
        
        CultureInfo.CurrentCulture = new CultureInfo("nl-be");
        string1 = $"{number:c}";
        Assert.AreEqual("€ 1.234,00", string1);
        Assert.AreEqual(number.ToString("c"), string1);
        
        CultureInfo.CurrentCulture = new CultureInfo("en-us");
        decimal value = 123.456m;
        string1 = $"{value:C4}";
        Assert.AreEqual("$123.4560", string1);
        Assert.AreEqual(value.ToString("c4"), string1);
    }

    [Test]
    public void DecimalFormat()
    {
        // The "D" (or decimal) format specifier converts a number to a string of decimal digits (0-9), prefixed by a minus sign if the number is negative.
        
        // The precision specifier indicates the minimum number of digits desired in the resulting string.
        // If no precision specifier is specified, the default is the minimum value required to represent the integer without leading zeros.
        int value = 0123456;
        string string1 = $"{value:D}";
        
        Assert.AreEqual("123456", string1);
        Assert.AreEqual($"{value}", $"{value:D}");
        Assert.AreEqual(value.ToString("D"), $"{value:D}");

        // If required, the number is padded with zeros to its left to produce the number of digits given by the precision specifier.
        string1 = $"{value:D8}";
        Assert.AreEqual("00123456",string1);
        
        // This format is supported only for integral types.
        double number = 0123.456d;
        Assert.Throws<FormatException>(() =>
        {
            string1 = $"{number:D}";
        });
    }

    [Test]
    public void ExponentialFormat()
    {
        // The exponential ("E") format specifier converts a number to a string of the form "-d.ddd…E+ddd" or "-d.ddd…e+ddd", where each "d" indicates a digit (0-9).
        // The string starts with a minus sign if the number is negative. Exactly one digit always precedes the decimal point.
        
        // The precision specifier indicates the desired number of digits after the decimal point.
        // If the precision specifier is omitted, a default of six digits after the decimal point is used.

        double value = 12345.6789;
        string string1 = $"{value:E}";
        
        Assert.AreEqual("1.234568E+004", string1);
        Assert.AreEqual(value.ToString("E"), string1);
        
        // The case of the format specifier indicates whether to prefix the exponent with an "E" or an "e".
        string1 = $"{value:e}";
        Assert.AreEqual("1.234568e+004", string1);
        Assert.AreEqual(value.ToString("e"), string1);
    }

    [Test]
    public void PercentFormat()
    {
        // The percent ("P") format specifier multiplies a number by 100 and converts it to a string that represents a percentage.
        // The precision specifier indicates the desired number of decimal places.
        // If the precision specifier is omitted, the default numeric precision supplied by the current PercentDecimalDigits property is used.

        float value = .2568f;
        var string1 = $"{value:P}";
        
        Assert.AreEqual("25.68 %", string1);
        Assert.AreEqual(value.ToString("P"), string1);

        string1 = $"{value:P0}";
        Assert.AreEqual("26 %", string1);
        Assert.AreEqual(value.ToString("P0"), string1);
    }
    #endregion
    
    #region Methods
    
    // The String class features a number of methods useful for working with strings
    // https://docs.microsoft.com/en-us/dotnet/api/system.string
    // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/strings/
    
    // It should be pointed out though, that for scenarios involving tight loops that execute many times per second,
    // it is recommended to use the Span<T> struct for string manipulations.
    // https://docs.microsoft.com/en-us/archive/msdn-magazine/2018/january/csharp-all-about-span-exploring-a-new-net-mainstay
    // https://docs.microsoft.com/en-us/dotnet/api/system.span-1

    #region Substrings
    [Test]
    public void SubString()
    {
        // Use the Substring method to create a new string from a part of the original string.
        string string1 = "Firstname Lastname";
        
        // You can search for one or more occurrences of a substring by using the IndexOf method.
        // Notice we increment the index of the space character by 1 to point to the start of the actual substring we are interested in
        var lastName = string1.Substring(string1.IndexOf(" ") + 1 ); 
        Assert.AreEqual("Lastname", lastName);
    }
    
    [Test]
    public void Split()
    {
        // Use the Split method to create a string array containing the substrings in this instance that are delimited by elements of a specified string or Unicode character array.
        string string1 = "Firstname Lastname";

        string[] substrings = string1.Split(' ');
        Assert.AreEqual("Firstname", substrings[0]);
        Assert.AreEqual("Lastname", substrings[1]);
    }
    
    [Test]
    public void RangeIndexer()
    {
        // A String object is a sequential collection of System.Char objects that represent a string.
        // This means the range operator is supported for extracting substrings.
        // https://docs.microsoft.com/en-us/dotnet/api/system.range
        // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-8.0/ranges
        string string1 = "Firstname Lastname";


        var firstName = string1[..string1.IndexOf(' ')];
        
        // Notice we increment the index of the space character by 1 to point to the start of the actual substring we are interested in
        var lastName = string1[(string1.IndexOf(' ') + 1)..];
        
        Assert.AreEqual("Firstname", firstName);
        Assert.AreEqual("Lastname", lastName);
    }
    #endregion

    [Test]
    public void StartsAndEndsWith()
    {
        string string1 = "https://www.domain.com/";
        
        // The StartsWith method determines whether the beginning of this string instance matches the specified string.
        // https://docs.microsoft.com/en-us/dotnet/api/system.string.startswith
        Assert.IsTrue(string1.StartsWith("https"));
        Assert.IsFalse(string1.StartsWith(' '));
        
        // The EndsWith method determines whether the end of this string instance matches a specified string.
        Assert.IsTrue(string1.EndsWith('/'));
        Assert.IsFalse(string1.EndsWith(".com"));
    }
    
    [Test]
    public void Trim()
    {
        string string1 = " string starting & ending with whitespace characters \r\n  ";
        
        // The Trim method returns a new string in which all leading and trailing occurrences of a set of specified characters from the current string are removed.
        // The Trim() overload (without parameter) removes all leading and trailing white-space characters from the current string.
        var trimmed = string1.Trim();
        
        Assert.IsFalse(trimmed.StartsWith(' '));
        Assert.IsFalse(trimmed.EndsWith(' '));
        
        // The newline characters have been removed as well
        Assert.IsFalse(trimmed.Contains("\r\n"));
        
        // In more detail, Trim() removes any leading or trailing characters that result in the method returning true when they are passed to the Char.IsWhiteSpace method.
        // https://docs.microsoft.com/en-us/dotnet/api/system.char.iswhitespace?view=net-5.0#System_Char_IsWhiteSpace_System_Char_
        
        // The Trim(Char) overload removes all leading and trailing instances of a character from the current string.
        trimmed = string1.Trim(' ');
        Assert.IsFalse(trimmed.StartsWith(' '));
        Assert.IsFalse(trimmed.EndsWith(' '));
        
        // The newline characters are still present in the trimmed string
        Assert.IsTrue(trimmed.Contains("\r\n"));
        
    }
    
    #endregion
}