using System;
using UnityEngine;

[RequireComponent(typeof(DestroyTimer), typeof(Rigidbody), typeof(Renderer))]
public class Cube : SpawningObject
{

    public event Action<Cube> CollisionDetected;

    public bool HasStandartColor => StandartColor == Renderer.material.color;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Platform>(out Platform _))
        {
            CollisionDetected?.Invoke(this);
            Timer.StartTimer(LifeTime);
        }
    }
}