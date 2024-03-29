﻿namespace ObjectPool;

/// <summary>
/// An interface to be implemented by objects that can be pooled.
/// </summary>
public interface IPoolable
{
    /// <summary>
    /// Implements the logic to reset the object to its initial state.
    /// It will be called by the Pool itself, no need to call it explicitly
    /// </summary>
    void Reset();

    /// <summary>
    /// Invoke this action to return object to Pool
    /// </summary>
    Action ReturnToPool { get; set; }
}