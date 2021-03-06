﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shapemovement : MonoBehaviour
{

    public float waitMove;

    private bool moveLeft;
    private bool moveRight;
    //made public because it will be set to false when a new shape is spawned 
    private bool next = true;
    public bool dropping = false;

    private float fall = 0;
    public float fallSpeed = 1;


    public bool allowRotation = true;
    public bool limitRotation = false;

    public bool move = true;


    //game sounds
    public AudioClip rightMove;
    public AudioClip leftMove;
    public AudioClip rotate;

    private AudioSource audioSource;

    private bool gameover = false;


    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    //rotate 90 degree on y axis 
    void Update()
    {
        if (enabled)
        {
            CheckUserInput();
        }
        Debug.Log(this.tag);
        
        if (this.tag == "Bomb" && !CheckIsValidPosition())
        {
            Debug.Log("HI");
            FindObjectOfType<GameController>().DeleteMinoAt((int)transform.position.y);
        }
     

        foreach (Transform mino in transform)
        {
            Vector2 pos = FindObjectOfType<GameController>().Round(mino.position);

            if (pos.y >= 18 && (pos.x > 8 || pos.x >= 0 && pos.x < 5) && !enabled && gameover == false)
            {
                Debug.Log("Game over");
                gameover = true;
                FindObjectOfType<GameController>().GameOver();
            }
        }
    }

    void  PlayMoveLeftAudio()
    {
        audioSource.PlayOneShot(leftMove);
    }
    void PlayMoveRightAudio()
    {
        audioSource.PlayOneShot(rightMove);
    }
    void PlayRotateAudio()
    {
        audioSource.PlayOneShot(rotate);
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
                return false;
            }
            else if(FindObjectOfType<GameController>().GetTransformAtGridPosition(pos) != null && FindObjectOfType<GameController>().GetTransformAtGridPosition(pos).parent != transform)
            {
                
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

    bool CheckMoveLeft()
    {
        foreach (Transform mino in transform)
        {

            if (FindObjectOfType<GameController>().CheckLeft(mino))
            {
                return false;
            }

        }
        return true;
    }

    bool CheckMoveRight()
    {
        foreach (Transform mino in transform)
        {

            if (FindObjectOfType<GameController>().CheckRight(mino))
            {
                return false;
            }

        }
        return true;
    }

    void CheckUserInput()
    {

        if(Input.GetKeyDown(KeyCode.RightArrow) && CheckMoveRight())
        {
            transform.position += new Vector3(1, 0, 0);
            if (CheckIsValidPosition())
            {
                FindObjectOfType<GameController>().UpdateGrid(this);
                PlayMoveRightAudio();
            }
            else
            {
                transform.localPosition = new Vector3(transform.localPosition.x - 1.0f, transform.localPosition.y, 0);
            }
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow) && CheckMoveLeft())
        {
            transform.position += new Vector3(-1, 0, 0);
            if (CheckIsValidPosition())
            {
                FindObjectOfType<GameController>().UpdateGrid(this);
                PlayMoveLeftAudio();
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
                    PlayRotateAudio();
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
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    PlayMoveRightAudio();
                }
            }
            else if (gameover == false)
            {
                transform.position += new Vector3(0, 1, 0);
                FindObjectOfType<GameController>().UpdateGrid(this);
                FindObjectOfType<GameController>().DeleteRow();
                FindObjectOfType<GameController>().SpawnNextTetrimino();
                enabled = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            dropping = true;
            

            while (dropping == true)
            {
                transform.position += new Vector3(0, -1f, 0);
                if (CheckIsValidPosition() && CheckIfGoingToCollide())
                {
                    
                    FindObjectOfType<GameController>().UpdateGrid(this);
                    
                }
                else
                {
                    transform.position += new Vector3(0, 1, 0);
                    FindObjectOfType<GameController>().UpdateGrid(this);
                    dropping = false;
                }
                
                    
                
            }

            
        }
    }
}