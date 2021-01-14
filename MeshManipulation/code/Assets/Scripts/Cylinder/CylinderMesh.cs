using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
public class CylinderMesh : MonoBehaviour
{

    float Radius = 0.5f;
    float Deg = 220f;
    int Res = 7;
    float height = 1f;
    [SerializeField]
    GameObject[] mController;
    public GameObject normalPrefab;
    Mesh theMesh;
    // Start is called before the first frame update

    void Start()
    {
        theMesh = GetComponent<MeshFilter>().mesh;
        Assert.IsNotNull(theMesh);
        theMesh.Clear();
        mController = new GameObject[Res * Res];
        locateBase();
        initVertices();
        initTriangles();
        initNormal();
        changeEndControllerColor();
    }

    public void changeResolution(int res)
    {
        Res = res;
        resetCylidner();
    }

    public void changeDegree(float deg)
    {
        Deg = deg;
        resetCylidner();
    }

    public void changeHeight(float ht)
    {
        height = ht;
        resetCylidner();
    }

    public void changeRad(float rd)
    {
        Radius = rd;
        resetCylidner();
    }

    private void resetCylidner()
    {
        if (theMesh != null)
            theMesh.Clear();
        if (mController != null)
            clearMController();
        mController = new GameObject[Res * Res];
        locateBase();
        initVertices();
        initTriangles();
        initNormal();
        changeEndControllerColor();
    }

    private void locateBase()
    {
        mController[0] = Instantiate(normalPrefab);
        mController[0].transform.localPosition = new Vector3(Radius, 0f, 0f);
        mController[0].transform.localRotation = Quaternion.FromToRotation(Vector3.up, Vector3.right);

        mController[0].SetActive(false);

        for (int i = 1; i < Res; i++)
        {
            mController[i] = Instantiate(normalPrefab);
            mController[i].transform.localPosition = Quaternion.Euler(0, -Deg / (Res - 1) * i, 0) * Vector3.right * Radius;
            mController[i].transform.localRotation = Quaternion.FromToRotation(Vector3.up, mController[i].transform.localPosition);

            mController[i].SetActive(false);

        }
    }
    private void clearMController()
    {
        for (int i = 0; i < mController.Length; i++)
        {
            Destroy(mController[i]);
        }
    }

    private void initVertices()
    {
        Vector3[] v = new Vector3[Res * Res];
        for (int i = 0; i < Res; i++)
        {
            v[i] = mController[i].transform.localPosition;
        }
        for (int i = Res; i < Res * Res; i++)
        {
            v[i] = mController[i % Res].transform.localPosition + new Vector3(0, height / (Res - 1) * (i / Res), 0);
            mController[i] = Instantiate(normalPrefab);
            mController[i].transform.localPosition = v[i];
            mController[i].transform.localRotation = mController[i % Res].transform.localRotation;
            mController[i].SetActive(false);
        }
        theMesh.vertices = v;
    }

    private void initTriangles()
    {
        int[] t = new int[(Res - 1) * (Res - 1) * 6];
        //increment by res 
        int index = 0;
        for (int l = 0; l < Res * (Res - 1); l += Res)
        {
            for (int i = l; i < l + Res - 1; i++)
            {
                t[index] = i; index++;
                t[index] = i + Res + 1; index++;
                t[index] = i + 1; index++;

                t[index] = i + Res + 1; index++;
                t[index] = i; index++;
                t[index] = i + Res; index++;


            }
        }
        theMesh.triangles = t;
    }



    private void initNormal()
    {
        Vector3[] n = new Vector3[Res * Res];
        for (int i = 0; i < n.Length; i++)
        {
            n[i] = theMesh.vertices[i % Res].normalized;

        }
        theMesh.normals = n;
    }

    private void UpdateNormal()
    {
        for (int i = 0; i < mController.Length; i++)
        {
            mController[i].transform.localRotation = Quaternion.FromToRotation(transform.up, theMesh.normals[i]);
        }
    }

    Vector3 FaceNormal(Vector3[] v, int i0, int i1, int i2)
    {
        Vector3 a = v[i1] - v[i0];
        Vector3 b = v[i2] - v[i0];
        return Vector3.Cross(a, b).normalized;
    }

    void ComputeNormals()
    {
        Mesh theMesh = GetComponent<MeshFilter>().mesh;

        int[] triangleIndeces = theMesh.triangles;
        Vector3[] verts = theMesh.vertices;
        Vector3[] norms = theMesh.normals;

        //Array of sums for each vertex (to be normalized 
        //for normal calculation later)
        Vector3[] vertSums = new Vector3[verts.Length];

        //Increment through the triangle indeces in sets of three
        for (int i = 0; i < triangleIndeces.Length; i += 3)
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
        for (int i = 0; i < norms.Length; ++i)
        {
            norms[i] = vertSums[i].normalized;
        }

        //Assign the updated normals
        theMesh.normals = norms;
        UpdateNormal();
    }

    private void Update()
    {
        updateVertex();
        ComputeNormals();
        detectChange();
    }



    private void updateVertex()
    {
        Vector3[] v = new Vector3[Res * Res];
        for (int i = 0; i < Res * Res; i++)
            v[i] = mController[i].transform.localPosition;

        theMesh.vertices = v;
    }

    public void EnableNormalDisplay(bool on)
    {
        for (int i = 0; i < mController.Length; i++)
            mController[i].SetActive(on);
    }

    private void changeEndControllerColor()
    {
        //EnableNormalDisplay();
        int a = 0;
        for (int i = 0; i < Res * Res; i += Res)
        {
            mController[i].GetComponent<Renderer>().material.SetColor("_Color", Color.red);

        }
    }

    public int isController(Transform ob)
    {
        int vertIndex = -1;
        for (int i = 0; i < Res * Res; i += Res)
        {
            if (ob == mController[i].transform)
                vertIndex = i;
        }
        Debug.Log("Controller Index: " + vertIndex);
        return vertIndex;
    }

    public void setControllerPosAt(int i, Vector3 newPos)
    {
        mController[i].transform.position = newPos;
    }


    public Vector3 controllerPosAt(int i)
    {
         return mController[i].transform.position;
    }

    public Vector3 intialPosAt(int index) {
        int level = index / Res;
        int at = index % Res;
        Vector3 pos = Quaternion.Euler(0, at * -Deg / (Res - 1), 0) * new Vector3(Radius ,height / (Res - 1) * level ,0);
        return pos;
    }

    void detectChange()
    {
        for (int i = 0; i < Res* Res; i += Res)
        {
            if (intialPosAt(i) != mController[i].transform.position)
            {
                Vector3 off = -(intialPosAt(i) - mController[i].transform.position);
                Debug.Log(off);
                for(int j = i + 1; j < i + Res; j++)
                {
                    mController[j].transform.position = intialPosAt(j) + (Quaternion.Euler(0, -Deg / (Res - 1) * (j - i),0) * off);
                }
            }
                
        }
    }

}
