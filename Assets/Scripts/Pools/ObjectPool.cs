// Assets/Scripts/Services/ObjectPool.cs
using System;
using System.Collections.Generic;

/// <summary>
/// Generic object pool for reuse of GameObjects or other classes.
/// </summary>
public class ObjectPool<T> where T : class
{
    private readonly Func<T> _create;
    private readonly Action<T> _onGet;
    private readonly Action<T> _onRelease;
    private readonly Stack<T> _stack = new Stack<T>();

    public ObjectPool(Func<T> create, Action<T> onGet, Action<T> onRelease)
    {
        _create = create;
        _onGet = onGet;
        _onRelease = onRelease;
    }

    public T Get()
    {
        var item = _stack.Count > 0 ? _stack.Pop() : _create();
        _onGet(item);
        return item;
    }

    public void Release(T item)
    {
        _onRelease(item);
        _stack.Push(item);
    }
}