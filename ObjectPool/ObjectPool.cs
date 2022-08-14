using System.Collections.Concurrent;

namespace ObjectPool;

/// <summary>
/// A pool of objects of type T that can be reused.
/// </summary>
public static class ObjectPool
{
    #region pool
    //private static ConcurrentDictionary<string, GenericPool<IPoolable>> _dictPools = new ();

    /// <summary>
    /// Get an object from the pool
    /// </summary>
    /// <returns>The object</returns>
    public static T RentFromPool<T>() where T : IPoolable, new()
    {
        var type = typeof(T).Name;
        GenericPool<IPoolable>? pool;
        
        if (!_dictPools.TryGetValue(type, out pool))
        {
            pool = new GenericPool<IPoolable>(10,1, () => new T());
            _dictPools.TryAdd(type, pool);
        }
        
        return (T)pool.Get();
    }

    /// <summary>
    /// Return an object to the pool
    /// </summary>
    /// <param name="obj"></param>
    public static void ReturnToPool(IPoolable obj)
    {
        var type = obj.GetType().Name;
        
        if (_dictPools.TryGetValue(type, out var pool))
            pool.Return(obj);
        else
            throw new InvalidOperationException("Object type not in pool. Object should be rented first");
    }
    
    /// <summary>
    /// Number of objects in the pool
    /// </summary>
    public static int ObjectsInPool<T>() where T : IPoolable
    {
        var type = typeof(T).Name;
        if (_dictPools.TryGetValue(type, out var pool))
            return pool.Count;

        return -1;
    }
    #endregion
}