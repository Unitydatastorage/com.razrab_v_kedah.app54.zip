using UnityEngine;

public class PaletteColor : MonoBehaviour
{
    public Color Color
    {
        get => transform.GetChild(0).GetComponent<SpriteRenderer>().color;
        set => transform.GetChild(0).GetComponent<SpriteRenderer>().color = value;
    }

    public static void Instant(Vector2 position, Color color, Transform parent)
    {
        var go = Instantiate(Resources.Load<PaletteColor>("palettecolor"));
        go.transform.parent = parent;
        go.transform.localPosition = position;
        go.Color = color;
    }

    private void OnMouseDown()
    {
        Palette.active = Color;
        var colors = FindObjectsOfType<PaletteColor>();
        foreach(PaletteColor paletteColor in colors)
        {
            var index = transform.GetSiblingIndex();
            var otherIndex = paletteColor.transform.GetSiblingIndex();

            var indicator = paletteColor.transform.GetChild(1).GetComponent<SpriteRenderer>();
            indicator.color = index == otherIndex ? Color.white : Color.gray;
        }

        Sfx.Instant();
    }
}
