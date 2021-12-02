using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace CSharpFundamentals.Collections;

/// <summary>
/// the collections in the System.Collections.Concurrent namespace provide efficient thread-safe operations for accessing collection items from multiple threads.
/// The classes in the System.Collections.Concurrent namespace should be used instead of the corresponding types in the System.Collections.Generic
/// and System.Collections namespaces whenever multiple threads are accessing the collection concurrently.
/// https://docs.microsoft.com/en-us/dotnet/api/system.collections.concurrent
/// https://docs.microsoft.com/en-us/dotnet/standard/collections/thread-safe/
/// </summary>
public class SystemCollectionsConcurrent
{
    [Test]
    public void ConcurrentDictionary()
    {
        // ConcurrentDictionary<TKey,TValue> represents a thread-safe collection of key/value pairs that can be accessed by multiple threads concurrently.
        // Like the Dictionary<TKey,TValue> class, ConcurrentDictionary<TKey,TValue> implements the IDictionary<TKey,TValue> interface.
        // In addition, ConcurrentDictionary<TKey,TValue> provides several methods for adding or updating key/value pairs in the dictionary.
        // https://docs.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentdictionary-2
        
        ConcurrentDictionary<int, int> dictionary = new ();
        
        // The IsEmpty property value indicates whether the ConcurrentDictionary<TKey,TValue> is empty.
        Assert.IsTrue(dictionary.IsEmpty);
        
        // AddOrUpdate adds a key/value pair to the ConcurrentDictionary<TKey,TValue> if the key does not already exist,
        // or updates a key/value pair in the ConcurrentDictionary<TKey,TValue> if the key already exists.
        Parallel.For(0, 10000, i =>
        {
            // Initial call will set dictionary[1] = 1.
            // Ensuing calls will set dictionary[1] = dictionary[1] + 1
            var addValue = 1;
            dictionary.AddOrUpdate(1, addValue, (key, oldValue) => oldValue + addValue);
        });

        var expected = 10000;
        Assert.AreEqual(expected, dictionary[1]);
        
        // GetOrAdd adds a key/value pair to the ConcurrentDictionary<TKey,TValue> if the key does not already exist.
        // Returns the new value, or the existing value if the key already exists.
        int value = dictionary.GetOrAdd(2, (key) => 100);

        expected = 100;
        Assert.AreEqual(expected, value);

        // Should return 100, as key 2 is already set to that value
        value = dictionary.GetOrAdd(2, (key) => 2000);
        expected = 100;
        Assert.AreEqual(expected, value);
        
        // TryGetValue attempts to get the value associated with the specified key from the ConcurrentDictionary<TKey,TValue>.
        bool result = dictionary.TryGetValue(3, out var element);
        Assert.IsFalse(result);

        expected = 0;
        Assert.AreEqual(expected, element);
    }

    [Test]
    public void ConcurrentQueue()
    {
        // ConcurrentQueue<T> represents a thread-safe first in-first out (FIFO) collection.
        // ConcurrentQueue<T> implements the IReadOnlyCollection<T> interface.
        //
        // ConcurrentQueue<T> handles all synchronization internally.
        // If two threads call TryDequeue at precisely the same moment, neither operation is blocked.
        // When a conflict is detected between two threads, one thread has to try again to retrieve the next element, and the synchronization is handled internally.
        //
        // https://docs.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentqueue-1
        
        ConcurrentQueue<double[]> doublesQueue = new();

        var expected = ;
        Assert.AreEqual(expected, doublesQueue.Count);
        
        // Three main operations can be performed on a Queue<T> and its elements: Enqueue, Dequeue & Peek
        
        // Enqueue adds an element to the end of the Queue<T>
        doublesQueue.Enqueue(new []{ 1.0, 0.2, 1.5E4 });

        expected = ;
        Assert.AreEqual(expected, doublesQueue.Count);
        
        // TryDequeue tries to remove and return the object at the beginning of the ConcurrentQueue<T>.
        // It returns true if an element was removed and returned from the beginning of the ConcurrentQueue<T> successfully; otherwise, false.
        bool result = doublesQueue.TryDequeue(out double[] element);
        
        Assert.IsTrue(result);
        Assert.IsNotNull(element);
        
        // TryPeek returns a value that indicates whether there is an object at the beginning of the ConcurrentQueue<T>
        // If one is present, the element is copied to the result parameter but does not remove it from the Queue<T>
        // Otherwise, the result parameter is set to the default value of T.
        result = doublesQueue.TryPeek(out double[] peekElement);
        Assert.IsFalse(result);
        Assert.IsNull(peekElement);
    }

    [Test]
    public void ConcurrentStack()
    {
        // ConcurrentStack<T> represents a thread-safe last in-first out (LIFO) collection.
        // https://docs.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentstack-1
        
        ConcurrentStack<int> stack = new ();
        
        // TryPeek attempts to return an object from the top of the ConcurrentStack<T> without removing it.
        // Returns true if and object was returned successfully; otherwise, false.
        bool result = stack.TryPeek(out int value);
        Assert.IsFalse(result);

        var expected = ;
        Assert.AreEqual(expected, value);
        
        // Push inserts an object at the top of the ConcurrentStack<T>.
        Parallel.For(0, 10000, i =>
        {
            stack.Push(i);
        });

        expected = 0;
        Assert.AreEqual(expected, stack.Count);
    }
}