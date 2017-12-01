using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

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
    }
	
	// Update is called once per frame
	void Update () {
		
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
