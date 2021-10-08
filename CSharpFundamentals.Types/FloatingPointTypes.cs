using System.Diagnostics.CodeAnalysis;

#pragma warning disable 649
namespace CSharpFundamentals.Types;

/// <summary>
/// The floating-point numeric types represent real numbers. All floating-point numeric types are value types.
/// They are also simple types and can be initialized with literals.
/// All floating-point numeric types support arithmetic, comparison, and equality operators.
///
/// https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types
/// </summary>
[SuppressMessage("ReSharper", "LocalVariableHidesMember")]
public class FloatingPointTypes
{
    // C# supports the following predefined floating-point types: float, double, decimal (in order of increasing range & precision)
    // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types#characteristics-of-the-floating-point-types

    private readonly float floatDefaultValue;
    private readonly double doubleDefaultValue;
    private readonly decimal decimalDefaultValue;
    
    private readonly float floatMinValue = float.MinValue;
    private readonly double doubleMinValue = double.MinValue;
    private readonly decimal decimalMinValue = decimal.MinValue;
    
    private readonly float floatMaxValue = float.MaxValue;
    private readonly double doubleMaxValue = double.MaxValue;
    private readonly decimal decimalMaxValue = decimal.MaxValue;

    [Test]
    public void DefaultValue()
    {
        // The default value of each floating-point type is zero, 0
        Assert.AreEqual(0, floatDefaultValue);
        Assert.AreEqual(0, doubleDefaultValue);
        Assert.AreEqual(0, decimalDefaultValue);
    }
    
    [Test]
    public void MinimumValue()
    {
        // Each of the floating-point types has the MinValue constant that provide the minimum finite value of that type.
        // The type of a real literal is determined by its suffix. You can also use scientific notation.
        
        // The literal without suffix or with the d or D suffix is of type double
        double doubleMinValue = -1.7976931348623157E+308;
        Assert.AreEqual(double.MinValue, doubleMinValue);
        
        // The literal with the f or F suffix is of type float
        float floatMinValue = -3.40282347E+38f;
        Assert.AreEqual(float.MinValue, floatMinValue);

        // The literal with the m or M suffix is of type decimal
        decimal decimalMinValue = -79228162514264337593543950335m;
        Assert.AreEqual(decimal.MinValue, decimalMinValue);
    }
    
    [Test]
    public void MaximumValue()
    {
        // Each of the floating-point types has the MaxValue constant that provide the maximum finite value of that type.
        float floatMaxValue = 3.40282347E+38f;
        Assert.AreEqual(float.MaxValue, floatMaxValue);

        double doubleMaxValue = 1.7976931348623157E+308;
        Assert.AreEqual(double.MaxValue, doubleMaxValue);

        decimal decimalMaxValue = 79228162514264337593543950335m;
        Assert.AreEqual(decimal.MaxValue, decimalMaxValue);
    }

    [Test]
    public void NaNAndInfinityValues()
    {
        // The float and double types also provide constants that represent not-a-number and infinity values.
        // https://docs.microsoft.com/en-us/dotnet/api/system.double.nan

        // A method or operator returns NaN when the result of an operation is undefined. For example, the result of dividing zero by zero is NaN
        // The following example illustrates the use of NaN
        double zero = 0;
        Assert.IsTrue(double.IsNaN(0/zero));

        // Dividing a non-zero number by zero returns either PositiveInfinity or NegativeInfinity, depending on the sign of the divisor.
        double notZero = 1.0;
        
        Assert.IsTrue(double.IsPositiveInfinity(notZero/zero));
        Assert.IsTrue(double.IsNegativeInfinity(-notZero/zero));
        
        // The IsInfinity method returns true if the parameter evaluates to PositiveInfinity or NegativeInfinity; otherwise, false
        Assert.IsTrue(double.IsInfinity(notZero/zero));
        Assert.IsTrue(double.IsInfinity(-notZero/zero));
    }
    
    #region ConvertingToAndFrom

    [Test]
    public void ImplicitConversions()
    {
        // There is only one implicit conversion between floating-point numeric types: from float to double.
        float floatValue = 12.345f;
        double doubleValue = 0;
        
        Assert.DoesNotThrow(() => {
             doubleValue = floatValue;
        });
        
        Assert.AreEqual(floatValue, doubleValue);
        Assert.AreEqual(12.345000267028809d, doubleValue);
    }
    
    [Test]
    public void ExplicitConversions()
    {
        // You can convert any floating-point type to any other floating-point type with the explicit cast.
        // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/type-testing-and-cast#cast-expression
        float floatValue = 0;
        double doubleValue = 12.345d;
        decimal decimalValue = 0;
        
        Assert.DoesNotThrow(() => {
            // A cast expression of the form (T)E performs an explicit conversion of the result of expression E to type T
            floatValue = (float)doubleValue;
            decimalValue = (decimal)floatValue;
        });
        
        Assert.AreEqual(doubleValue, decimalValue);
        Assert.AreEqual(floatValue, decimalValue);
        Assert.AreEqual(12.345m, decimalValue);
        
        // An explicit numeric conversion might result in data loss or throw an exception, typically an OverflowException.
        
        // When you convert a decimal value to an integral type, this value is rounded towards zero to the nearest integral value.
        int integralValue = (int)decimalValue;
        Assert.AreEqual(12, integralValue);

        integralValue = (int)1234.5678m;
        Assert.AreEqual(1234,integralValue);
        
        // If the resulting integral value is outside the range of the destination type, an OverflowException is thrown.
        Assert.Throws<OverflowException>(() =>
        { 
            integralValue = (int)decimalMaxValue;
        });
        
        // When you convert a double or float value to an integral type, this value is rounded towards zero to the nearest integral value.
        integralValue = (int)987.654321;
        Assert.AreEqual(987, integralValue);
        
        integralValue = (int)-123.4567f;
        Assert.AreEqual(-123, integralValue);
        
        // If the resulting integral value is outside the range of the destination type, the result depends on the overflow checking context.
        // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/checked-and-unchecked
        
        // In an unchecked context, the result is an unspecified value of the destination type.
        Assert.DoesNotThrow(() =>
        {
            integralValue = (int)doubleMaxValue;
        });
        Assert.AreEqual(int.MinValue, integralValue);
        
        Assert.DoesNotThrow(() =>
        {
            integralValue = (int)floatMaxValue;
        });
        Assert.AreEqual(int.MinValue, integralValue);
        
        // In a checked context, an OverflowException is thrown
        Assert.Throws<OverflowException>(() =>
        {
            checked
            {
                integralValue = (int)doubleMaxValue;
            }
        });
        
        Assert.Throws<OverflowException>(() =>
        {
            checked
            {
                integralValue = (int)floatMaxValue;
            }
        });
        
        // When you convert double to float, the double value is rounded to the nearest float value.
        floatValue = (float)Math.PI; // 3.14159265358979323846
        Assert.AreEqual(3.14159274f, floatValue);
        
        // If the double value is too small or too large to fit into the float type, the result is zero or infinity.
        floatValue = (float)double.Epsilon; // Epsilon represents the smallest positive value greater than zero
        Assert.AreEqual(0f, floatValue);
        Assert.AreNotEqual(float.Epsilon,floatValue);

        floatValue = (float)doubleMinValue;
        Assert.AreEqual(float.NegativeInfinity, floatValue);
        
        floatValue = (float)doubleMaxValue;
        Assert.AreEqual(float.PositiveInfinity, floatValue);
    }

    [Test]
    public void DecimalConversionMethods()
    {
        // In addition to explicit conversion using cast operators, the System.Decimal struct features methods for converting decimal type objects to other types
        // https://docs.microsoft.com/en-us/dotnet/api/system.decimal
        decimal value = 12.345m;
        
        Assert.AreEqual((int)value, decimal.ToInt32(value)); // https://docs.microsoft.com/en-us/dotnet/api/system.decimal.toint32
        Assert.AreEqual((float)value, decimal.ToSingle(value)); // https://docs.microsoft.com/en-us/dotnet/api/system.decimal.tosingle
        Assert.AreEqual((double)value, decimal.ToDouble(value)); // https://docs.microsoft.com/en-us/dotnet/api/system.decimal.todouble
        
        // Similar to explicit numeric conversions, the return value of the conversion methods might result in data loss or throw an exception, typically an OverflowException.
        Assert.Throws<OverflowException>(() =>
        {
            int integralValue = decimal.ToInt32(decimal.MaxValue);
        });
        
        Assert.Throws<OverflowException>(() =>
        {
            int integralValue = decimal.ToInt32(decimal.MinValue);
        });
        
        Assert.DoesNotThrow(() =>
        {
            float floatValue = decimal.ToSingle(decimal.MaxValue);
        });
        
        Assert.DoesNotThrow(() =>
        {
            double doubleValue = decimal.ToDouble(decimal.MaxValue);
        });
    }
    
    [Test]
    public void ParseFromString()
    {
        // Each of the floating-point types has Parse & TryParse methods that converts the string representation of a number to its floating-point number equivalent.
        
        var string1 = "123.456";
        float floatValue = float.Parse(string1);
        Assert.AreEqual(123.456f, floatValue);
        
        double doubleValue = double.Parse(string1);
        Assert.AreEqual(123.456, doubleValue);
        
        decimal decimalValue = decimal.Parse(string1);
        Assert.AreEqual(123.456m, decimalValue);
        
        // Float & double: Values that are too large to represent are rounded to PositiveInfinity or NegativeInfinity as required by the IEEE 754 specification.
        string1 = "3.402824e38";
        floatValue = float.Parse(string1);
        Assert.AreEqual(float.PositiveInfinity, floatValue);
        
        string1 = "1.79769313486232e308";
        doubleValue = double.Parse(string1);
        Assert.AreEqual(double.PositiveInfinity, doubleValue);
        
        // Decimal: an OverflowException will be thrown when trying to parse the string representation of a number less than MinValue or greater than MaxValue.
        string1 = "79,228,162,514,264,337,593,543,950,336";
        
        Assert.Throws<OverflowException>(() =>
        {
            decimalValue = decimal.Parse(string1);
        });
    }
    #endregion
}