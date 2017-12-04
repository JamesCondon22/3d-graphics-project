using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shapemovement : MonoBehaviour
{

    public float waitMove;

    private bool moveLeft;
    private bool moveRight;
    //made public because it will be set to false when a new shape is spawned 
    public bool dropping;

    private float fall = 0;
    public float fallSpeed = 1;


    public bool allowRotation = true;
    public bool limitRotation = false;

    public bool move = true;

    // Use this for initialization
    void Start()
    {

    }


    //rotate 90 degree on y axis 
    void Update()
    {
        if (enabled)
        {
            CheckUserInput();
        }


    }

    bool CheckIsValidPosition ()
    {
        foreach (Transform mino in transform)
        {
            Vector2 pos = FindObjectOfType<GameController>().Round(mino.position);

            //pos.y -= 1.0f;
            //pos.y = mino.parent.parent.localPosition.y;
            //Debug.Log(mino.parent.name + ": " + pos.y);
            

            if (FindObjectOfType<GameController>().CheckIsInsideGrid(pos) == false)
            {
                Debug.Log(mino.parent.name + " not in grid");
                return false;
            }

            if(FindObjectOfType<GameController>().GetTransformAtGridPosition(pos) != null && FindObjectOfType<GameController>().GetTransformAtGridPosition(pos).parent != transform)
            {

                Debug.Log(mino.parent.name + " error");
                return false;
            }

            
        }
        return true;
    }

    bool CheckIfGoingToCollide()
    {

        foreach (Transform mino in transform)
        {

            if (FindObjectOfType<GameController>().CheckNextPos(mino))
            {
                return false;
            }

        }
        return true;
    }

    void CheckUserInput()
    {

        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (CheckIsValidPosition())
            {
                FindObjectOfType<GameController>().UpdateGrid(this);
            }
            else
            {
                transform.localPosition = new Vector3(transform.localPosition.x - 1.0f, transform.localPosition.y, 0);
            }
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (CheckIsValidPosition())
            {
                FindObjectOfType<GameController>().UpdateGrid(this);
            }
            else
            {
                transform.localPosition = new Vector3(transform.localPosition.x + 1.0f, transform.localPosition.y, 0);
            }
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (allowRotation)
            {
                if (limitRotation)
                {
                    if (transform.rotation.eulerAngles.z >= 90)
                    {
                        transform.Rotate(0, 0, -90);
                    }
                    else
                    {
                        transform.Rotate(0, 0, 90);
                    }
                }
                else
                {
                    transform.Rotate(0, 0, 90);
                }
                if (CheckIsValidPosition())
                {
                    FindObjectOfType<GameController>().UpdateGrid(this);
                }
                else
                {

                    if (limitRotation)
                    {
                        if (transform.rotation.eulerAngles.z >= 90)
                        {
                            transform.Rotate(0, 0, -90);
                        }
                        else
                        {
                            transform.Rotate(0, 0, 90);
                        }
                    }
                    else
                    {
                        transform.Rotate(0, 0, -90);
                    }
                }
            }

        }
        else if(Input.GetKeyDown(KeyCode.DownArrow) || Time.time - fall >= fallSpeed)
        {
            fall = Time.time;
            transform.position += new Vector3(0, -1, 0);

            if (CheckIsValidPosition() && CheckIfGoingToCollide())
            {
                //transform.position += new Vector3(0, -1, 0);
                FindObjectOfType<GameController>().UpdateGrid(this);
                Debug.Log("Update");
            }
            else
            {
                transform.position += new Vector3(0, 1, 0);
                FindObjectOfType<GameController>().DeleteRow();
                FindObjectOfType<GameController>().SpawnNextTetrimino();
                enabled = false;
                Debug.Log("Delete row");
            }
        }
    }
}