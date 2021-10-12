namespace CSharpFundamentals.Enums;
/// <summary>
/// An enumeration type (or enum type) is a value type defined by a set of named constants of the underlying integral numeric type.
/// </summary>
public class Enums
{
    // To define an enumeration type, use the enum keyword and specify the names of enum members:
    public enum Season
    {
        Spring,
        Summer,
        Autumn,
        Winter
    }

    // Declaring the field automatically initializes it with the default value.
    
    // Notice we do not assign any value here;
    private readonly Status statusDefaultValue;
    private readonly Season seasonDefaultValue;

    [Test]
    public void DefaultValue()
    {
        // The default value of an enumeration type E is the value produced by expression (E)0, even if zero doesn't have the corresponding enum member.
        Assert.AreEqual(Status.Undefined, statusDefaultValue);
        Assert.AreEqual(Season.Spring, seasonDefaultValue);
    }
    
    [Test]
    public void ExplicitEnumConversions()
    {
        // By default, the associated constant values of enum members are of type int;
        // they start with zero and increase by one following the definition text order.
        
        // For any enumeration type, there exist explicit conversions between the enumeration type and its underlying integral type.
        // If you cast an enum value to its underlying type, the result is the associated integral value of an enum member.
        // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/enum#conversions
        
        Assert.AreEqual(0, (int)Season.Spring);
        Assert.AreEqual(1, (int)Season.Summer);
        Assert.AreEqual(2, (int)Season.Autumn);
        Assert.AreEqual(3, (int)Season.Winter);
    }
    
    // It is possible to explicitly define the associated constant value of enum members, in any order.
    // Associated constant values are not required to be sequential. Notice there is no member declared for constant value 5.
    public enum Status
    {
        Undefined = 0,
        Notified = 1,
        AwaitingConfirmation = 2,
        Confirmed = 3,
        Inactive = 6,
        Deleted = -1,
        Archived = 4
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
        Assert.AreEqual(Status.Confirmed,enumValue2 + (int)Status.AwaitingConfirmation); // Notified (1) + AwaitingConfirmation (2) = Confirmed (3)
        Assert.AreEqual(Status.Undefined,enumValue2 + (int)Status.Deleted); // Notified (1) + Deleted (-1) = Undefined (0)
        
        // ++ and -- (Postfix increment and decrement operators and Prefix increment and decrement operators)
        Assert.AreEqual(Status.AwaitingConfirmation,++enumValue2); // Notified (1) + 1 = AwaitingConfirmation (2)
        Assert.AreEqual(Status.Confirmed,--enumValue1); // Archived (4) - 1 = Confirmed (3)
        
        
        // The Status enum has no member declared for constant value 5. However, implicitly a member for constant value 5 does exist.
        // The set of values that an enum type can take on is not limited by its enum members.
        // In particular, any value of the underlying type of an enum can be cast to the enum type, and is a distinct valid value of that enum type.
        Assert.AreEqual((Status)5, enumValue1 + (int)enumValue2); // Archived (4) + Notified (1) = 5
        
        // Notice that the result of adding Status.Archived to Status.Archived does not produce the expected result of 8
        var sumOfEnumValues = enumValue1 + (int)enumValue1;
        Assert.AreNotEqual((Status)8, sumOfEnumValues); // Archived (4) + Archived (4) = 8
        
        // Instead the sum is capped at the enum member with the highest constant value 
        Assert.AreEqual((Status)6, sumOfEnumValues);
        Assert.AreEqual((Status)6, Status.Inactive);
    }
}