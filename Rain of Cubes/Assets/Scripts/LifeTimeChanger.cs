using System.Collections.Generic;
using UnityEngine;

public class LifeTimeChanger : MonoBehaviour
{
    [SerializeField] private float _minLifeTime;
    [SerializeField] private float _maxLifeTime;

    public void ChangeLifeTime(List<SpawningObject> objects)
    {
        foreach (SpawningObject cube in objects)
        {
            float randomLifeTime = Random.Range(_minLifeTime, _maxLifeTime);

            cube.SetLifeTime(randomLifeTime);
        }
    }
}