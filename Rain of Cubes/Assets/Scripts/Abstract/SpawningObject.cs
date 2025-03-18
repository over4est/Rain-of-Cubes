using System;
using UnityEngine;

[RequireComponent(typeof(DestroyTimer), typeof(Rigidbody), typeof(Renderer))]
public abstract class SpawningObject : MonoBehaviour
{
    private float _lifeTime;
    private Rigidbody _rigidbody;
    private Renderer _renderer;
    private DestroyTimer _timer;
    private Color _standartColor;

    public event Action<SpawningObject> LifeTimeEnded;

    public Rigidbody Rigidbody => _rigidbody;
    public Renderer Renderer => _renderer;

    public float LifeTime => _lifeTime;
    public Color StandartColor => _standartColor;
    protected DestroyTimer Timer => _timer;

    public void SetLifeTime(float value)
    {
        _lifeTime = value;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
        _timer = GetComponent<DestroyTimer>();
        _standartColor = Renderer.material.color;
    }

    private void OnEnable()
    {
        _timer.TimerTicked += RequestDisable;
    }

    private void OnDisable()
    {
        _timer.TimerTicked -= RequestDisable;
    }

    private void RequestDisable()
    {
        LifeTimeEnded?.Invoke(this);
    }
}