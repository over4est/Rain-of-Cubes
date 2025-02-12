using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public void ChangeColor(Cube cube)
    {
        Color randomColor = Random.ColorHSV();

        cube.Renderer.material.color = randomColor;
    }
}