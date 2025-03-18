using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private T _prefab;
    private Transform _conteiner;
    private Stack<T> _pool;
    private List<T> _objects = new List<T>();

    public int ActiveObjectsAmount => _objects.Count - _pool.Count;

    public ObjectPool(T prefab, int objectCount, Transform container)
    {
        _prefab = prefab;
        _conteiner = container;

        CreatePool(objectCount);
    }

    public bool TryGet(out T obj)
    {
        obj = null;

        if (_pool.TryPop(out var element))
        {
            obj = element;
            element.gameObject.SetActive(true);

            return true;
        }

        return false;
    }

    public void Release(T obj)
    {
        obj.gameObject.SetActive(false);
        _pool.Push(obj);
    }

    public List<T> GetAllElements()
    {
        return new List<T>(_objects);
    }

    private void CreatePool(int count)
    {
        _pool = new Stack<T>();

        for (int i = 0; i < count; i++)
        {
            T obj = CreateObject();

            _pool.Push(obj);
        }
    }

    private T CreateObject(bool isActiveByDefault = false)
    {
        var newObject = Object.Instantiate(_prefab, _conteiner);

        _objects.Add(newObject);
        newObject.gameObject.SetActive(isActiveByDefault);

        return newObject;
    }
}