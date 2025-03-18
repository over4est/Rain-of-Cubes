using UnityEngine;

[RequireComponent(typeof(DestroyTimer), typeof(Renderer), typeof(Rigidbody))]
public class Bomb : SpawningObject
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    public void StartTimer()
    {
        Timer.StartTimer(LifeTime);
    }

    public void Explode()
    {
        int maxTargetsCount = 20;
        Collider[] targets = new Collider[maxTargetsCount];

        if (Physics.OverlapSphereNonAlloc(transform.position, _explosionRadius, targets) > 0)
        {
            foreach (Collider target in targets)
            {
                if (target != null && target.TryGetComponent(out Rigidbody rigidbody))
                {
                    rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
                }
            }
        }
    }
}