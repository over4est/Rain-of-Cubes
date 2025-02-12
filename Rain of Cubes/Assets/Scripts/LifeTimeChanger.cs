using System.Collections.Generic;
using UnityEngine;

public class LifeTimeChanger : MonoBehaviour
{
    [SerializeField] private float _minLifeTime;
    [SerializeField] private float _maxLifeTime;

    public void ChangeLifeTime(List<Cube> cubes)
    {
        foreach (Cube cube in cubes)
        {
            float randomLifeTime = Random.Range(_minLifeTime, _maxLifeTime);

            cube.SetLifeTime(randomLifeTime);
        }
    }
}