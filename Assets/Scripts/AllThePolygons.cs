using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllThePolygons : AbstractMeshGenerator
{
    [Range(3,5000)]
    [SerializeField] private int numSides = 3;
    [SerializeField] private float radius;

    protected override void SetMeshNums()
    {
        numVertices = numSides;
        // There are numSides-2 physical triangles in a regular polygon. 
        // 3 ints describe each triangle.  
        numTriangles = 3 * (numSides-2);
    }
    protected override void SetVertices()
    {
        for (int i = 0; i < numSides; i++)
        {
            float angle = 2 * Mathf.PI * i / numSides;
            vertices.Add(new Vector3(radius * Mathf.Cos(angle), radius * Mathf.Sin(angle), 0f));
        }
    }
    protected override void SetTriangles()
    {
        for (int i = 1; i < numSides - 1; i++)
        {
            triangles.Add(0);
            triangles.Add(i + 1);
            triangles.Add(i);
        }
    }

    protected override void SetNormals() { }
    protected override void SetTangeants() { }
    protected override void SetUVs() { }
    protected override void SetVertexColors() { }
}
