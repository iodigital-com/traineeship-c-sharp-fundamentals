// ReSharper disable ConditionIsAlwaysTrueOrFalse

using System.Linq;

namespace CSharpFundamentals.Collections;

/// <summary>
/// You can create a generic collection by using one of the classes in the System.Collections.Generic namespace.
/// A generic collection is useful when every item in the collection has the same data type.
/// A generic collection enforces strong typing by allowing only the desired data type to be added.
/// https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic
/// https://docs.microsoft.com/en-us/dotnet/standard/collections/selecting-a-collection-class
/// https://docs.microsoft.com/en-us/dotnet/standard/collections/commonly-used-collection-types
///
/// The following examples also demonstrate the use of LINQ (method syntax) on collection types (eg. First(), Last(), ...).
/// https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable
/// </summary>
public class SystemCollectionsGeneric
{
    [Test]
    public void List()
    {
        // The List<T> class is the generic equivalent of the ArrayList class.
        // It implements the IList<T> generic interface by using an array whose size is dynamically increased as required.
        // Represents a strongly typed list of objects that can be accessed by index. Provides methods to search, sort, and manipulate lists.
        // https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1

        List<int> intList = new();

        // The Add() method adds an object to the collection
        intList.Add(5);

        // Assert intList contains 1 object
        var expected = 1;
        Assert.AreEqual(expected, intList.Count);

        // Assert intList contains an object representing the integer value 5 
        Assert.IsTrue(intList.Contains(5));

        // The AddRange() method adds a range of objects to the collection
        intList.AddRange(new[] { 1, 6, 3, 2 });

        // Assert intList contains 5 object
        expected = 5;
        Assert.AreEqual(expected, intList.Count);

        // Assert the last object in intList contains an object representing the integer value 2
        expected = 2;
        Assert.AreEqual(expected, intList[4]);

        // Sort the intList collection
        intList.Sort();

        // Assert the last object in intList contains an object representing the integer value 6 
        expected = 6;
        Assert.AreEqual(expected, intList.Last());

        // Initialize a new collection for string object
        var stringList = new List<string>
        {
            "juice",
            "adrenaline",
            "inert",
            "zoo",
            "chocolate",
            "boost",
        };

        // Sort the stringList collection
        stringList = stringList.OrderBy(x => x).ToList();

        // Assert the last object in stringList contains an object representing the string value "zoo"
        var expectedString = "zoo";
        Assert.AreEqual(expectedString, stringList.Last());

        // Remove the string object "adrenaline"
        stringList.Remove("adrenaline");

        // Assert the string object "adrenaline" is removed from stringList
        Assert.IsFalse(stringList.Exists(element => element == "adrenaline"));
        Assert.IsFalse(stringList.Contains("adrenaline"));
    }

    [Test]
    public void Dictionary()
    {
        // Dictionary<TKey,TValue> represents a collection of keys and values.
        // https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2
        // The Dictionary<TKey,TValue> generic class provides a mapping from a set of keys to a set of values.
        // Each addition to the dictionary consists of a value and its associated key.
        // Retrieving a value by using its key is very fast, close to O(1), because the Dictionary<TKey,TValue> class is implemented as a hash table.

        Dictionary<string, string[]> dictionary = new()
        {
            { "tea", new[] { "hot water", "green tea leaves" } }
        };

        dictionary.Add("coffee", new[] { "hot water", "roasted beans" });
        dictionary.Add("chocolate milk", new[] { "hot milk", "cocoa powder", "sugar" });

        // https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2#remarks
        // Every key in a Dictionary<TKey,TValue> must be unique according to the dictionary's equality comparer.
        Assert.Throws<ArgumentException>(() => { dictionary.Add("tea", new[] { "hot water", "black tea leaves" }); });

        // The TryAdd method attempts to add the specified key and value to the dictionary.
        // Unlike the Add method, this method doesn't throw an exception if the element with the given key exists in the dictionary.
        Assert.DoesNotThrow(() =>
        {
            var result = dictionary.TryAdd("tea", new[] { "hot water", "black tea leaves" });
            // If the key already exists, TryAdd does nothing and returns false.
            var expected = false;
            Assert.AreEqual(expected, result);
        });
        
        // The indexer throws an exception if the requested key is not in the dictionary.
        
        Assert.Throws<KeyNotFoundException>(() =>
        {
            var nonExistingItem = dictionary["black tea"];
        });
        
        // Exceptions are expensive in C#, an alternative (less expensive) method is TryGetValue(),
        // which does not throw an exception if the requested key is not in the dictionary.
        Assert.DoesNotThrow(() =>
        {
            var result = dictionary.TryGetValue("black tea", out string[] value);
            Assert.IsFalse(result);
            Assert.IsNull(value);
        });
        
        // A key cannot be null
        Assert.Throws<ArgumentNullException>(() =>
        {
            dictionary.Add(null, new[] { "hot water", "black tea leaves" });
        });

        // But a value can be, if its type TValue is a reference type
        Assert.DoesNotThrow(() => { dictionary.Add("black tea", null); });
        var expected = true;
        Assert.AreEqual(expected, dictionary.ContainsValue(null));

        // For purposes of enumeration, each item in the dictionary is treated as a KeyValuePair<TKey,TValue> structure representing a value and its key.
        // The order in which the items are returned is undefined.
        foreach (KeyValuePair<string, string[]> item in dictionary)
        {
            if (item.Value == null)
            {
                dictionary.Remove(item.Key);
            }
        }

        expected = false;
        Assert.AreEqual(expected, dictionary.ContainsValue(null));
    }

    [Test]
    public void SortedList()
    {
        // SortedList<TKey,TValue> represents a collection of key/value pairs that are sorted by key based on the associated IComparer<T> implementation.
        // The SortedList<TKey,TValue> generic class is an array of key/value pairs with O(log n) retrieval, where n is the number of elements in the dictionary.
        // In this, it is similar to the SortedDictionary<TKey,TValue> generic class.
        // The two classes have similar object models, and both have O(log n) retrieval. Where the two classes differ is in memory use and speed of insertion and removal.
        // More info: https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.sortedlist-2#remarks

        SortedList<string, string[]> sortedList = new()
        {
            { "tea", new[] { "hot water", "green tea leaves" } }
        };

        // Assert first element key equals "tea"
        var expected = "tea";
        Assert.AreEqual(expected, sortedList.First().Key);
        
        sortedList.Add("coffee", new[] { "hot water", "roasted beans" });
        sortedList.Add("chocolate milk", new[] { "hot milk", "cocoa powder", "sugar" });

        // Assert first element key equals "chocolate milk"
        expected = "chocolate milk";
        Assert.AreEqual(expected, sortedList.First().Key);
    }

    [Test]
    public void SortedDictionary()
    {
        // SortedDictionary<TKey,TValue> represents a collection of key/value pairs that are sorted on the key.
        // The SortedDictionary<TKey,TValue> generic class is a binary search tree with O(log n) retrieval,
        // where n is the number of elements in the dictionary.
        // https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.sorteddictionary-2
        
        SortedDictionary<string, List<string>> dictionary = new()
        {
            { "green tea", new(){ "hot water", "green tea leaves" } }
        };

        dictionary.Add("coffee", new() { "hot water", "roasted beans" });
        dictionary.Add("chocolate milk", new() { "hot milk", "cocoa powder", "sugar" });
        dictionary.Add("black tea", new() { "hot water", "black tea leaves" });
        
        var expected = "black tea";
        Assert.AreEqual(expected, dictionary.First().Key);

        expected = "green tea";
        Assert.AreEqual(expected, dictionary.Last().Key);
    }

    // Stacks and queues are useful when you need temporary storage for information; that is, when you might want to discard an element after retrieving its value.
    // Use Queue<T> if you need to access the information in the same order that it is stored in the collection.
    // Use System.Collections.Generic.Stack<T> if you need to access the information in reverse order.
    
    [Test]
    public void Queue()
    {
        // Queue<T> represents a first-in, first-out (FIFO) collection of objects.
        // This class implements a generic queue as a circular array.
        // Objects stored in a Queue<T> are inserted at one end and removed from the other.
        // https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1

        Queue<double[]> doublesQueue = new();
        Assert.AreEqual(0, doublesQueue.Count);
        
        // Three main operations can be performed on a Queue<T> and its elements: Enqueue, Dequeue & Peek
        
        // Enqueue adds an element to the end of the Queue<T>
        doublesQueue.Enqueue(new []{ 1.0, 0.2, 1.5E4 });
        Assert.AreEqual(1, doublesQueue.Count);
        
        // Dequeue removes the oldest element from the start of the Queue<T>
        var lastElement = doublesQueue.Dequeue();
        
        var expected = 0;
        Assert.AreEqual(expected, doublesQueue.Count);
        
        // Calling Dequeue on an empty Queue<T> will throw an InvalidOperationException
        Assert.Throws<InvalidOperationException>(() =>
        {
            lastElement = doublesQueue.Dequeue();
        });
        
        // TryDequeue removes the object at the beginning of the Queue<T>, and copies it to the result parameter.
        // It returns true if the object is successfully removed; false if the Queue<T> is empty.
        Assert.DoesNotThrow(() =>
        {
            bool result = doublesQueue.TryDequeue(out double[] element);
            var expected = false;
            Assert.AreEqual(expected, result);
        });
        
        // Peek returns the oldest element that is at the start of the Queue<T> but does not remove it from the Queue<T>
        // Note that calling Peek() on an empty Queue<T> will throw an InvalidOperationException
        Assert.Throws<InvalidOperationException>(() =>
        {
            lastElement = doublesQueue.Peek();
        });

        // TryPeek returns a value that indicates whether there is an object at the beginning of the Queue<T>
        // If one is present, the element is copied to the result parameter but does not remove it from the Queue<T>
        // Otherwise, the result parameter is set to the default value of T.
        Assert.DoesNotThrow(() =>
        {
             bool result = doublesQueue.TryPeek(out double[] element);
             var expected = false;
             Assert.AreEqual(expected, result);
             Assert.IsNull(element);
        });
    }

    [Test]
    public void Stack()
    {
        // Stack<T> represents a variable size last-in-first-out (LIFO) collection of instances of the same specified type.
        // A common use for System.Collections.Generic.Stack<T> is to preserve variable states during calls to other procedures.
        // https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.stack-1?

        Stack<SortedList<string, string[]>> listStack = new();
        var expected = 0;
        Assert.AreEqual(expected, listStack.Count);
        
        // Three main operations can be performed on a Stack<T> and its elements: Push, Pop & Peek
        
        // Push inserts an element at the top of the Stack<T>.
        listStack.Push(new SortedList<string, string[]>()
        {
            {"green tea", new []{"hot water", "green tea leaves"}},
            {"black tea", new []{"hot water", "black tea leaves"}},
            {"camomile tea", new []{"hot water", "camomile flowers", "honey"}}
        });

        expected = 1;
        Assert.AreEqual(expected, listStack.Count);
        
        // Pop removes and returns the object at the top of the Stack<T>.
        var element = listStack.Pop();
        expected = 0;
        Assert.AreEqual(expected, listStack.Count);
        
        // Calling Pop on an empty Stack<T> will throw an InvalidOperationException
        Assert.Throws<InvalidOperationException>(() =>
        {
            element = listStack.Pop();
        });
        
        // Returns a value that indicates whether there is an object at the top of the Stack<T>,
        // and if one is present, copies it to the result parameter, and removes it from the Stack<T>.
        Assert.DoesNotThrow(() =>
        {
            bool result = listStack.TryPop(out SortedList<string, string[]> element);
            var expected = false;
            Assert.AreEqual(expected, result);
            Assert.IsNull(element);
        });
        
        // Peek returns an element that is at the top of the Stack<T> but does not remove it from the Stack<T>.
        Assert.Throws<InvalidOperationException>(() =>
        {
            element = listStack.Peek();
        });
        
        // TryPeek returns a value that indicates whether there is an object at the top of the Stack<T>,
        // and if one is present, copies it to the result parameter. The object is not removed from the Stack<T>.
        Assert.DoesNotThrow(() =>
        {
            bool result = listStack.TryPeek(out SortedList<string, string[]> element);
            var expected = false;
            Assert.AreEqual(expected, result);
            Assert.IsNull(element);
        });
    }
}