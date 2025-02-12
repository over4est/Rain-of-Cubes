using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private Color _standartColor;

    private DestroyTimer _timer;
    private float _lifeTime;

    public event Action<Cube> CollisionDetected;
    public event Action<Cube> LifeTimeEnded;

    public bool HasStandartColor => _standartColor == Renderer.material.color;
    public Rigidbody Rigidbody => GetComponent<Rigidbody>();
    public Renderer Renderer => GetComponent<Renderer>();

    private void Awake()
    {
        _timer = GetComponent<DestroyTimer>();
    }

    private void OnEnable()
    {
        Renderer.material.color = _standartColor;
        _timer.TimerTicked += RequestDisable;
    }

    private void OnDisable()
    {
        _timer.TimerTicked -= RequestDisable;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Platform>(out Platform _))
        {
            CollisionDetected?.Invoke(this);
            _timer.StartTimer(_lifeTime);
        }
    }

    public void SetLifeTime(float value)
    {
        _lifeTime = value;
    }

    private void RequestDisable()
    {
        LifeTimeEnded?.Invoke(this);
    }
}