public class ActiveObjectsAmountTextView : TextView
{
    protected override void ChangeText()
    {
        Text.text = SpawningObjectHandler.ActiveObjectsAmount.ToString();
    }

    private void Start()
    {
        ChangeText();
    }

    private void OnEnable()
    {
        SpawningObjectHandler.ObjectSpawned += ChangeText;
        SpawningObjectHandler.ObjectDispawned += ChangeText;
    }

    private void OnDisable()
    {
        SpawningObjectHandler.ObjectSpawned -= ChangeText;
        SpawningObjectHandler.ObjectDispawned -= ChangeText;
    }
}