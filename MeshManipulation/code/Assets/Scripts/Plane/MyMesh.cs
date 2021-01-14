using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MyMesh : MonoBehaviour
{
    Vector2 curTranslation, curScale;
    float curRotation;

    // Use this for initialization
    void Start()
    {
        curTranslation = new Vector2(0, 0);
        curScale = new Vector2(1, 1);
        curRotation = 0;

        Mesh theMesh = GetComponent<MeshFilter>().mesh;   // get the mesh component
        theMesh.Clear();    // delete whatever is there!!

        Vector3[] v = new Vector3[9];   // 2x2 mesh needs 3x3 vertices
        int[] t = new int[8 * 3];         // Number of triangles: 2x2 mesh and 2x triangles on each mesh-unit
        Vector3[] n = new Vector3[9];   // MUST be the same as number of vertices


        v[0] = new Vector3(-1, 0, -1);
        v[1] = new Vector3(0, 0, -1);
        v[2] = new Vector3(1, 0, -1);

        v[3] = new Vector3(-1, 0, 0);
        v[4] = new Vector3(0, 0, 0);
        v[5] = new Vector3(1, 0, 0);

        v[6] = new Vector3(-1, 0, 1);
        v[7] = new Vector3(0, 0, 1);
        v[8] = new Vector3(1, 0, 1);

        n[0] = new Vector3(0, 1, 0);
        n[1] = new Vector3(0, 1, 0);
        n[2] = new Vector3(0, 1, 0);
        n[3] = new Vector3(0, 1, 0);
        n[4] = new Vector3(0, 1, 0);
        n[5] = new Vector3(0, 1, 0);
        n[6] = new Vector3(0, 1, 0);
        n[7] = new Vector3(0, 1, 0);
        n[8] = new Vector3(0, 1, 0);

        // First triangle
        t[0] = 0; t[1] = 3; t[2] = 4;  // 0th triangle
        t[3] = 0; t[4] = 4; t[5] = 1;  // 1st triangle

        t[6] = 1; t[7] = 4; t[8] = 5;  // 2nd triangle
        t[9] = 1; t[10] = 5; t[11] = 2;  // 3rd triangle

        t[12] = 3; t[13] = 6; t[14] = 7;  // 4th triangle
        t[15] = 3; t[16] = 7; t[17] = 4;  // 5th triangle

        t[18] = 4; t[19] = 7; t[20] = 8;  // 6th triangle
        t[21] = 4; t[22] = 8; t[23] = 5;  // 7th triangle

        Vector2[] uv = new Vector2[9];

        for (int i = 0; i < uv.Length/*dimension + 1*/; ++i)
        {
            Debug.Log("UV index " + i + " is at (" + v[i].x + ", " + v[i].z + ")");
            uv[i] = new Vector2(v[i].x, v[i].z);

        }
        //uv[0] = new Vector2(0, 0);
        //uv[1] = new Vector2(0.5f, 0);
        //uv[2] = new Vector2(1, 0);

        //uv[3] = new Vector2(0, 0.5f);
        //uv[4] = new Vector2(0.5f, 0.5f);
        //uv[5] = new Vector2(1, 0.5f);

        //uv[6] = new Vector2(0, 1);
        //uv[7] = new Vector2(0.5f, 1);
        //uv[8] = new Vector2(1, 1);

        theMesh.vertices = v; //  new Vector3[3];
        theMesh.triangles = t; //  new int[3];
        theMesh.normals = n;
        theMesh.uv = uv;

        InitControllers(v);
    }

    // Update is called once per frame
    void Update()
    {
        Mesh theMesh = GetComponent<MeshFilter>().mesh;
        Vector3[] v = theMesh.vertices;
        for (int i = 0; i < mControllers.Length; i++)
        {
            v[i] = mControllers[i].transform.localPosition;
        }

        theMesh.vertices = v;

        if (Input.GetKey(KeyCode.LeftControl))
            ToggleControllers(true);
        else
            ToggleControllers(false);

        ComputeNormals(theMesh.vertices, theMesh.normals);
    }

    public void UpdateMesh(int dimension)
    {
        Debug.Log("Updating Mesh");

        Mesh theMesh = GetComponent<MeshFilter>().mesh;   // get the mesh component
        theMesh.Clear();    // delete whatever is there!!

        int vertCount = (dimension + 1) * (dimension + 1);

        Vector3[] v = new Vector3[vertCount];   // 2x2 mesh needs 3x3 vertices
        int[] t = new int[dimension * dimension * 2 * 3];        // Number of triangles: 2x2 mesh and 2x triangles on each mesh-unit
        Vector3[] n = new Vector3[vertCount];   // MUST be the same as number of vertices

        float xCoord = -1.0f, zCoord = -1.0f;
        int vertIndex = 0;

        //Double for loop to calculate vertex positions
        for (int i = 0; i < dimension + 1; ++i)
        {
            for(int j = 0; j < dimension; ++j)
            {
                //Add a new vector to both the vector array and normal array
                v[vertIndex] = new Vector3(xCoord, 0, zCoord);
                n[vertIndex] = new Vector3(0, 1, 0);

                //Increment the x-coordinate
                xCoord += (2f / dimension);
                Debug.Log("X coord: " + xCoord + ", Z coord: " + zCoord);

                //Increment the vertex index
                vertIndex++;

                //Add the final row
                if(j == dimension - 1)
                {
                    v[vertIndex] = new Vector3(xCoord, 0, zCoord);
                    n[vertIndex] = new Vector3(0, 1, 0);
                    vertIndex++;
                }
            }

            //Reset the x-coordinate
            xCoord = -1.0f;

            //Increment the z-coordinate
            zCoord += (2f / dimension);

        }

        int start = 0;
        int triIndex = 0;

        //Calculate triangles - Each loop establishes 
        //corner vertices for two triangles forming a square of
        //the mesh
        for(int i = 0; i < dimension; ++i)
        {
            for(int j = 0; j < dimension; ++j)
            {
                t[triIndex++] = start;
                t[triIndex++] = start + dimension + 1;
                t[triIndex++] = start + dimension + 2;
                t[triIndex++] = start;
                t[triIndex++] = start + dimension + 2;
                t[triIndex++] = start + 1;

                //Update the start index for the next loop
                if(j < dimension - 1)
                    start += 1;
            }

            //Update the start index to begin at the initial 
            //edge of the quad
            if(i < dimension - 1)
                start += 2;
        }

        Vector2[] uv = new Vector2[vertCount];

        //Update the uv
        for(int i = 0; i < uv.Length/*dimension + 1*/; ++i)
        {
            Debug.Log("UV index " + i + " is at (" + v[i].x + ", " + v[i].z + ")");
            uv[i] = new Vector2(v[i].x, v[i].z);

            //for (int j = 0; j < dimension + 1; ++j)
            //{
            //    uv[uvIndex++] = new Vector2(xVal, zVal);
            //    xVal += 1f / dimension;
            //}
            //zVal += 1f / dimension;
        }


        //Update the mesh
        theMesh.vertices = v;
        theMesh.triangles = t;
        theMesh.normals = n;
        theMesh.uv = uv;

        //Destroy previous controllers
        ClearControllers();

        //Reinitialize the vertex controllers
        InitControllers(v);
    }

}
