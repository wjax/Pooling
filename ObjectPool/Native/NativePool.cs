using Microsoft.Extensions.ObjectPool;

namespace ObjectPool.Native;

/// <summary>
/// A pool of objects that can be reused to manage memory efficiently.
/// </summary>
/// <typeparam name="T">The type of object that is pooled.</typeparam>
public static class NativePool<T> where T : class, IPoolable, new()
{
    private static DefaultObjectPool<T> _internalPool = default!;
    private static int _count = 0;

    /// <summary>
    /// The constructor.
    /// </summary>
    static NativePool()
    {
        Init(10);
    }

    /// <summary>
    /// Initialize the pool
    /// </summary>
    public static void Init(int maxItemsInPool)
    {
        _internalPool = new(
            new NativePooledObjectPolicy<T>(() =>
            {
                var item = new T();
                item.ReturnToPool = () => Return(item);

                return item;
            }, (x) => x.Reset()), maxItemsInPool);
    }

    /// <summary>
    /// Return an item into the pool for later re-use, and resets it state if a reset method was provided to the constructor.
    /// </summary>
    /// <param name="item">The item.</param>
    public static void Return(T item)
    {
        _internalPool.Return(item);
        _count++;
    }

    /// <summary>
    /// Get an item out of the pool for use.
    /// </summary>
    /// <returns>An item.</returns>
    public static T Get()
    {
        var item = _internalPool.Get();
        if (_count > 0)
            _count--;

        return item;
    }

    /// <summary>
    /// Number of items in the pool
    /// </summary>
    public static int Count => _count;
}