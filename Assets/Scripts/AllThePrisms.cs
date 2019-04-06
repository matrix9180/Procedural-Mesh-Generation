using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllThePrisms : AbstractMeshGenerator
{
    [Range(3, 50)]
    [SerializeField] private int numSides = 3;
    [SerializeField] private float radius;
    [SerializeField] private float length;

    protected override void SetMeshNums()
    {
        numVertices = 6 * numSides; // numSides vertices per end, and 4 per length-side
        numTriangles = 12 * (numSides - 1);// 3*(numSides-2) per end and 6 per length-side 
    }
    protected override void SetVertices()
    {
        // building block vertices
        Vector3[] vs = new Vector3[2 * numSides];

        // set the vs
        for (int i = 0; i < numSides; i++)
        {
            float angle = 2 * Mathf.PI * i / numSides;
            vs[i] = new Vector3(radius * Mathf.Cos(angle), radius * Mathf.Sin(angle), 0f);
            vs[i+numSides] = new Vector3(radius * Mathf.Cos(angle), radius * Mathf.Sin(angle), length);
        }

        // set vertices - first end
        for (int i = 0; i < numSides; i++)
        {
            vertices.Add(vs[i]);
        }

        // middle verts
        for (int i = 0; i < numSides; i++)
        {
            vertices.Add(vs[i]);
            int secondIndex = i == 0 ? 2 * numSides - 1 : numSides + i - 1;
            vertices.Add(vs[secondIndex]);
            int thirdIndex = i == 0 ? numSides - 1 : i - 1;
            vertices.Add(vs[thirdIndex]);
            vertices.Add(vs[i + numSides]);
        }

        // other end
        for (int i = 0; i < numSides; i++)
        {
            vertices.Add(vs[i + numSides]);
        }
    }
    protected override void SetTriangles()
    {
        // first end
        for (int i = 1; i < numSides - 1; i++)
        {
            triangles.Add(0);
            triangles.Add(i + 1);
            triangles.Add(i);
        }
        // middle
        for (int i = 1; i <= numSides; i++)
        {
            // numSides triangles first end, start at numSides. 
            int val = numSides + 4 * (i - 1);

            triangles.Add(val);
            triangles.Add(val + 1);
            triangles.Add(val + 2);

            triangles.Add(val);
            triangles.Add(val + 3);
            triangles.Add(val + 1);
        }

        // other end, opposite rotation
        for (int i = 1; i < numSides - 1; i++)
        {
            // numSides triangles first end, 4*numSides in mid, this starts 5*numSides
            triangles.Add(5*numSides);
            triangles.Add(5*numSides + i);
            triangles.Add(5*numSides + i + 1);
        }


    }

    protected override void SetNormals() { }
    protected override void SetTangeants() { }
    protected override void SetUVs() { }
    protected override void SetVertexColors() { }
}
