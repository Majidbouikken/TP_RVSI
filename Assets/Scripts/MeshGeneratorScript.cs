using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGeneratorScript : MonoBehaviour
{
    public Material newMatObject;
    public float rotationSpeed = 30f;

    private new GameObject gameObject;
    private Vector3[] verticies;
    private int[] triangles;

    public void Start()
    {
        gameObject = new GameObject("My3dObject");
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = newMatObject;
        meshFilter.mesh = new Mesh();

        verticies = new Vector3[]
        {
            new Vector3(-0.5f,-0.5f,-0.5f), // 0
            new Vector3(-0.5f,-0.5f,0.5f), // 1
            new Vector3(0.5f,-0.5f,-0.5f), // 2
            new Vector3(0.5f,-0.5f,0.5f), // 3
            new Vector3(-0.5f,0.5f,-0.5f), // 4
            new Vector3(-0.5f,0.5f,0.5f), // 5
            new Vector3(0.5f,0.5f,-0.5f), // 6
            new Vector3(0.5f,0.5f,0.5f), // 7
        };

        triangles = new int[]
        {
            0, 2, 1,
            1, 2, 3,
            1, 3, 5,
            5, 3, 7,
            5, 7, 4,
            4, 7, 6,
            4, 6, 0,
            0, 6, 2,
            0, 1, 4,
            1, 5, 4,
            2, 6, 3,
            3, 6, 7,
        };

        meshFilter.mesh.Clear();
        meshFilter.mesh.vertices = verticies;
        meshFilter.mesh.triangles = triangles;

        gameObject.transform.position = new Vector3(2.968165f, 2f, -3.129097f);
        gameObject.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
    }

    public void Update()
    {
        gameObject.transform.Rotate(new Vector3(1, 1, 1), rotationSpeed * Time.deltaTime);
    }
}
