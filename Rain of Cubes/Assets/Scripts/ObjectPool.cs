using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private T _prefab;
    private Transform _conteiner;
    private List<T> _pool;
    private Rigidbody _standartRigidbody;

    public ObjectPool(T prefab, int objectCount, Transform container)
    {
        _prefab = prefab;

        if (prefab.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
        {
            _standartRigidbody = rigidbody;
        }

        _conteiner = container;

        CreatePool(objectCount);
    }

    public T Get()
    {
        if (HasFreeElement(out var element))
        {
            element.gameObject.SetActive(true);

            return element;
        }

        return null;
    }

    public void Release(T element)
    {
        if (_standartRigidbody != null)
        {
            Rigidbody elementRigidbody = element.GetComponent<Rigidbody>();

            elementRigidbody.velocity = _standartRigidbody.velocity;
            elementRigidbody.angularVelocity = _standartRigidbody.angularVelocity;
        }

        element.transform.rotation = _prefab.transform.rotation;
        element.gameObject.SetActive(false);
    }

    public List<T> GetAllElements()
    {
        return new List<T>(_pool);
    }

    private bool HasFreeElement(out T element)
    {
        foreach (var obj in _pool)
        {
            if (obj.gameObject.activeInHierarchy == false)
            {
                element = obj;

                return true;
            }
        }

        element = null;

        return false;
    }

    private void CreatePool(int count)
    {
        _pool = new List<T>();

        for (int i = 0; i < count; i++)
        {
            T obj = CreateObject();

            _pool.Add(obj);
        }
    }

    private T CreateObject(bool isActiveByDefault = false)
    {
        var newObject = Object.Instantiate(_prefab, _conteiner);

        newObject.gameObject.SetActive(isActiveByDefault);

        return newObject;
    }
}