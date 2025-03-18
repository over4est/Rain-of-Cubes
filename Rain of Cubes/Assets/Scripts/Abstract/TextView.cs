using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public abstract class TextView : MonoBehaviour
{
    [SerializeField] private SpawningObjectHandler _objectHandler;

    private TextMeshProUGUI _text;

    protected SpawningObjectHandler SpawningObjectHandler => _objectHandler;
    protected TextMeshProUGUI Text => _text;

    protected abstract void ChangeText();

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }
}