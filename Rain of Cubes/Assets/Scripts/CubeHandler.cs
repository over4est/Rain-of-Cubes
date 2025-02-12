using System.Collections.Generic;
using UnityEngine;

public class CubeHandler : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private ColorChanger _colorChanger;
    [SerializeField] private LifeTimeChanger _lifeTimeChanger;

    [Header("Spawn Timer")]
    [SerializeField] private SpawnTimer _spawnTimer;
    [SerializeField] private float _spawnRate;

    [Header("Pool Settings")]
    [SerializeField] private int _poolSize;
    [SerializeField] private Transform _container;
    [SerializeField] private Cube _prefab;

    private ObjectPool<Cube> _pool;
    private List<Cube> _cubes;
    private bool _isCubesLifeTimeSet = false;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(_prefab, _poolSize, _container);
        _cubes = _pool.GetAllElements();
    }

    private void OnEnable()
    {
        _spawnTimer.StartTimer(_spawnRate);
        _spawnTimer.TimerTicked += Spawn;

        CubeSubscribe(_cubes);
    }

    private void OnDisable()
    {
        _spawnTimer.TimerTicked -= Spawn;

        CubeUnsubscribe(_cubes);
    }

    private void Spawn()
    {
        Cube cube = _pool.Get();

        if (cube != null)
        {
            _spawner.Spawn(cube);
        }
    }

    private void ChangeLifeTime(Cube _)
    {
        if (_isCubesLifeTimeSet == false)
        {
            _lifeTimeChanger.ChangeLifeTime(_cubes);

            _isCubesLifeTimeSet = true;
        }
    }

    private void Dispawn(Cube cube)
    {
        _pool.Release(cube);
    }

    private void ChangeColor(Cube cube)
    {
        if (cube.HasStandartColor)
        {
            _colorChanger.ChangeColor(cube);
        }
    }

    private void CubeSubscribe(List<Cube> cubes)
    {
        foreach (Cube cube in cubes)
        {
            cube.CollisionDetected += ChangeLifeTime;
            cube.CollisionDetected += ChangeColor;
            cube.LifeTimeEnded += Dispawn;
        }
    }

    private void CubeUnsubscribe(List<Cube> cubes)
    {
        foreach (Cube cube in cubes)
        {
            cube.CollisionDetected -= ChangeLifeTime;
            cube.CollisionDetected -= ChangeColor;
            cube.LifeTimeEnded -= Dispawn;
        }
    }
}
