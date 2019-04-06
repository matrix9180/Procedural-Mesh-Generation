using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
[ExecuteInEditMode]
public abstract class AbstractMeshGenerator : MonoBehaviour
{
    [SerializeField] protected Material material;

    protected List<Vector3> vertices;
    protected List<int> triangles;

    protected List<Vector3> normals;
    protected List<Vector4> tangeants;
    protected List<Vector2> uvs;
    protected List<Color32> vertexColors;

    protected int numVertices, numTriangles;

    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private MeshCollider meshCollider;

    private Mesh mesh;

    private void Update()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshCollider = GetComponent<MeshCollider>();

        meshRenderer.material = material;

        InitMesh();
        SetMeshNums();
        CreateMesh();
    }

    private bool ValidateMesh()
    {
        // Build a string containing errors
        string errorStr = "";

        // Check that there are the correct number of vertices and triangles
        errorStr += vertices.Count == numVertices ? "" : 
            "There should be " + numVertices + " vertices, but there are " + vertices.Count + ". ";
        errorStr += triangles.Count == numTriangles ? "" : 
            "There should be " + numTriangles + " triangles, but there are " + triangles.Count + ". ";

        // Check that there are the correct number of normals. These should be equal to the number of vertices, or 0 if we're not manuall calculating normals.
        errorStr += (normals.Count == 0 || normals.Count == numVertices) ? "" :
            "There should be " + numVertices + " normals, but there are " + normals.Count + ". ";
        errorStr += (tangeants.Count == 0 || normals.Count ==  numVertices) ? "" : 
            "There should be " + numVertices + " tangeants, but there are " + tangeants.Count + ". ";
        errorStr += (uvs.Count == 0 || uvs.Count == numVertices) ? "" : 
            "There should be " + numVertices + " uvs, but there are " + uvs.Count + ". ";
        errorStr += (vertexColors.Count == 0 || vertexColors.Count == numVertices) ? "" : 
            "There should be " + numVertices + " vertex colors, but there are " + vertexColors.Count + ". ";

        bool isValid = string.IsNullOrEmpty(errorStr);

        if (!isValid)
        {
            Debug.LogError("Not drawing mesh: " + errorStr);
        }

        return isValid;
    }

    private void InitMesh()
    {
        vertices = new List<Vector3>();
        triangles = new List<int>();

        normals = new List<Vector3>();
        tangeants = new List<Vector4>();
        uvs = new List<Vector2>();
        vertexColors = new List<Color32>();
    }

    private void CreateMesh()
    {
        mesh = new Mesh();

        SetVertices();
        SetTriangles();
        SetNormals();
        SetTangeants();
        SetUVs();
        SetVertexColors();
        if (ValidateMesh())
        {

            // Unity requires vertices first then triangles
            mesh.SetVertices(vertices);
            mesh.SetTriangles(triangles, 0);

            if (normals.Count == 0)
            {
                mesh.RecalculateNormals();
                normals.AddRange(mesh.normals);
            }

            mesh.SetNormals(normals);
            mesh.SetTangents(tangeants);
            mesh.SetUVs(0, uvs);
            mesh.SetColors(vertexColors);

            meshFilter.mesh = mesh;
            meshCollider.sharedMesh = mesh;
        }
    }

    protected abstract void SetMeshNums();
    protected abstract void SetVertices();
    protected abstract void SetTriangles();
    protected abstract void SetNormals();
    protected abstract void SetTangeants();
    protected abstract void SetUVs();
    protected abstract void SetVertexColors();

}
