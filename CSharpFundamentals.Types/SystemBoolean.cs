#pragma warning disable 649

namespace CSharpFundamentals.Types;

/// <summary>
/// The bool type keyword is an alias for the .NET System.Boolean structure type that represents a Boolean value, which can be either true or false.
/// https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool
/// </summary>
public class SystemBoolean
{
    // Declaring the field automatically initializes it with the default value (== false)
    // Notice we do not assign any value here;
    private readonly bool defaultValue;
    
    [Test]
    public void DefaultValue()
    {
        // The default value of the bool type is false
        Assert.IsFalse(defaultValue);
    }
    
    #region ConvertingToAndFrom
    // .NET provides methods that you can use to convert to or from the bool type
    // https://docs.microsoft.com/en-us/dotnet/api/system.boolean?view=net-6.0#converting-to-and-from-boolean-values

    [Test]
    public void ConvertToString()
    {
        var value = true;
        string expected = "True";
        Assert.AreSame(expected, value.ToString());
        Assert.AreSame(expected, bool.TrueString);

        value = false;
        expected = "False";
        Assert.AreSame(expected, value.ToString());
        Assert.AreSame(expected, bool.FalseString);
    }

    [Test]
    public void ParseFromString()
    {
        // https://docs.microsoft.com/en-us/dotnet/api/system.boolean?view=net-6.0#parse-boolean-values
        
        string[] trueStrings = { "True", "true", "TrUe", bool.TrueString, "   true   "};
        string[] falseStrings = { "False", "false", "FalSe", bool.FalseString, "   false   "};
        string[] invalidStrings = { "0", "1", "-1", string.Empty };
        
        // Parse strings using the Boolean.Parse method.
        foreach (var value in trueStrings)
        {
            var expected = true;
            Assert.AreEqual(expected, bool.Parse(value));
        }
        
        foreach (var value in falseStrings)
        {
            var expected = false;
            Assert.AreEqual(expected, bool.Parse(value));
        }
        
        foreach (var value in invalidStrings)
        {
            Assert.Throws<FormatException>(() =>
            {
                var parsed = bool.Parse(value);
            });
        }
        
        // Parse strings using the Boolean.TryParse method.
        foreach (var value in trueStrings)
        {
            var result = bool.TryParse(value, out var parsed);
            Assert.IsTrue(result);
            Assert.AreEqual(true, parsed);
        }
        
        foreach (var value in falseStrings)
        {
            var result = bool.TryParse(value, out var parsed);
            Assert.IsTrue(result);
            Assert.AreEqual(false, parsed);
        }
        
        foreach (var value in invalidStrings)
        {
            var result = bool.TryParse(value, out var parsed);
            Assert.IsFalse(result);
            Assert.AreEqual(false, parsed);
        }
    }

    [Test]
    public void ConvertFromInteger()
    {
        // You can call a method of the Convert class to convert a Int32 type to a Boolean value.
        // https://docs.microsoft.com/en-us/dotnet/api/system.convert?view=net-6.0
        
        int value = 1;
        bool expected = true;
        Assert.AreEqual(expected, Convert.ToBoolean(value));

        value = 0;
        expected = false;
        Assert.AreEqual(expected, Convert.ToBoolean(value));
    }
    
    [Test]
    public void ConvertFromString()
    {
        // You can call a method of the Convert class to convert a string type to a Boolean value.
        // https://docs.microsoft.com/en-us/dotnet/api/system.convert?view=net-6.0
        
        string value = "true";
        bool expected = true;
        Assert.AreEqual(expected, Convert.ToBoolean(value));

        value = "false";
        expected = false;
        Assert.AreEqual(expected, Convert.ToBoolean(value));
    }
    #endregion
}