using System.Collections.Concurrent;

namespace ObjectPool.Custom;

/// <summary>
/// A pool of objects that can be reused to manage memory efficiently.
/// </summary>
/// <typeparam name="T">The type of object that is pooled.</typeparam>
public static class Pool<T> where T : IPoolable, new()
{
    // The queue that holds the items
    private static Stack<IPoolable> _objects = new();
    
    /// <summary>
    /// Func that creates the objects. It can be assigned before getting objects
    /// </summary>
    public static Func<T> NewItemMethod = () => new T();
    
    /// <summary>
    /// Maximum capacity of the pool. Default: 10
    /// </summary>
    public static int MaxPoolSize = 10;

    /// <summary>
    /// Return an item into the pool for later re-use, and resets it state if a reset method was provided to the constructor.
    /// </summary>
    /// <param name="item">The item.</param>
    public static void Return(IPoolable item)
    {
        // Limit size
        if (_objects.Count >= MaxPoolSize)
        {
            // Dispose if applicable
            if (item is IDisposable disposable)
                disposable.Dispose();
            return;
        }

        item.Reset();

        lock (_objects)
        {
            _objects.Push(item);
        }
    }

    /// <summary>
    /// Get an item out of the pool for use.
    /// </summary>
    /// <returns>An item.</returns>
    public static T Get()
    {
        IPoolable? item;
        bool exists = false;

        lock (_objects)
        {
            exists = _objects.TryPop(out item);
        }

        if (!exists)
        {
            item = NewItemMethod();
            IPoolable itemAvoidClosure = item;
            item.ReturnToPool = () => Return(itemAvoidClosure);
        }

        return (T)item!;
    }

    /// <summary>
    /// Number of items in the Pool
    /// </summary>
    public static int Count => _objects.Count;
}