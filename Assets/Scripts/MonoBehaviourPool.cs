using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class MonoBehaviourPool<T> where T : Component
{
    public ReadOnlyCollection<T> UsedItems { get; private set; }
    
    private readonly List<T> _notUsedItems =  new List<T>();
    private readonly List<T> _usedItems = new List<T>();
    
    private readonly T _prefab;
    private readonly Transform _parent;

    public MonoBehaviourPool(T prefab, Transform parent, int defaultCount = 4)
    {
        _prefab = prefab;
        _parent = parent;
        
        for (var i = 0; i < defaultCount; i++)
        {
            AddNewItemInPool();
        }

        UsedItems = new ReadOnlyCollection<T>(_usedItems);
    }

    private void AddNewItemInPool()
    {
        var newItem = Object.Instantiate(_prefab, _parent, false);
        newItem.gameObject.SetActive(false);
        _notUsedItems.Add(newItem);
    }

    public T Take()
    {
        if (_notUsedItems.Count == 0)
        {
            AddNewItemInPool();
        }

        var lastIndex = _notUsedItems.Count - 1;
        var itemFromPool = _notUsedItems[lastIndex];
        _notUsedItems.RemoveAt(lastIndex);
        _usedItems.Add(itemFromPool);
        
        itemFromPool.gameObject.SetActive(true);

        return itemFromPool;
    }

    public void Release(T item)
    {
        item.gameObject.SetActive(false);
        _usedItems.Remove(item);
        _notUsedItems.Add(item);
    }

    public void ReleaseAll()
    {
        for (var i = 0; i < _usedItems.Count; i++)
        {
            _usedItems[i].gameObject.SetActive(false);
        }
        
        _notUsedItems.AddRange(_usedItems);
        _usedItems.Clear();
    }
}