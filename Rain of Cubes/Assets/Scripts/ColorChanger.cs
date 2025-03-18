using System.Collections;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public void ChangeColor(Cube cube)
    {
        Color randomColor = Random.ColorHSV();

        cube.Renderer.material.color = randomColor;
    }

    public void ChangeColor(Cube cube, Color color)
    {
        cube.Renderer.material.color = color;
    }

    public void ChangeAlpha(Bomb bomb, float time)
    {
        StartCoroutine(SmoothAlphaChange(bomb, time));
    }

    private IEnumerator SmoothAlphaChange(Bomb bomb, float time)
    {
        float elapsedTime = 0f;
        float targetAlpha = 0f;
        float startPoint = bomb.Renderer.material.color.a;

        while (elapsedTime < time)
        {
            float delta = elapsedTime / time;
            float alpha = Mathf.Lerp(startPoint, targetAlpha, delta);

            bomb.Renderer.material.color = new Color(bomb.Renderer.material.color.r, bomb.Renderer.material.color.g, bomb.Renderer.material.color.b, alpha);
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
}