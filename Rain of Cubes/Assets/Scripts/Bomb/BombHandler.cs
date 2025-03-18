using System;
using System.Collections.Generic;
using UnityEngine;

public class BombHandler : SpawningObjectHandler
{
    [SerializeField] private CubeHandler _cubeHandler;

    public override event Action ObjectDispawned;
    public override event Action ObjectSpawned;

    protected override void ObjectsSubscribe(List<SpawningObject> objects)
    {
        foreach (Bomb bomb in objects)
        {
            bomb.LifeTimeEnded += Dispawn;
        }
    }

    protected override void ObjectsUnsubscribe(List<SpawningObject> objects)
    {
        foreach (Bomb bomb in objects)
        {
            bomb.LifeTimeEnded -= Dispawn;
        }
    }

    protected override void Spawn()
    {
        if (ObjectPool.TryGet(out SpawningObject obj))
        {
            SpawnedObjectsAmount++;
            ObjectSpawned?.Invoke();

            Bomb bomb = obj as Bomb;

            bomb.Renderer.material.color = bomb.StandartColor;

            bomb.StartTimer();
            Spawner.Spawn(bomb, _cubeHandler.LastCubePosition);
            ColorChanger.ChangeAlpha(bomb, bomb.LifeTime);
        }
    }

    protected override void Dispawn(SpawningObject @object)
    {
        ObjectDispawned?.Invoke();
        (@object as Bomb).Explode();
        base.Dispawn(@object);
    }

    private void OnEnable()
    {
        _cubeHandler.ObjectDispawned += Spawn;

        LifeTimeChanger.ChangeLifeTime(Objects);
        ObjectsSubscribe(Objects);
    }

    private void OnDisable()
    {
        _cubeHandler.ObjectDispawned -= Spawn;

        ObjectsUnsubscribe(Objects);
    }
}