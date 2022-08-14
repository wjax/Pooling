using System.Collections.Concurrent;

namespace ObjectPool;

/// <summary>
/// A pool of objects that can be reused to manage memory efficiently.
/// </summary>
/// <typeparam name="T">The type of object that is pooled.</typeparam>
internal class Pool<T> where T : IPoolable
{
    // The queue that holds the items
    private readonly ConcurrentBag<T> _objects = new ();
    // The method that creates a new item
    private readonly Func<T> _newItemMethod;
    // The maximum size
    private readonly int _maxPoolSize;

    /// <summary>
    /// The constructor.
    /// </summary>
    /// <param name="maxPoolSize">The count of items in the pool.</param>
    /// <param name="minPoolSize">Number or preallocated items in the pool</param>
    /// <param name="newItemMethod">The method that creates a new item.</param>
    public Pool(int maxPoolSize, int minPoolSize, Func<T> newItemMethod)
    {
        _maxPoolSize = maxPoolSize;
        _newItemMethod = newItemMethod;
        
        for (int i = 0; i < minPoolSize; i++)
        {
            var item = _newItemMethod();
            item.Reset();
            _objects.Add(item);
        }
    }

    /// <summary>
    /// Return an item into the pool for later re-use, and resets it state if a reset method was provided to the constructor.
    /// </summary>
    /// <param name="item">The item.</param>
    public void Return(T item)
    {
        // Limit queue size
        if (_objects.Count >= _maxPoolSize)
        {
            // Dispose if applicable
            item.Dispose();
            return;
        }

        item.Reset();

        _objects.Add(item);
    }

    /// <summary>
    /// Get an item out of the pool for use.
    /// </summary>
    /// <returns>An item.</returns>
    public T Get() => _objects.TryTake(out T? item) ? item : _newItemMethod();

    public int Count => _objects.Count;
}