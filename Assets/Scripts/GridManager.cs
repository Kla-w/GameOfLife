using System;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject sprite;
    public float itemSize_X, itemSize_Y;
    public float padding_X, padding_Y;
    public int gridSize_X, gridSize_Y;
    public Cell[,] cells;

    private Vector3 GridScreenSize => new Vector3(
        gridSize_X * (2f * padding_X + itemSize_X),
        gridSize_Y * (2f * padding_Y + itemSize_Y),
        0);

    private void Start()
    {
        DestroyGrid();
        CreateGrid();
    }

    public void CreateGrid()
    {
        if (cells != null)
            DestroyGrid();

        cells = new Cell[gridSize_X, gridSize_Y];
        for (var i = 0; i < gridSize_X; i++)
        {
            for (var j = 0; j < gridSize_Y; j++)
            {
                var squarePosition = new Vector3(
                    padding_X + i * (itemSize_X + padding_X * 2),
                    padding_Y + j * (itemSize_Y + padding_Y * 2),
                    0) - GridScreenSize / 2f;

                var square = Instantiate(sprite,
                    squarePosition,
                    Quaternion.identity,
                    transform);

                square.name = $"X: {i} | Y: {j}";
                square.transform.localScale = new Vector3(itemSize_X, itemSize_Y, 1);
                var cell = square.GetComponent<Cell>();
                cell.grid = this;
                cells[i, j] = cell;
            }
        }
    }

    public void DestroyGrid()
    {
        var i = 0;

        //Array to hold all child obj
        var allChildren = new GameObject[transform.childCount];

        //Find all child obj and store to that array
        foreach (Transform child in transform)
        {
            allChildren[i] = child.gameObject;
            i += 1;
        }

        //Now destroy them
        foreach (var child in allChildren)
        {
            DestroyImmediate(child.gameObject);
        }

        cells = null;
    }

    public void ClearGrid()
    {
        for (var i = 0; i < gridSize_X; i++)
        {
            for (var j = 0; j < gridSize_Y; j++)
            {
                cells[i, j].Alive = false;
            }
        }
    }

    private void FixedUpdate()
    {
        tick();
    }

    private void tick()
    {
        var alive = new bool[gridSize_X, gridSize_Y];

        for (var i = 0; i < gridSize_X; i++)
        {
            for (var j = 0; j < gridSize_Y; j++)
            {
                alive[i, j] = ShouldCellBeAlive(i, j);
            }
        }

        for (var i = 0; i < gridSize_X; i++)
        {
            for (var j = 0; j < gridSize_Y; j++)
            {
                cells[i, j].Alive = alive[i, j];
            }
        }
    }


    bool IsCellAlive(int x, int y)
    {
        try
        {
            return cells[x, y].Alive;
        }
        catch (Exception _)
        {
            return false;
        }
    }

    bool ShouldCellBeAlive(int x, int y)
    {
        var c = 0;

        if (IsCellAlive(x - 1, y))
            c++;
        if (IsCellAlive(x + 1, y))
            c++;
        if (IsCellAlive(x, y - 1))
            c++;
        if (IsCellAlive(x, y + 1))
            c++;
        if (IsCellAlive(x - 1, y - 1))
            c++;
        if (IsCellAlive(x - 1, y + 1))
            c++;
        if (IsCellAlive(x + 1, y - 1))
            c++;
        if (IsCellAlive(x + 1, y + 1))
            c++;

        if (IsCellAlive(x, y))
        {
            //If cell is alive
            if (c <= 1) return false; //Dies of solitude
            if (c >= 4) return false; //Dies of overpopulation
            return true; //Else stay alive
        }

        //If cell is dead
        return c == 3; //Becomes alive if cell has 3 neighbours
    }
}