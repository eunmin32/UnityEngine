using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MyMesh : MonoBehaviour
{
    [SerializeField]
    float normalLength = 1.0f;

    void UpdateNormals(Vector3[] v, Vector3[] n)
    {
        for (int i = 0; i < v.Length; i++)
        {
            Vector3 endpoint1 = v[i];
            Vector3 endpoint2 = v[i] + (normalLength * n[i]);

            Vector3 normVisualVector = endpoint2 - endpoint1;
            Vector3 vectorPos = v[i] + (normVisualVector * 0.5f);

            VertexController vertexScript = mControllers[i].GetComponent<VertexController>();
            vertexScript.DisplayNormal(vectorPos, normVisualVector);
        }
    }

    Vector3 FaceNormal(Vector3[] v, int i0, int i1, int i2)
    {
        Vector3 a = v[i1] - v[i0];
        Vector3 b = v[i2] - v[i0];
        return Vector3.Cross(a, b).normalized;
    }

    void ComputeNormals(Vector3[] v, Vector3[] n)
    {
        Mesh theMesh = GetComponent<MeshFilter>().mesh;

        int[] triangleIndeces = theMesh.triangles;
        Vector3[] verts = theMesh.vertices;
        Vector3[] norms = theMesh.normals;

        //Array of sums for each vertex (to be normalized 
        //for normal calculation later)
        Vector3[] vertSums = new Vector3[verts.Length];

        //Increment through the triangle indeces in sets of three
        for(int i = 0; i < triangleIndeces.Length; i += 3)
        {
            //Store the indeces (for readability)
            int i0 = triangleIndeces[i];
            int i1 = triangleIndeces[i + 1];
            int i2 = triangleIndeces[i + 2];

            //Compute the triangle's face normal
            Vector3 faceNorm = FaceNormal(verts, i0, i1, i2);

            //Add the face normal to the vertex sum associated with each vertex
            vertSums[i0] += faceNorm;
            vertSums[i1] += faceNorm;
            vertSums[i2] += faceNorm;
        }

        //Set the normals to equal the normalized equivalent of their corresponding
        //vertex sum
        for(int i = 0; i < norms.Length; ++i)
        {
            norms[i] = vertSums[i].normalized;
        }

        //Assign the updated normals
        theMesh.normals = norms;

        UpdateNormals(verts, norms);
    }
}
