using UnityEngine;

public class Cell : MonoBehaviour
{
    public Color Color
    {
        get => GetComponent<SpriteRenderer>().color;
        set => GetComponent<SpriteRenderer>().color = value;
    }

    public Color TargetColor { get; set; }

    private float duration = 3.0f;
    public float EimeElapsed { get; set; }

    public static Cell Instant(Vector2 position, Color color, Transform parent)
    {
        var go = Instantiate(Resources.Load<Cell>("cell"));
        go.transform.parent = parent;
        go.transform.localPosition = position;
        go.TargetColor = color;
        return go;
    }

    private void Update()
    {
        EimeElapsed += Time.deltaTime;

        if (EimeElapsed >= duration)
        {
            EimeElapsed = duration;
        }

        float t = EimeElapsed / duration;
        Color = Color.Lerp(Color, TargetColor, t);
    }

    private void OnMouseDown()
    {
        Sfx.Instant();
        TargetColor = Palette.active;
        Level.CheckFinishGame();
        EimeElapsed = 0.0f;
    }
}
