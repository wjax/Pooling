namespace ObjectPool;

/// <summary>
/// An interface to be implemented by objects that can be pooled.
/// </summary>
public interface IPoolable : IDisposable
{
    /// <summary>
    /// Implements the logic to reset the object to its initial state.
    /// </summary>
    void Reset();
}