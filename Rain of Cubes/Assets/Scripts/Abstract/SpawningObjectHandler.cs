using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawningObjectHandler : MonoBehaviour
{
    [Header("Pool Settings")]
    [SerializeField] private int _poolSize;
    [SerializeField] private Transform _container;
    [SerializeField] private SpawningObject _prefab;

    private ColorChanger _colorChanger;
    private LifeTimeChanger _lifeTimeChanger;
    private ObjectPool<SpawningObject> _pool;
    private List<SpawningObject> _objects;
    private Spawner _spawner;

    public virtual event Action ObjectDispawned;
    public virtual event Action ObjectSpawned;

    public int SpawnedObjectsAmount { get; protected set; } = 0;
    public int CreatedObjectsAmount => _objects.Count;
    public int ActiveObjectsAmount => _pool.ActiveObjectsAmount;

    protected List<SpawningObject> Objects => new List<SpawningObject>(_objects);
    protected ObjectPool<SpawningObject> ObjectPool => _pool;
    protected LifeTimeChanger LifeTimeChanger => _lifeTimeChanger;
    protected ColorChanger ColorChanger => _colorChanger;
    protected Spawner Spawner => _spawner;

    protected abstract void Spawn();

    protected abstract void ObjectsSubscribe(List<SpawningObject> objects);

    protected abstract void ObjectsUnsubscribe(List<SpawningObject> objects);

    protected virtual void Dispawn(SpawningObject @object)
    {
        _pool.Release(@object);
    }

    private void Awake()
    {
        _colorChanger = GetComponentInParent<ColorChanger>();
        _lifeTimeChanger = GetComponentInParent<LifeTimeChanger>();
        _spawner = GetComponentInParent<Spawner>();
        _pool = new ObjectPool<SpawningObject>(_prefab, _poolSize, _container);
        _objects = _pool.GetAllElements();
    }
}