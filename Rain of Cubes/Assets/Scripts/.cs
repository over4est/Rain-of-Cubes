using UnityEngine;

public class CubePool : MonoBehaviour
{
    [SerializeField] private int _poolCapacity;
    [SerializeField] private bool _autoExpand;
    [SerializeField] private Cube _prefab;

    private ObjectPool<Cube> _pool;

    private void Start()
    {
        _pool = new ObjectPool<Cube>(_prefab, _poolCapacity, transform);
    }
}
