using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpawnTimer), typeof(Positioner))]
public class CubeHandler : SpawningObjectHandler
{
    [Header("Spawn Timer")]
    [SerializeField] private float _spawnDelay;

    private Positioner _positioner;
    private SpawnTimer _spawnTimer;
    private bool _isCubesLifeTimeSet = false;

    public override event Action ObjectDispawned;
    public override event Action ObjectSpawned;

    public Vector3 LastCubePosition { get; private set; }

    protected override void Spawn()
    {
        if (ObjectPool.TryGet(out SpawningObject obj))
        {
            SpawnedObjectsAmount++;
            ObjectSpawned?.Invoke();

            Cube cube = obj as Cube;
            Vector3 spawnPosition = _positioner.GetPosition();

            Spawner.Spawn(cube, spawnPosition);
            ColorChanger.ChangeColor(cube, cube.StandartColor);
        }
    }

    protected override void ObjectsSubscribe(List<SpawningObject> cubes)
    {
        foreach (Cube cube in cubes)
        {
            cube.CollisionDetected += ChangeLifeTime;
            cube.CollisionDetected += ChangeColor;
            cube.LifeTimeEnded += Dispawn;
        }
    }

    protected override void ObjectsUnsubscribe(List<SpawningObject> cubes)
    {
        foreach (Cube cube in cubes)
        {
            cube.CollisionDetected -= ChangeLifeTime;
            cube.CollisionDetected -= ChangeColor;
            cube.LifeTimeEnded -= Dispawn;
        }
    }

    protected override void Dispawn(SpawningObject @object)
    {
        LastCubePosition = @object.transform.position;

        ObjectDispawned?.Invoke();
        base.Dispawn(@object);
    }

    private void OnEnable()
    {
        _positioner = GetComponent<Positioner>();
        _spawnTimer = GetComponent<SpawnTimer>();
        _spawnTimer.StartTimer(_spawnDelay);

        _spawnTimer.TimerTicked += Spawn;

        ObjectsSubscribe(Objects);
    }

    private void OnDisable()
    {
        _spawnTimer.TimerTicked -= Spawn;

        ObjectsUnsubscribe(Objects);
    }

    private void ChangeColor(Cube cube)
    {
        if (cube.HasStandartColor)
        {
            ColorChanger.ChangeColor(cube);
        }
    }

    private void ChangeLifeTime(Cube _)
    {
        if (_isCubesLifeTimeSet == false)
        {
            LifeTimeChanger.ChangeLifeTime(Objects);

            _isCubesLifeTimeSet = true;
        }
    }
}
