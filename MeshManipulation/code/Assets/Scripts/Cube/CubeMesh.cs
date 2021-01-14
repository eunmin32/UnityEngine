using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMesh : MonoBehaviour
{
    [SerializeField]
    GameObject[] mControllers;

    [SerializeField]
    GameObject selectedController, vertexPrefab;

    [SerializeField]
    float normalLength = 1.0f;

    [SerializeField]
    Material selectedMaterial, defaultMaterial;

    // Start is called before the first frame update
    void Start()
    {
        Mesh theMesh = GetComponent<MeshFilter>().mesh;   // get the mesh component
        theMesh.Clear();    // delete whatever is there!!

        Vector3[] v = new Vector3[8];
        int[] t = new int[6 * 2 * 3];    
        Vector3[] n = new Vector3[8];

        v[0] = new Vector3(-.5f, 0, -.5f);
        v[1] = new Vector3(.5f, 0, -.5f);
        v[2] = new Vector3(-.5f, 1, -.5f);
        v[3] = new Vector3(.5f, 1, -.5f);
        v[4] = new Vector3(-.5f, 1, .5f);
        v[5] = new Vector3(.5f, 1, .5f);
        v[6] = new Vector3(-.5f, 0, .5f);
        v[7] = new Vector3(.5f, 0, .5f);

        n[0] = new Vector3(-1, -1, -1);
        n[1] = new Vector3(1, -1, -1);
        n[2] = new Vector3(-1, 1, -1);
        n[3] = new Vector3(1, 1, -1);
        n[4] = new Vector3(-1, 1, 1);
        n[5] = new Vector3(1, 1, 1);
        n[6] = new Vector3(1, -1, 1);
        n[7] = new Vector3(1, -1, 1);

        t[0] = 0; t[1] = 2; t[2] = 3;
        t[3] = 0; t[4] = 3; t[5] = 1;

        t[6] = 2; t[7] = 4; t[8] = 5;
        t[9] = 2; t[10] = 5; t[11] = 3;

        t[12] = 4; t[13] = 6; t[14] = 7;
        t[15] = 4; t[16] = 7; t[17] = 5;

        t[18] = 6; t[19] = 0; t[20] = 1;
        t[21] = 6; t[22] = 1; t[23] = 7;

        t[24] = 0; t[25] = 2; t[26] = 4;
        t[27] = 0; t[28] = 4; t[29] = 6;

        t[30] = 1; t[31] = 3; t[32] = 5;
        t[33] = 1; t[34] = 5; t[35] = 7;


        theMesh.vertices = v; //  new Vector3[3];
        theMesh.triangles = t; //  new int[3];
        theMesh.normals = n;

        InitControllers(v);
    }

    // Update is called once per frame
    void Update()
    {
        Mesh theMesh = GetComponent<MeshFilter>().mesh;

        //    ComputeNormals(theMesh.vertices, theMesh.normals);
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
    }

    void InitControllers(Vector3[] v)
    {
        mControllers = new GameObject[v.Length];
        for (int i = 0; i < v.Length; i++)
        {
            //mControllers[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            //mControllers[i].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            mControllers[i] = Instantiate(vertexPrefab);

            mControllers[i].transform.localPosition = v[i];
            mControllers[i].transform.parent = this.transform;

            //Add a vertex control script to the vertex
            //    mControllers[i].AddComponent<VertexController>();

            //Tag the vertex
            mControllers[i].transform.tag = "Vertex";
        }

        //    ToggleControllers(false);
    }

    //void UpdateNormals(Vector3[] v, Vector3[] n)
    //{
    //    for (int i = 0; i < v.Length; i++)
    //    {
    //        Vector3 endpoint1 = v[i];
    //        Vector3 endpoint2 = v[i] + (normalLength * n[i]);

    //        Vector3 normVisualVector = endpoint2 - endpoint1;
    //        Vector3 vectorPos = v[i] + (normVisualVector * 0.5f);

    //        VertexController vertexScript = mControllers[i].GetComponent<VertexController>();
    //        vertexScript.DisplayNormal(vectorPos, normVisualVector);
    //    }
    //}

    //Vector3 FaceNormal(Vector3[] v, int i0, int i1, int i2)
    //{
    //    Vector3 a = v[i1] - v[i0];
    //    Vector3 b = v[i2] - v[i0];
    //    return Vector3.Cross(a, b).normalized;
    //}

    //void ComputeNormals(Vector3[] v, Vector3[] n)
    //{
    //    Mesh theMesh = GetComponent<MeshFilter>().mesh;

    //    int[] triangleIndeces = theMesh.triangles;
    //    Vector3[] verts = theMesh.vertices;
    //    Vector3[] norms = theMesh.normals;

    //    //Array of sums for each vertex (to be normalized 
    //    //for normal calculation later)
    //    Vector3[] vertSums = new Vector3[verts.Length];

    //    //Increment through the triangle indeces in sets of three
    //    for (int i = 0; i < triangleIndeces.Length; i += 3)
    //    {
    //        //Store the indeces (for readability)
    //        int i0 = triangleIndeces[i];
    //        int i1 = triangleIndeces[i + 1];
    //        int i2 = triangleIndeces[i + 2];

    //        //Compute the triangle's face normal
    //        Vector3 faceNorm = FaceNormal(verts, i0, i1, i2);

    //        //Add the face normal to the vertex sum associated with each vertex
    //        vertSums[i0] += faceNorm;
    //        vertSums[i1] += faceNorm;
    //        vertSums[i2] += faceNorm;
    //    }

    //    //Set the normals to equal the normalized equivalent of their corresponding
    //    //vertex sum
    //    for (int i = 0; i < norms.Length; ++i)
    //    {
    //        norms[i] = vertSums[i].normalized;
    //    }

    //    //Assign the updated normals
    //    theMesh.normals = norms;

    //    UpdateNormals(verts, norms);
    //}

    void ClearControllers()
    {
        foreach (GameObject controller in mControllers)
        {
            Destroy(controller);
        }
    }

    //Script to toggle the control points for vertices on/off
    void ToggleControllers(bool state)
    {
        foreach (GameObject controller in mControllers)
        {
            MeshRenderer controlRend = controller.GetComponent<MeshRenderer>();
            CubeController controlScript = controller.GetComponent<CubeController>();

            controlRend.enabled = state;
            controlScript.enabled = state;

            if (controller == selectedController)
                controlScript.ToggleAxes(state);
            else
                controlScript.ToggleAxes(false);

        //    controlScript.ToggleNormal(state);
        }
    }

    //Script to tell the mesh which vertex is currently selected
    public void SetSelectedVert(GameObject vertex)
    {
        foreach (GameObject controller in mControllers)
        {
            CubeController controlScript = controller.GetComponent<CubeController>();

            //If the current game object in the loop is the newly selected vertex,
            //update its color. If not, set it to the default color (white)
            if (GameObject.ReferenceEquals(controller, vertex))
            {
                selectedController = controller;
                MeshRenderer controlRenderer = controller.GetComponent<MeshRenderer>();
                controlRenderer.material = selectedMaterial;
                controlScript.ToggleAxes(true);
            }
            else
            {
                MeshRenderer controlRenderer = controller.GetComponent<MeshRenderer>();
                controlRenderer.material = defaultMaterial;
                controlScript.ToggleAxes(false);
            }
        }
    }
}
