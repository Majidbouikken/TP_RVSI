using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGeneratorScript : MonoBehaviour
{
    public Material newMatObject;
    public float rotationSpeed = 30f;

    private new GameObject gameObject;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private Light light;
    private Vector3[] vertices;
    private int[] triangles;

    public void Start()
    {
        gameObject = new GameObject("My3dObject");
        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = newMatObject;
        // meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        // meshRenderer.receiveShadows = true;
        meshFilter.mesh = new Mesh();

        // On selectionne un mesh aleatoirement
        int randomMeshType = Random.Range(0, 2); // 0 pour cube, 1 pour sphere et 2 pour gem

        if (randomMeshType == 0)
        {
            GenerateCube();
        } else
        {
            GenerateGem();
        }

        gameObject.transform.position = new Vector3(2.968165f, 2f, -3.129097f);
        gameObject.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

        // Ajouter la lumiere
        light = gameObject.AddComponent<Light>();
        light.color = new Color(1f, 0.898f, 0.392f); // #FFE564 en hex
        light.range = 3f;
        light.intensity = 3.6f;
    }

    public void Update()
    {
        gameObject.transform.Rotate(new Vector3(1, 1, 1), rotationSpeed * Time.deltaTime);
    }

    // Pour generer un cube
    private void GenerateCube()
    {
        vertices = new Vector3[]
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
        meshFilter.mesh.vertices = vertices;
        meshFilter.mesh.triangles = triangles;
    }

    // Pour generer un Gem
    private void GenerateGem()
    {
        float height = 1.2f; // Height of the gem
        float bottomRadius = 0.6f; // Radius of the bottom octagon
        float topRadius = 0.4f; // Radius of the top octagon
        int numSides = 8; // Number of sides for both octagons

        vertices = new Vector3[numSides * 2 + 1]; // One extra vertex for the top
        triangles = new int[numSides * 9]; // Each side contributes 3 triangles

        // Calculate vertices for the bottom octagon
        for (int i = 0; i < numSides; i++)
        {
            float angle = i * 360f / numSides;
            vertices[i] = new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad) * bottomRadius, -height / 4f, Mathf.Cos(angle * Mathf.Deg2Rad) * bottomRadius);
        }

        // Calculate vertices for the top octagon
        for (int i = 0; i < numSides; i++)
        {
            float angle = i * 360f / numSides;
            vertices[numSides + i] = new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad) * topRadius, -height / 2f, Mathf.Cos(angle * Mathf.Deg2Rad) * topRadius);
        }

        // Top vertex
        vertices[numSides * 2] = new Vector3(0f, height / 2f, 0f);

        // Calculate triangles for the bottom octagon
        for (int i = 0; i < numSides; i++)
        {
            triangles[i * 3] = i;
            triangles[i * 3 + 1] = (i + 1) % numSides;
            triangles[i * 3 + 2] = numSides * 2; // Connect to the top vertex
        }

        // Calculate triangles for the top octagon
        for (int i = 0; i < numSides; i++)
        {
            triangles[(numSides * 3) + i * 3] = numSides + i;
            triangles[(numSides * 3) + i * 3 + 1] = numSides + (i + 1) % numSides;
            triangles[(numSides * 3) + i * 3 + 2] = numSides * 2; // Connect to the top vertex
        }

        // Calculate triangles connecting the bottom and top octagons
        for (int i = 0; i < numSides; i++)
        {
            triangles[(numSides * 6) + i * 3] = i;
            triangles[(numSides * 6) + i * 3 + 1] = (i + 1) % numSides;
            triangles[(numSides * 6) + i * 3 + 2] = numSides + (i + 1) % numSides;
        }

        meshFilter.mesh.Clear();
        meshFilter.mesh.vertices = vertices;
        meshFilter.mesh.triangles = triangles;
    }
}
