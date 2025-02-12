using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Positioner _positioner;

    public void Spawn(Cube cube)
    {
        cube.transform.position = _positioner.GetPosition();
    }
}