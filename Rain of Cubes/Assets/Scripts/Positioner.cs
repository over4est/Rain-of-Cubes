using UnityEngine;

public class Positioner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _offset;

    private Vector3 _position;

    private void Start()
    {
        _position = _spawnPoint.position;
    }

    public Vector3 GetPosition()
    {
        float xPosition = Random.Range(_position.x - _offset, _position.x + _offset);
        float yPosition = _position.y;
        float zPosition = Random.Range(_position.z - _offset, _position.z + _offset);
        Vector3 point = new Vector3(xPosition, yPosition, zPosition);

        return point;
    }
}