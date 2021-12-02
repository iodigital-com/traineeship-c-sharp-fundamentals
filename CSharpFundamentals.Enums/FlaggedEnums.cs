namespace CSharpFundamentals.Enums;

/// <summary>
/// If you want an enumeration type to represent a combination of choices, define enum members for those choices such that an individual choice is a bit field.
/// That is, the associated values of those enum members should be the powers of two.
/// </summary>
public class FlaggedEnums
{
    // To indicate that an enumeration type declares bit fields, apply the Flags attribute to it.
    [Flags]
    private enum Days
    {
        Undefined = 0,
        Monday    = 1,
        Tuesday   = 2,
        Wednesday = 4,
        Thursday  = 8,
        Friday    = 16,
        Saturday  = 32,
        Sunday    = 64,
        Weekend   = Saturday | Sunday,
    }

    [Test]
    public void Operations()
    {
        // You can use the bitwise logical operator | to combine choices.
        const Days businessDays = Days.Monday | Days.Tuesday | Days.Wednesday | Days.Thursday | Days.Friday;
        
        // You can use the bitwise logical operator & to intersect combinations of choices.
        
        // Assert that Days.Sunday is not a business day
        Assert.IsFalse((businessDays & Days.Sunday) == Days.Sunday);
        
        // Assert that Days.Sunday is a weekend day
        Assert.IsTrue((Days.Weekend & Days.Sunday) == Days.Sunday);
        
        // Addition & subtraction operator
        const Days sumOfWeekendDays = Days.Saturday + (int)Days.Sunday;
        var expected = ;
        Assert.AreEqual(expected, sumOfWeekendDays);
        
        // Postfix & prefix increment and decrement operators
        var saturday = Days.Saturday;
        var actual = ++saturday;

        expected = ;
        Assert.AreNotEqual(expected, actual);

        expected = Days.Saturday;
        Assert.AreEqual(expected, actual);
    }
}
