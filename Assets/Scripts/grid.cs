using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class grid : MonoBehaviour
{

    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    public float cellSize;
    public Vector3 gridOffset;
    int graidsize;

    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;

    }
    void Start()
    {
        makeGrid();
        UpdateMesh();
    }
       
       
    void makeGrid()
    {
        vertices = new Vector3[4];
        triangles = new int[6];

        float vertexOffset = cellSize * .5f;

        vertices[0] = new Vector3(-vertexOffset, 0, -vertexOffset);
        vertices[1] = new Vector3(-vertexOffset, 0, vertexOffset);
        vertices[2] = new Vector3(vertexOffset, 0, -vertexOffset);
        vertices[3] = new Vector3(vertexOffset, 0, vertexOffset);

        triangles[0] = 0;
        triangles[1] = triangles[4] = 1;
        triangles[2] = triangles[3] = 2;
        triangles[5] = 3;

    }
    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
        
}


