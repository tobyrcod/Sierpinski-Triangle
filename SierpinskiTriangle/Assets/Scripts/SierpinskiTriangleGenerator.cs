using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class SierpinskiTriangleGenerator : MonoBehaviour
{
    [SerializeField] private float _radius;

    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshFilter = this.GetComponent<MeshFilter>();
        _meshRenderer = this.GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        // Create the Mesh for the triangles
        Mesh mesh = new Mesh();

        // Create Vertices, UVs, and Triangles array
        Vector3[] vertices = new Vector3[3];
        Vector2[] uv = new Vector2[3];
        int[] triangles = new int[3];

        // Generate 3 points in an equilateral traingle centered at the origin
        float theta = 210;
        float r = _radius;
        for (int i = 0; i < 3; i++)
        {
            float x = r * Mathf.Cos(theta * Mathf.Deg2Rad);
            float y = r * Mathf.Sin(theta * Mathf.Deg2Rad);
            Debug.Log(new Vector3(x, y));
            vertices[i] = new Vector3(x, y);

            theta -= 120;
        }

        // Set the first (and only) triangle to be the first 3 (and only) vertices
        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        // Initialise the Mesh with a single triangle
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        _meshFilter.mesh = mesh;
    }
}
