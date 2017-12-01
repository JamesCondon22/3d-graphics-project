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


    public bool allowRotation = true;
    public bool limitRotation = false;

    // Use this for initialization
    void Start()
    {

        moveLeft = true;
        moveRight = true;

        StartCoroutine(MoveDown());
    }



    // FixedUpdate is called at a fixed interval and is independent of frame rate.
    void FixedUpdate()
    {
        //Store the current horizontal input in the float moveHorizontal
        float moveHorizontal = Input.GetAxis("Horizontal");

        if (dropping == false)
        {
            if (moveHorizontal > 0)
            {
                if (moveRight == true)
                {
                    transform.localPosition = new Vector3(transform.localPosition.x + 1.0f, transform.localPosition.y, transform.localPosition.z);

                    if (CheckIsValidPosition())
                    {
                        
                    }
                    else
                    {
                        transform.localPosition = new Vector3(transform.localPosition.x - 1.0f, transform.localPosition.y, transform.localPosition.z);
                    }
                }
                moveRight = false;
            }
            else
            {
                moveRight = true;
            }

            if (moveHorizontal < 0)
            {
                if (moveLeft == true)
                {
                    transform.localPosition = new Vector3(transform.localPosition.x - 1.0f, transform.localPosition.y, transform.localPosition.z);
                    if (CheckIsValidPosition())
                    {

                    }
                    else
                    {
                        transform.localPosition = new Vector3(transform.localPosition.x + 1.0f, transform.localPosition.y, transform.localPosition.z);
                    }
                }
                moveLeft = false;
            }
            else
            {
                moveLeft = true;
            }
        }

        if (dropping)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - 1.0f, transform.localPosition.z);
        }
    }

    IEnumerator MoveDown()
    {
        yield return new WaitForSeconds(waitMove);

        while (true)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - 1.0f, transform.localPosition.z);
            if (CheckIsValidPosition())
            {

            }
            else
            {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 1.0f, transform.localPosition.z);
            }
            yield return new WaitForSeconds(waitMove);
        }

    }
    //rotate 90 degree on y axis 
    void Update()
    {
        if (Input.GetKeyDown("up"))
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
                if (CheckIsValidPosition()){
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
        if (Input.GetKeyDown("space"))
        {
            dropping = true;
        }

        if (Input.GetKeyDown("down"))
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - 1.0f, transform.localPosition.z);
            if (CheckIsValidPosition())
            {

            }
            else
            {
                transform.localPosition = new Vector3(transform.localPosition.x , transform.localPosition.y + 1.0f, transform.localPosition.z);
            }
        }

    }

    bool CheckIsValidPosition ()
    {
        foreach (Transform mino in transform)
        {
            Vector2 pos = FindObjectOfType<GameController>().Round(mino.position);

            if (FindObjectOfType<GameController>().CheckIsInsideGrid(pos) == false)
            {
                return false;
            }
        }
        return true;
    }
}



