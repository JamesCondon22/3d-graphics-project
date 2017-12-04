using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static int gridWidth = 11;
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


    private GameObject previewTetrimino;
    private GameObject nextTetrimino;

    private bool gameStarted = false;
    private Vector2 previewPosition = new Vector2(-6.5f, 15);


    // Use this for initialization
    void Start () {
        SpawnNextTetrimino();
    }
	

    public bool CheckIsInsideGrid(Vector2 pos)
    {
        //Debug.Log(pos.y);
        return ((int)pos.x > 0 && (int)pos.x < gridWidth && (int)pos.y > 0);
    }
    

    public Vector2 Round(Vector2 pos)
    {
        return new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));
    }

    public bool IsFullRowAt(int y)
    {
        //Debug.Log(y);
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
            //Debug.Log(IsFullRowAt(y));
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
                

                if (grid[x, y] != null)
                {
                    
                    if (grid[x,y].parent == tetromino.transform)
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
                //Debug.Log("Mino y pos: " + pos.y);
                grid[(int)pos.x, (int)pos.y] = mino;
                //Debug.Log(grid[(int)pos.x, (int)pos.y]);
                //Debug.Log("X: " + pos.x + "Y: " + pos.y);
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

    public bool CheckNextPos(Transform mino)
    {
        Vector2 pos = Round(mino.position);

        if (grid[(int)pos.x, (int)pos.y] == null )
        {
            return false;
        }
        else if (grid[(int)pos.x, (int)pos.y].parent == mino.parent)
        {
            return false;
        }

        Debug.Log(grid[(int)pos.x, (int)pos.y].parent + " " + mino.parent);

        return true;
    }
    
    public void SpawnNextTetrimino()
    {
        if (!gameStarted)
        {
            gameStarted = true;
            nextTetrimino = (GameObject)Instantiate(Resources.Load(GetRandomTetrimino(), typeof(GameObject)), new Vector2(5.0f, 20.0f), Quaternion.identity);
            previewTetrimino = (GameObject)Instantiate(Resources.Load(GetRandomTetrimino(), typeof(GameObject)), previewPosition,Quaternion.identity);
            previewTetrimino.GetComponent<Shapemovement>().enabled = false;
        }
    
        else
        {
            previewTetrimino.transform.localPosition = new Vector2(5.0f, 20.0f);
            nextTetrimino = previewTetrimino;
            nextTetrimino.GetComponent<Shapemovement>().enabled = true;

            previewTetrimino = (GameObject) Instantiate(Resources.Load(GetRandomTetrimino(), typeof(GameObject)), previewPosition, Quaternion.identity);
            previewTetrimino.GetComponent<Shapemovement>().enabled = false;
        }

    }

    string GetRandomTetrimino()
    {
        int random = Random.Range(1, 8);

        string randomTetName = "Prefabs/Line2";

        switch (random) {

            case 1:
                randomTetName = "Prefabs/Line2";
                break;

            case 2:
                randomTetName = "Prefabs/Square2";
                break;
            case 3:
                randomTetName = "Prefabs/L_Shape";
                break;

            case 4:
                randomTetName = "Prefabs/L_Shape(mirrored)";
                break;
            case 5:
                randomTetName = "Prefabs/S_Shape";
                break;

            case 6:
                randomTetName = "Prefabs/T_Shape";
                break;
            case 7:
                randomTetName = "Prefabs/Z_Shape";
                break;
        }

        return randomTetName;

    }
}
