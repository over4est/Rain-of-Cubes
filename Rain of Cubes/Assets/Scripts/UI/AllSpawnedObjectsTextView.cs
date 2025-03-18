public class AllSpawnedObjectsTextView : TextView
{
    protected override void ChangeText()
    {
        Text.text = SpawningObjectHandler.SpawnedObjectsAmount.ToString();
    }

    private void Start()
    {
        ChangeText();
    }

    private void OnEnable()
    {
        SpawningObjectHandler.ObjectSpawned += ChangeText;
    }

    private void OnDisable()
    {
        SpawningObjectHandler.ObjectSpawned -= ChangeText;
    }
}