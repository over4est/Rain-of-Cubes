using UnityEngine;

public class Spawner : MonoBehaviour
{
    public void Spawn(SpawningObject obj, Vector3 position)
    {
        obj.transform.position = position;
    }
}