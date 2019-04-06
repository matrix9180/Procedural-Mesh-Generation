using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllTheSharedVertTetrahedrons : AbstractMeshGenerator
{
    [SerializeField] private Vector3[] vs = new Vector3[4];

    protected override void SetMeshNums()
    {
        numVertices = 4;
        numTriangles = 12; //tetrahedron has 4 triangular sides, 3 vertices per triangle
    }

    protected override void SetVertices()
    {
        vertices.AddRange(vs);
    }

    protected override void SetTriangles()
    {
        // base
        triangles.Add(0);
        triangles.Add(2);
        triangles.Add(1);


        // sides
        triangles.Add(0);
        triangles.Add(3);
        triangles.Add(2);

        triangles.Add(2);
        triangles.Add(3);
        triangles.Add(1);

        triangles.Add(1);
        triangles.Add(3);
        triangles.Add(0);

    }
     
    protected override void SetNormals() {}

    protected override void SetTangeants() {}

    protected override void SetUVs() {}

    protected override void SetVertexColors() {}
}
