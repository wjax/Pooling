using Microsoft.Extensions.ObjectPool;

namespace ObjectPool.Native;

/// <summary>
/// Default native memory pool policy
/// </summary>
/// <typeparam name="T"></typeparam>
public class NativePooledObjectPolicy<T> : PooledObjectPolicy<T> where T : notnull
{
    private Func<T> _createObject;
    private Action<T>? _resetObject;
    
    /// <summary>
    /// Creates a new instance of the <see cref="NativePooledObjectPolicy{T}"/> class.
    /// </summary>
    /// <param name="createObject"></param>
    /// <param name="resetObject"></param>
    public NativePooledObjectPolicy(Func<T> createObject, Action<T>? resetObject = null)
    {
        _createObject = createObject;
        _resetObject = resetObject;
    }

    /// <inheritdoc/>
    public override T Create() => _createObject();

    /// <inheritdoc/>
    public override bool Return(T obj)
    {
        _resetObject?.Invoke(obj);

        return true;
    }
}