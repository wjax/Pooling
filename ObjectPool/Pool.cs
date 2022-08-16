using System.Collections.Concurrent;

namespace ObjectPool;

/// <summary>
/// A pool of objects that can be reused to manage memory efficiently.
/// </summary>
/// <typeparam name="T">The type of object that is pooled.</typeparam>
public static class Pool<T> where T : IPoolable, new()
{
    // The queue that holds the items
    private static Stack<IPoolable> _objects = new ();
    // The method that creates a new item
    public static Func<T> NewItemMethod = () => new T();
    // The maximum size
    public static int MaxPoolSize = 10;

    /// <summary>
    /// The constructor.
    /// </summary>
    static Pool()
    { }

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

    public static int Count => _objects.Count;
}