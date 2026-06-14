using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;

public class ObjectPool<T> where T : IPoolable
{
    private List<T> activePool = new List<T>();
    private List<T> inactivePool = new List<T>();

    private T AddNewItemToPool()
    {
        T instance = (T)Activator.CreateInstance(typeof(T));
        inactivePool.Add(instance);
        return instance;
    }

    public T RequestObject()
    {
        if (inactivePool.Count > 0)
        {
            return ActivateItem(inactivePool[0]);
        }
        return ActivateItem(AddNewItemToPool());
    }

    public T ActivateItem(T item)
    {

        if (inactivePool.Contains(item))
        {
            inactivePool.Remove(item);
        }
        item.OnEnableObject();
        item.active = true;
        activePool.Add(item);
        return item;
    }

    public void ReturnObjectToPool(T item)
    {
        if(activePool.Contains(item)) activePool.Remove(item);
        item.OnDissableObject();
        item.active = false;
        inactivePool.Add(item);
    }
}