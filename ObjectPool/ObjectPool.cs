namespace ObjectPool;

/// <summary>
/// A pool of objects of type T that can be reused.
/// </summary>
/// <typeparam name="T"></typeparam>
public static class ObjectPool<T> where T : class, IPoolable, new()
{
    #region pool
    private static Pool<T> _pool = new (50, 1, () => new T());
    
    /// <summary>
    /// Get an object from the pool
    /// </summary>
    /// <returns>The object</returns>
    public static T RentFromPool() => _pool.Get();
    
    /// <summary>
    /// Return an object to the pool
    /// </summary>
    /// <param name="obj"></param>
    public static void ReturnToPool(T obj) => _pool.Return(obj);
    
    /// <summary>
    /// Number of objects in the pool
    /// </summary>
    public static int ObjectsInPool => _pool.Count;
    #endregion
}