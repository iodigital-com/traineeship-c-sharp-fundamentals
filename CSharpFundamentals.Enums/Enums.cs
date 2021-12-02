namespace CSharpFundamentals.Enums;
/// <summary>
/// An enumeration type (or enum type) is a value type defined by a set of named constants of the underlying integral numeric type.
/// </summary>
public class Enums
{
    // To define an enumeration type, use the enum keyword and specify the names of enum members:
    private enum Season
    {
        Spring,
        Summer,
        Autumn,
        Winter
    }

    // It is possible to explicitly define the associated constant value of enum members, in any order.
    // Associated constant values are not required to be sequential. Notice there is no member declared for constant value 5.
    private enum Status
    {
        Undefined = 0,
        Notified = 1,
        AwaitingConfirmation = 2,
        Confirmed = 3,
        Inactive = 6,
        Deleted = -1,
        Archived = 4
    }
    
    // Declaring the field automatically initializes it with the default value.
    
    // Notice we do not assign any value here;
    private readonly Status statusDefaultValue;
    private readonly Season seasonDefaultValue;

    [Test]
    public void DefaultValue()
    {
        // The default value of an enumeration type E is the value produced by expression (E)0, even if zero doesn't have the corresponding enum member.
        var expectedStatusDefaultValue = Status.Undefined;
        Assert.AreEqual(expectedStatusDefaultValue, statusDefaultValue);

        var expectedSeasonDefaultValue = Season.Spring;
        Assert.AreEqual(expectedSeasonDefaultValue, seasonDefaultValue);
    }
    
    [Test]
    public void ExplicitEnumConversions()
    {
        // By default, the associated constant values of enum members are of type int;
        // they start with zero and increase by one following the definition text order.
        
        // For any enumeration type, there exist explicit conversions between the enumeration type and its underlying integral type.
        // If you cast an enum value to its underlying type, the result is the associated integral value of an enum member.
        // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/enum#conversions

        var expected = 0;
        Assert.AreEqual(expected, (int)Season.Spring);
        
        expected = 1;
        Assert.AreEqual(expected, (int)Season.Summer);
        
        expected = 2;
        Assert.AreEqual(expected, (int)Season.Autumn);
        
        expected = 3;
        Assert.AreEqual(expected, (int)Season.Winter);
    }
    
    
    [Test]
    public void Operations()
    {
        var enumValue1 = Status.Archived;
        var enumValue2 = Status.Notified;
        
        // The following operators can be used on values of enum types:
        
        // ==, !=, <, >, <=, >= (Enumeration comparison operators)
        Assert.IsTrue(enumValue1 == Status.Archived);
        Assert.IsTrue(enumValue1 != Status.Deleted);
        Assert.IsFalse(enumValue1 < Status.Confirmed);
        Assert.IsTrue(enumValue1 > Status.Deleted);
        
        // binary + (Addition operator), binary - (Subtraction operator)
        var expected = Status.Confirmed;
        Assert.AreEqual(expected,enumValue2 + (int)Status.AwaitingConfirmation); // Notified (1) + AwaitingConfirmation (2) = Confirmed (3)

        expected = Status.Undefined;
        Assert.AreEqual(expected,enumValue2 + (int)Status.Deleted); // Notified (1) + Deleted (-1) = Undefined (0)
        
        // ++ and -- (Postfix increment and decrement operators and Prefix increment and decrement operators)

        expected = Status.AwaitingConfirmation;
        Assert.AreEqual(expected,++enumValue2); // Notified (1) + 1 = AwaitingConfirmation (2)

        expected = Status.Confirmed;
        Assert.AreEqual(expected,--enumValue1); // Archived (4) - 1 = Confirmed (3)
        
        
        // The Status enum has no member declared for constant value 5. However, implicitly a member for constant value 5 does exist.
        // The set of values that an enum type can take on is not limited by its enum members.
        // In particular, any value of the underlying type of an enum can be cast to the enum type, and is a distinct valid value of that enum type.
        var sumOfEnumValues = Status.Archived + (int)Status.Notified;
        expected = (Status)5;
        Assert.AreEqual(expected, sumOfEnumValues); // Archived (4) + Notified (1) = 5
        
        sumOfEnumValues = Status.Archived + (int)Status.Archived;
        expected = (Status)8;
        Assert.AreEqual(expected, sumOfEnumValues); // Archived (4) + Archived (4) = 8
    }
}