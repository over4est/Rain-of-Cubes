public class CreatedObjectsAmountTextView : TextView
{
    protected override void ChangeText()
    {
        Text.text = SpawningObjectHandler.CreatedObjectsAmount.ToString();
    }

    private void Start()
    {
        ChangeText();
    }
}