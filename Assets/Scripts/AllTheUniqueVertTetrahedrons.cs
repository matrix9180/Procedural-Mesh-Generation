using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllTheUniqueVertTetrahedrons : AbstractMeshGenerator
{
    [SerializeField] private Vector3[] vs = new Vector3[4];

    protected override void SetMeshNums()
    {
        numVertices = 12;
        numTriangles = 12; //tetrahedron has 4 triangular sides, 3 vertices per triangle
    }

    protected override void SetVertices()
    {
        vertices.Add(vs[0]);
        vertices.Add(vs[2]);
        vertices.Add(vs[1]);

        vertices.Add(vs[0]);
        vertices.Add(vs[3]);
        vertices.Add(vs[2]);

        vertices.Add(vs[2]);
        vertices.Add(vs[3]);
        vertices.Add(vs[1]);

        vertices.Add(vs[1]);
        vertices.Add(vs[3]);
        vertices.Add(vs[0]);
    }

    protected override void SetTriangles()
    {
        for (int i = 0; i < numTriangles; i++)
        {
            triangles.Add(i);

        }
    }

    protected override void SetNormals() { }

    protected override void SetTangeants() { }

    protected override void SetUVs() { }

    protected override void SetVertexColors() { }
}
