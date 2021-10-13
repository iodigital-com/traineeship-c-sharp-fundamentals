using System.Linq;
using System.Text;

namespace CSharpFundamentals.Linq;

/// <summary>
/// The methods in this class provide an implementation of the standard query operators for querying data sources that implement IEnumerable<T>.
/// The standard query operators are general purpose methods that follow the LINQ pattern and enable you
/// to express traversal, filter, and projection operations over data in any .NET-based programming language.
/// 
/// It is important to note that none of these methods alter the queried collection, but instead return a new collection that contains the result of the operation.
/// 
/// The examples below demonstrate the use cases of only a subset of the methods provided in the Enumerable class. 
/// More info: https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable
/// </summary>
public class SystemLinqEnumerable
{
    private List<string> stringList = new();
    private string[] stringArray = Array.Empty<string>();
    private Dictionary<string, string[]> dictionary = new();
    private int[] intArray = Array.Empty<int>();
    
    [SetUp]
    public void Setup()
    {
        stringList = new List<string> { "water", "soda", "beer", "juice" };
        stringArray = new []{ "water", "soda", "beer","juice" };
        dictionary = new Dictionary<string, string[]>
        {
            { "beer", new []{"pils", "trappist", "ale"}},
            { "dairy", new []{"milk", "yoghurt"}},
            { "fruit juice", new []{ "apple juice", "orange juice", "lemon juice"}}
        };
        intArray = new []{ 0, 1, 1, 2, 3, 5, 8, 13 };
    }
    
    [Test]
    public void All()
    {
        // The Enumerable.All method determines whether all elements of a sequence satisfy a condition.
        // Returns true if every element of the source sequence passes the test in the specified predicate, or if the sequence is empty; otherwise, false.
        // https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.all
        
        // The following 2 statements both perform the same operation:
        // Check the collection does not contain any strings that are null, empty ("") or whitespace.
        Assert.IsFalse(stringList.All(x => string.IsNullOrWhiteSpace(x)));
        Assert.IsTrue(stringArray.All(x => !string.IsNullOrWhiteSpace(x)));
    }
    
    [Test]
    public void Any()
    {
        // The Enumerable.Any method determines whether any element of a sequence exists or satisfies a condition.
        // Returns true if the source sequence contains any elements; otherwise, false.
        // The enumeration of source is stopped as soon as the result can be determined.
        // https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.any
        
        // Check the collection contains elements
        Assert.IsTrue(dictionary.Any());
        
        // Check the collection contains any elements satisfying the specified condition
        Assert.IsTrue(stringList.Any(x => x.Equals("water")));
        Assert.IsTrue(stringArray.Any(x => x.Equals("juice")));
        Assert.IsFalse(dictionary.Any(keyValuePair => keyValuePair.Key.Equals("water")));
        Assert.IsTrue(dictionary.Any(keyValuePair => keyValuePair.Value.Contains("ale")));
    }

    #region RetrievingElements
    
    [Test]
    public void FirstOrDefault()
    {
        // The Enumerable.FirstOrDefault method returns the first element of a sequence, or a default value if no element is found.
        // https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.firstordefault
        
        // The following examples try to retrieve the first element from dictionary satisfying the specified condition
        Assert.AreEqual(new KeyValuePair<string,string[]>(), dictionary.FirstOrDefault(x => x.Value.Length.Equals(4)));
        Assert.IsNotNull(stringArray.FirstOrDefault(x => x.Equals("water")));
    }
    
    [Test]
    public void LastOrDefault()
    {
        // The Enumerable.LastOrDefault method returns the last element of a sequence, or a default value if no element is found.
        // https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.lastordefault
        
        // The following examples try to retrieve the last element from dictionary satisfying the specified condition
        Assert.AreEqual(dictionary["fruit juice"], dictionary.LastOrDefault(x => x.Value.Length.Equals(3)).Value);
        Assert.AreEqual(intArray[2], intArray.LastOrDefault(x => x.Equals(1)));
    }
    
    [Test]
    public void Single()
    {
        // The Enumerable.Single method returns a single, specific element of a sequence,
        // and throws an exception if there is not exactly one element in the sequence.
        // https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.single

        Assert.Throws<InvalidOperationException>(() =>
        {
            var element = intArray.Single(x => x.Equals(1));
        });
        
        Assert.AreEqual(dictionary["beer"], dictionary.Single(x => x.Key.Equals("beer")).Value);
    }

    [Test]
    public void SkipTake()
    {
        // The Enumerable.Skip method bypasses a specified number of elements in a sequence and then returns the remaining elements.
        // https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.skip
        // The Enumerable.Take method returns a specified number of contiguous elements from the start of a sequence.
        // https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.take
        // Combined, these methods are useful to retrieve paginated results

        const int limit = 3;
        for (var i = 0; i < intArray.Length; i += limit)
        {
            var results = intArray.Skip(i).Take(limit);
            Assert.AreEqual(intArray[i], results.First());
        }
    }
    
    [Test]
    public void Select()
    {
        // The Enumerable.Select method projects each element of a sequence into a new form.
        // https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.select

        var projected = dictionary.Select(element =>
        {
            StringBuilder builder = new (); // https://docs.microsoft.com/en-us/dotnet/api/system.text.stringbuilder
            builder.AppendJoin(", ", element.Value);
            builder.Append(" are examples of ");
            builder.Append(element.Key);
            return builder.ToString();
        }).ToList();
        
        Assert.AreEqual("pils, trappist, ale are examples of beer", projected.First());
        Assert.AreEqual(dictionary.Count, projected.Count);
    }

    [Test]
    public void SelectMany()
    {
        // The Enumerable.SelectMany method projects each element of a sequence to an IEnumerable<T> and flattens the resulting sequences into one sequence.
        // Although SelectMany works similarly to Select, it differs in that the transform function returns a collection that is then expanded by SelectMany before it is returned.
        // https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.selectmany
        
        var result = dictionary.SelectMany(x =>
        {
            return x.Value.Select(value => $"{value} is {x.Key}");
        }).ToArray();
        
        Assert.IsTrue(result.Any(x => x.Equals("ale is beer")));
        Assert.IsTrue(result.Any(x => x.Equals("milk is dairy")));
        Assert.IsTrue(result.Any(x => x.Equals("orange juice is fruit juice")));
    }
    
    [Test]
    public void Where()
    {
        // The Enumerable.Where method filters a sequence of values based on a predicate.
        // https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.where
        
        // Retrieve only the elements that are even integers
        var even = intArray.Where(x => x % 2 == 0).ToArray();
        
        Assert.IsTrue(even.All(x => x % 2 == 0));
        
        // Retrieve only the elements that satisfy the specified condition
        var result = dictionary.Where(x => x.Value.Contains("yoghurt")).SelectMany(x =>
        {
            return x.Value.Select(value => $"{value} is a {x.Key} product");
        }).ToArray();

        Assert.AreEqual("milk is a dairy product", result.First());
        Assert.AreEqual("yoghurt is a dairy product", result.Last());
    }
    
    #endregion

    // TODO Concat, Zip, Join, Except, Union
    [Test]
    public void Concat()
    {
        // https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.concat
        // It is possible to combine collections that implement IEnumerable interface
        // The following example concatenates an string array with a list of strings
        var result = stringArray.Concat(new List<string>{"banana", "strawberry"}).ToList();
        Assert.AreEqual(stringArray.Length + 2, result.Count);
    }
    
    #region NumericComputation
    
    [Test]
    public void Average()
    {
        // The Enumerable.Average method computes the average of a sequence of numeric values
        // https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.average
        
        Assert.AreEqual(4.125d, intArray.Average());
    }
    
    [Test]
    public void Sum()
    {
        // The Enumerable.Sum method computes the sum of a sequence of numeric values
        // https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.sum
        
        Assert.AreEqual(33, intArray.Sum());
    }
    #endregion
}