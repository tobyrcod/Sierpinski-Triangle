using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class SierpinskiTriangleGenerator : MonoBehaviour
{
    [SerializeField] private float _radius;

    private Mesh _mesh;
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
        _mesh = new Mesh();

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

        // Offset the Triangle from 'center' to 'vertical middle'
        float offset = r * (1 - Mathf.Sin(30 * Mathf.Deg2Rad)) / 2;
        for (int i = 0; i < 3; i++)
        {
            vertices[i] -= Vector3.up * offset;
        }

        // Set the first (and only) triangle to be the first 3 (and only) vertices
        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        // Initialise the Mesh with a single triangle
        _mesh.vertices = vertices;
        _mesh.uv = uv;
        _mesh.triangles = triangles;
        _meshFilter.mesh = _mesh;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextGeneration();
        }
    }

    private void NextGeneration()
    {
        Vector3[] oldVertices = _mesh.vertices;
        Vector3[] newVertices = new Vector3[oldVertices.Length * 2];
        Array.Copy(oldVertices, newVertices, oldVertices.Length);

        int[] oldTriangles = _mesh.triangles;
        int[] newTriangles = new int[oldTriangles.Length * 3];

        // For every triangle
        for (int i = 0; i < oldTriangles.Length; i += 3)
        {
            // First, create the new vertices
            // Get the indicies of the vertices of the triangle
            int t1 = oldTriangles[i];
            int t2 = oldTriangles[i + 1];
            int t3 = oldTriangles[i + 2];

            // Get the vertices of the triangle
            Vector3 v1 = oldVertices[t1];
            Vector3 v2 = oldVertices[t2];
            Vector3 v3 = oldVertices[t3];

            // Find the midpoints (new vertices)
            Vector3 v4 = (v1 + v2) / 2;
            Vector3 v5 = (v2 + v3) / 2;
            Vector3 v6 = (v3 + v1) / 2;

            // Get the indices of the new vertices
            int t4 = oldVertices.Length + i / 3;
            int t5 = t4 + 1;
            int t6 = t5 + 1;

            // Add the new vertices to the mesh
            newVertices[t4] = v4;
            newVertices[t5] = v5;
            newVertices[t6] = v6;

            _mesh.vertices = newVertices;
        }
    }

    private void OnDrawGizmos()
    {
        if (_mesh != null)
        {
            foreach (Vector3 v in _mesh.vertices)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(v, 0.2f);
            }
        }
    }
}
