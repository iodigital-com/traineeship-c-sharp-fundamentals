using System;
using System.Linq;

namespace CSharpFundamentals.Arrays;

/// <summary>
/// You can store multiple variables of the same type in an array data structure. You declare an array by specifying the type of its elements.
/// https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/arrays/single-dimensional-arrays
/// </summary>
public class SingleDimensionalArrays
{
    [Test]
    public void Declaring()
    {
        // You create a single-dimensional array using the `new` operator specifying the array element type and the number of elements.
        var array = new int[5];
        
        // The elements of the array are initialized to the default value of the element type (in this case 0 for integers).
        Assert.IsTrue(array.All(element => element.Equals(0)));

        var array2 = new string[4];
        Assert.IsTrue(array2.All(element => string.IsNullOrEmpty(element)));
        
        // In the previous examples, the LINQ method All() is used to query the elements stored in the array.
        // https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable
    }

    [Test]
    public void Initializing()
    {
        // You can initialize the elements of an array when you declare the array.
        // The array length is inferred by the number of elements in the initialization list.

        var array = new string[] { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };
        Assert.AreEqual(7, array.Length);
        
        // You can avoid the new expression and the array type when you initialize an array upon declaration
        // This is called an implicitly typed array
        // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/arrays/implicitly-typed-arrays
        string[] array2 = { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };
        
        Assert.AreEqual(array, array2);
    }

    [Test]
    public void RetrievingData()
    {
        // You can retrieve the data of an array by using an index.
        string[] days = { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
        
        Assert.AreEqual("Mon", days[0]);
        Assert.AreEqual("Mon", days[0]);
        // Ranges and indices provide a succinct syntax for accessing single elements or ranges in a sequence.
        // https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/tutorials/ranges-indexes
        
        // Using the range .. operator you can specify the start & end of a range.
        // Ranges are exclusive, meaning the end isn't included in the range.
        var weekDays = days[..5];
        Assert.AreEqual(5, weekDays.Length);
        Assert.AreEqual("Mon", weekDays[0]);
        Assert.AreEqual("Fri", weekDays[4]);

        var weekendDays = days[5..];
        Assert.AreEqual(2, weekendDays.Length);
        Assert.AreEqual("Sat", weekendDays[0]);
        Assert.AreEqual("Sun", weekendDays[1]);

        var tuesdayThroughFriday = days[1..5];
        Assert.AreEqual(4, tuesdayThroughFriday.Length);
        Assert.AreEqual("Tue", tuesdayThroughFriday[0]);
        Assert.AreEqual("Fri", tuesdayThroughFriday[3]);
        
        // The `index from end` operator ^ specifies that an index is relative to the end of a sequence.
        Assert.AreEqual(weekendDays, days[^2..]);
        Assert.AreEqual(weekDays, days[^7..^2]);
    }

    [Test]
    public void FixedCapacity()
    {
        // Unlike the classes in the System.Collections namespaces, Array has a fixed capacity.
        // https://docs.microsoft.com/en-us/dotnet/api/system.array#remarks
        
        var days = new []{ "Mon", "Tue", "Wed", "Thu", "Fri" };
        Assert.Throws<IndexOutOfRangeException>(() =>
        {
            days[5] = "Sat";
        });
        
        // To increase the capacity, you must create a new Array object with the required capacity,
        // copy the elements from the old Array object to the new one, and delete the old Array.

        var days2 = new string[6];
        
        // The System.Array class provides methods for creating, manipulating, searching, and sorting arrays
        // https://docs.microsoft.com/en-us/dotnet/api/system.array
        Array.Copy(days, days2, 5);
        days2[5] = "Sat";
        Assert.IsTrue(days2.Contains("Sat"));
    }
    
    [Test]
    public void OutOfRange()
    {
        // Attempting to retrieve a non-existing index from an array results in an IndexOutOfRangeException
        string[] days = { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };

        Assert.Throws<IndexOutOfRangeException>(() =>
        {
            var day = days[8];
        });
    }
}