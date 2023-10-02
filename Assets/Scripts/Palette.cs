using UnityEngine;

public class Palette : MonoBehaviour
{
    public static Color active;
    [SerializeField] string colors;

    private void Start()
    {
        var gridWidth = 4;
        var tileSize = 1.5f;

        var startX = -gridWidth * tileSize / 2 + tileSize / 2;
        for (int x = 0; x < gridWidth; x++)
        {
            var color = colors[x] switch
            {
                '0' => Color.red,
                '1' => Color.cyan,
                '2' => Color.green,
                '3' => Color.yellow,
            };

            var position = new Vector2(startX + x * tileSize, 0);
            PaletteColor.Instant(position, color, transform);
        }

        active = transform.GetChild(0).GetComponent<PaletteColor>().Color;
        foreach(Transform t in transform)
        {
            var index = t.GetSiblingIndex();

            var indicator = t.GetChild(1).GetComponent<SpriteRenderer>();
            indicator.color = index == 0 ? Color.white : Color.gray;
        }
    }
}
