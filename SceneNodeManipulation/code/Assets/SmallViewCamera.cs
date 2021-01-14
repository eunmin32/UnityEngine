using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallViewCamera : MonoBehaviour
{ //camera primaitive 

    //public SceneNode cameraNode;
    public LineSegment LineOfSight = null;
    public Vector3 Pivot;
    public Camera cam;
    public Transform head;
    public SceneNode SceneNode;
    public Transform direcitonLine;
    public void LoadShaderMatrix(Matrix4x4 nodeMatrix)
    {
        Matrix4x4 p = Matrix4x4.TRS(Pivot, Quaternion.identity, Vector3.one);
        Matrix4x4 invp = Matrix4x4.TRS(-Pivot, Quaternion.identity, Vector3.one);
        Matrix4x4 trs = Matrix4x4.TRS(transform.localPosition, transform.localRotation, transform.localScale);
        Matrix4x4 m = nodeMatrix * p * trs * invp;
        cam.worldToCameraMatrix = m;
        //GetComponent<Renderer>().material.SetMatrix("MyXformMat", m);
        //GetComponent<Renderer>().material.SetColor("MyColor", MyColor);
    }
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 V = SceneNode.upVector();
        transform.position = SceneNode.returnAxisPos() + (1.75f *V);
        Vector3 W = Vector3.Cross(-V, Vector3.up);
        Vector3 U = Vector3.Cross(W, -V);
        transform.localRotation = Quaternion.FromToRotation(Vector3.up, U);
        Quaternion alignU = Quaternion.FromToRotation(transform.forward, V);
        transform.localRotation = alignU * transform.localRotation;

        direcitonLine.position = SceneNode.returnAxisPos() + (2.5f * V);
        //direcitonLine.localRotation = Quaternion.FromToRotation(Vector3.up, U);
       // direcitonLine.localRotation = alignU * transform.localRotation;

       // Vector3 up = mCombinedParentXform.GetColumn(1).normalized;
       // Vector3 forward = mCombinedParentXform.GetColumn(2).normalized;
        float angle = Mathf.Acos(Vector3.Dot(Vector3.up, V)) * Mathf.Rad2Deg;
        Vector3 axis = Vector3.Cross(Vector3.up, V);
        direcitonLine.localRotation = Quaternion.AngleAxis(angle, axis);
        

    }
    //if (LineOfSight != null)
    //LineOfSight.SetPoints(transform.localPosition, LookAtPosition.localPosition);
    // Viewing vector is from transform.localPosition to the lookat position
    //Vector3 V = LookAtPosition.localPosition - transform.localPosition;
    //Vector3 W = Vector3.Cross(-V, Vector3.up);
    //Vector3 U = Vector3.Cross(W, -V);
    //transform.localRotation = Quaternion.FromToRotation(Vector3.up, U);
    //Quaternion alignU = Quaternion.FromToRotation(transform.forward, V);
    //transform.localRotation = alignU * transform.localRotation;


}

