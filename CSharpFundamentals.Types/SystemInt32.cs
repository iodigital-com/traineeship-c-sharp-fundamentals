#pragma warning disable 649

namespace CSharpFundamentals.Types;

/// <summary>
/// Int32 is an immutable value type that represents signed integers with values
/// that range from negative 2,147,483,648 (which is represented by the Int32.MinValue constant)
/// through positive 2,147,483,647 (which is represented by the Int32.MaxValue constant.
/// https://docs.microsoft.com/en-us/dotnet/api/system.int32?view=net-6.0
/// </summary>
public class SystemInt32
{
    // Declaring the field automatically initializes it with the default value (== 0)
    // Notice we do not assign any value here;
    private readonly int defaultValue;
    
    [Test]
    public void DefaultValue()
    {
        // The default value of the Int32 type is 0
        int expected = 0;
        Assert.AreEqual(expected, defaultValue);
    }
    
    // The following examples also shows the use of _ as a digit separator.
    // You can use the digit separator with all kinds of numeric literals.
    // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/integral-numeric-types#integer-literals

    [Test]
    public void MinimumValue()
    {
        int minValue = -2_147_483_648;
        Assert.AreEqual(int.MinValue, minValue);
        
        // By default, assigning a value exceeding the supported range will wrap around,
        // without throwing an OverflowException.
        Assert.DoesNotThrow(() =>
        {
            minValue = minValue - 1;
        });
        
        Assert.AreEqual(int.MaxValue, minValue);
    }
    
    [Test]
    public void MaximumValue()
    {
        int maxValue = 2_147_483_647;
        Assert.AreEqual(int.MaxValue, maxValue);
        
        // By default, assigning a value exceeding the supported range will wrap around,
        // without throwing an OverflowException.
        Assert.DoesNotThrow(() =>
        {
            maxValue = maxValue + 1;
        });
        
        Assert.AreEqual(int.MinValue, maxValue);
    }

    [Test]
    public void CheckingContext()
    {
        // The result of assigning a value exceeding the supported range depends on the checking context.
        // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/checked-and-unchecked
        
        int maxValue = 2_147_483_647;
        int overflowValue = 0;
        
        // If neither checked nor unchecked is specified, the default context for non-constant expressions (expressions that are evaluated at run time)
        // is defined by the value of the CheckForOverflowUnderflow compiler option.
        // By default the value of that option is unset and arithmetic operations are executed in an unchecked context.
        Assert.DoesNotThrow(() =>
        {
            overflowValue = maxValue + 1;
        });
        Assert.AreEqual(int.MinValue, overflowValue);
        
        // The checked keyword is used to explicitly enable overflow checking for integral-type arithmetic operations and conversions.
        Assert.Throws<OverflowException>(() =>
        {
            checked
            {
                overflowValue = maxValue + 1;
            }
        });
        
        // The unchecked keyword is used to suppress overflow-checking for integral-type arithmetic operations and conversions.
        Assert.DoesNotThrow(() =>
        {
            unchecked
            {
                overflowValue = maxValue + 1;
            }
        });
    }
    
    #region ConvertingToAndFrom

    [Test]
    public void ParseFromString()
    {
        const string string1 = "101";
        const string hexadecimal = "F9A3C";
        
        // You can call the Parse or TryParse method to convert
        // the string representation of an Int32 value to an Int32.
        // The string can contain either decimal or hexadecimal digits. 
        int expected = 101;
        Assert.AreEqual(expected, int.Parse(string1));

        expected = 1022524;
        Assert.AreEqual(expected, int.Parse(hexadecimal, System.Globalization.NumberStyles.HexNumber));

        var result = int.TryParse(string1, out int parsed);
        Assert.IsTrue(result); // The return value indicates whether the conversion succeeded.

        expected = 101;
        Assert.AreEqual(expected, parsed);
        
        result = int.TryParse(hexadecimal, 
            System.Globalization.NumberStyles.HexNumber,
            null, 
            out int parsedHex);
        Assert.IsTrue(result); // The return value indicates whether the conversion succeeded.
        expected = 1022524;
        Assert.AreEqual(expected, parsedHex);
    }

    [Test]
    public void ConvertFromString()
    {
        const string string1 = "101";
        const string hexadecimal = "F9A3C";
        
        // You can call a method of the Convert class to a string type to an Int32 value.
        // https://docs.microsoft.com/en-us/dotnet/api/system.convert?view=net-6.0
        int expected = 101;
        Assert.AreEqual(expected, Convert.ToInt32(string1));
        
        // However, this does not work for strings containing hexadecimal digits.
        Assert.Throws<FormatException>(() =>
        {
            // This will throw a FormatException because the string to be converted
            // is not recognized as a valid number.
            Convert.ToInt32(hexadecimal);
        });
    }

    [Test]
    public void ImplicitNumericConversions()
    {
        int a = 12;
        
        // Any integral numeric type is implicitly convertible to any floating-point numeric type.
        // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/numeric-conversions
        // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types
        
        #region ImplicitConversionToDouble
            double d = 0.00;
            
            Assert.DoesNotThrow(() =>
            {
                d = a;
            });
            
            Assert.AreEqual(12.00, d);
        #endregion
        
        #region ImplicitConversionToFloat
            float f = 0f;
            
            Assert.DoesNotThrow(() =>
            {
                f = a;
            });
            
            Assert.AreEqual(12.0f, f);
        #endregion
    
        #region ImplicitConversionToDecimal
            decimal dec = 0m;
                
            Assert.DoesNotThrow(() =>
            {
                dec = a;
            });
                
            Assert.AreEqual(12m, dec);
        #endregion
    }
    #endregion
}