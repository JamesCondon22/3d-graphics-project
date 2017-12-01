using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static int gridWidth = 10;
    public static int gridHeight = 20;

    public static Transform[,] grid = new Transform[gridWidth, gridHeight];

    public GameObject tShape;
    public GameObject lShape;
    public GameObject mirroredLShape;
    public GameObject lineShape;
    public GameObject squareShape;
    public GameObject sShape;
    public GameObject zShape;
    public float wait;
    


    // Use this for initialization
    void Start () {
        StartCoroutine(Spawn());
        //SpawnNextTetromine();
    }
	

    public bool CheckIsInsideGrid(Vector2 pos)
    {
        return ((int)pos.x > 0 && (int)pos.x < gridWidth && (int)pos.y > 0);
    }

    public Vector2 Round(Vector2 pos)
    {
        return new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));
    }

    public bool IsFullRowAt(int y)
    {
        for (int x = 0; x < gridWidth; x++)
        {
            if(grid[x, y] == null)
            {
                return false;
            }
        }

        return true;
    }

    public void MoveRowDown(int y)
    {
        for(int x = 0; x < gridWidth; x++)
        {
            if(grid[x,y] != null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    public void MoveAllRowsDown(int y)
    {
        for (int i = y; i < gridHeight; i++)
        {
            MoveRowDown(i);
        }
    }

    public void DeleteMinoAt(int y)
    {
        for (int x = 0; x < gridWidth; x++)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    public void DeleteRow()
    {
        for(int y = 0; y < gridHeight; y++)
        {
            if(IsFullRowAt(y))
            {
                DeleteMinoAt(y);
                MoveAllRowsDown(y + 1);
                y--;
            }
        }
    }

    public void UpdateGrid(Shapemovement tetromino)
    {
        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                if(grid[x, y] != null)
                {
                    if(grid[x,y].parent == tetromino.transform)
                    {
                        grid[x, y] = null;
                    }
                }
            }

        }
        
        foreach (Transform mino in tetromino.transform)
        {
            Vector2 pos = new Vector2(Mathf.Round(mino.position.x), Mathf.Round(mino.position.y));
            if(pos.y < gridHeight)
            {
                grid[(int)pos.x, (int)pos.y] = mino;
            }
        }
    }

    public Transform GetTransformAtGridPosition(Vector2 pos)
    {
        if (pos.y < gridHeight - 1)
        {
            return null;
        }
        else
        {
            return grid[(int)pos.x, (int)pos.y];
        }
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(wait);
        while (true)
        {
            int whichPiece = Random.Range(0, 7);
            if (whichPiece == 0)
            {
                Instantiate(tShape);
            }
            else if (whichPiece == 1)
            {
                Instantiate(lShape);
            }
            else if (whichPiece == 2)
            {
                Instantiate(mirroredLShape);
            }
            else if (whichPiece == 3)
            {
                Instantiate(lineShape);
            }
            else if (whichPiece == 4)
            {
                Instantiate(squareShape);
            }
            else if (whichPiece == 5)
            {
                Instantiate(sShape);
            }
            else
            {
                Instantiate(zShape);
            }

            yield return new WaitForSeconds(wait);
        }
    }
}
