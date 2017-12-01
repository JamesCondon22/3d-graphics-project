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
                    transform.localPosition = new Vector3(transform.localPosition.x - 1.0f, transform.localPosition.y, 4.5f);
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
            yield return new WaitForSeconds(waitMove);
        }

    }
    //rotate 90 degree on y axis 
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            transform.Rotate(0, 0, 90);
        }

        if (Input.GetKeyDown("down"))
        {
            dropping = true;
        }

    }
}



