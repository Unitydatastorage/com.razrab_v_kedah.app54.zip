using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Linq;

public class Level : MonoBehaviour
{
    private static List<Cell> leftCells = new List<Cell>();
    private static List<Cell> rightCells = new List<Cell>();

    private IEnumerator Start()
    {
        var gridWidth = 5;
        var gridHeight = 5;

        var tileSize = 1.5f;

        var startX = -gridWidth * tileSize / 2 + tileSize / 2;
        var startY = -gridHeight * tileSize / 2 + tileSize / 2;

        var leftParent = GameObject.Find("left").transform;
        var leftCoroutine = StartCoroutine(CreateGrid(gridWidth, gridHeight, tileSize, startX, startY, leftParent, leftCells));

        var rightParent = GameObject.Find("right").transform;
        var rightCoroutine = StartCoroutine(CreateGrid(gridWidth, gridHeight, tileSize, startX, startY, rightParent, rightCells));

        yield return leftCoroutine;
        yield return rightCoroutine;
    }

    private static bool CheckCellsEquals()
    {
        return leftCells.Select(cell => cell.TargetColor).SequenceEqual(rightCells.Select(cell => cell.TargetColor));
    }

    public static void CheckFinishGame()
    {
        if(CheckCellsEquals())
        {
            ReinstantGame();
        }
    }

    private IEnumerator CreateGrid(int gridWidth, int gridHeight, float tileSize, float startX, float startY, Transform parent, List<Cell> cells)
    {
        for (int y = gridHeight - 1; y >= 0; y--)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                var isEven = (x + y) % 2 == 0;
                var color = string.Equals(parent.name, "left") ?  (isEven ? Color.white : Color.gray) : Random.Range(0, 4).ToString()[0] switch
                {
                    '0' => Color.red,
                    '1' => Color.cyan,
                    '2' => Color.green,
                    '3' => Color.yellow,
                };

                var position = new Vector2(startX + x * tileSize, startY + y * tileSize);
                var cell = Cell.Instant(position, color, parent);

                if (string.Equals(parent.name, "left"))
                {
                    cell.gameObject.AddComponent<BoxCollider2D>();
                }

                cells.Add(cell);
                yield return new WaitForSeconds(0.05f);
            }
        }
    }

    public static void ReinstantGame()
    {
        WinVfx.Instant();

        foreach(Cell cell in leftCells)
        {
            var isEven = cell.transform.GetSiblingIndex() % 2 == 0;
            cell.TargetColor = isEven ? Color.white : Color.gray;
            cell.EimeElapsed = 0;
        }

        foreach (Cell cell in rightCells)
        {
            cell.TargetColor = Random.Range(0, 4).ToString()[0] switch
            {
                '0' => Color.red,
                '1' => Color.cyan,
                '2' => Color.green,
                '3' => Color.yellow,
            };

            cell.EimeElapsed = 0;
        }

        CameraShake.Make();
    }
}
