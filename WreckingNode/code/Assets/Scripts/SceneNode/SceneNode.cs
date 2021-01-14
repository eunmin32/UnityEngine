using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneNode : MonoBehaviour {

    protected Matrix4x4 mCombinedParentXform;
    
    public Vector3 NodeOrigin = Vector3.zero;
    public Vector3 AngleOrigin = Vector3.zero;

    public List<NodePrimitive> PrimitiveList;
    public Transform AxisFrame = null;

    private Quaternion rotation;
    // Use this for initialization
    protected void Start () {
        InitializeSceneNode();
        // Debug.Log("PrimitiveList:" + PrimitiveList.Count);
	}
	
	// Update is called once per frame
	void Update () {
        //Quaternion.Euler(new Vector3(sliderX.value, selectedObject.rotation.eulerAngles.y, selectedObject.rotation.eulerAngles.z)
        rotation = Quaternion.Euler(AngleOrigin);
    }

    public Vector3 returnLocation()
    {
        return mCombinedParentXform.MultiplyPoint(Vector3.zero);
    }

   
    private void InitializeSceneNode()
    {
        mCombinedParentXform = Matrix4x4.identity;
    }

    // This must be called _BEFORE_ each draw!! 
    public void CompositeXform(ref Matrix4x4 parentXform)
    {
        Matrix4x4 orgT = Matrix4x4.Translate(NodeOrigin);
        Matrix4x4 orgR = Matrix4x4.Rotate(rotation);
        //Debug.Log("localPos:" + transform.localPosition + " localRot: " + transform.localRotation + " localScale: " + transform.localScale);
        Matrix4x4 trs = Matrix4x4.TRS(transform.localPosition, transform.localRotation, transform.localScale);
        
        mCombinedParentXform = parentXform * orgT * orgR * trs;

        // propagate to all children
        foreach (Transform child in transform)
        {
            SceneNode cn = child.GetComponent<SceneNode>();
            if (cn != null)
            {
                cn.CompositeXform(ref mCombinedParentXform);
            }
        }
        
        // disenminate to primitives
        foreach (NodePrimitive p in PrimitiveList)
        {
            p.LoadShaderMatrix(ref mCombinedParentXform);
        }
        
        // Compute AxisFrame 
        if (AxisFrame != null)
        {
            AxisFrame.localPosition = mCombinedParentXform.MultiplyPoint(Vector3.zero);

            Vector3 up = mCombinedParentXform.GetColumn(1).normalized;
            Vector3 forward = mCombinedParentXform.GetColumn(2).normalized;
            float angle = Mathf.Acos(Vector3.Dot(Vector3.up, up)) * Mathf.Rad2Deg;
            Vector3 axis = Vector3.Cross(Vector3.up, up);
            AxisFrame.localRotation = Quaternion.AngleAxis(angle, axis);

            angle = Mathf.Acos(Vector3.Dot(AxisFrame.transform.forward, forward)) * Mathf.Rad2Deg;
            axis = Vector3.Cross(AxisFrame.transform.forward, forward);
            AxisFrame.localRotation = Quaternion.AngleAxis(angle, axis) * AxisFrame.localRotation;
        }

    }

    public Vector3 returnAxisPos()
    {
        return mCombinedParentXform.MultiplyPoint(Vector3.zero);
    }
    public Vector3 upVector()
    {
        return mCombinedParentXform.GetColumn(1).normalized;
    }
    public Vector3 fowardVector()
    {
        return mCombinedParentXform.GetColumn(2).normalized;
    }

    public Quaternion returnRotationPartOne(Transform trans)
    {
        
        Vector3 up = mCombinedParentXform.GetColumn(1).normalized;
        //Vector3 forward = mCombinedParentXform.GetColumn(2).normalized;
        float angle = Mathf.Acos(Vector3.Dot(Vector3.up, up)) * Mathf.Rad2Deg;
        Vector3 axis = Vector3.Cross(Vector3.up, up);
        return Quaternion.AngleAxis(angle, axis);


    }
    public Quaternion returnRotationPartTwo(Transform trans)
    {
        Vector3 forward = mCombinedParentXform.GetColumn(2).normalized;
       
        float angle = Mathf.Acos(Vector3.Dot(trans.forward, forward)) * Mathf.Rad2Deg;
        Vector3 axis = Vector3.Cross(trans.forward, forward);
        Quaternion quat = Quaternion.AngleAxis(angle, axis) * trans.localRotation; 
        return quat;
    }
    public Quaternion returnRotationPartThree(Transform trans)
    {
        
        Vector3 right = Vector3.Cross(mCombinedParentXform.GetColumn(1).normalized, mCombinedParentXform.GetColumn(2).normalized);
        //Debug.Log("Real right " + right);
        
        if (-right == trans.right)
        {
            return Quaternion.AngleAxis(180, trans.up) * trans.localRotation;
            //Debug.Log("bad right " + trans.right); 
        }
        return Quaternion.AngleAxis(0, trans.up) * trans.localRotation;
    }


    public void debug(Transform trans)
    {
        Vector3 x = mCombinedParentXform.GetColumn(1).normalized;
        Vector3 y = mCombinedParentXform.GetColumn(2).normalized;
        Vector3 z = Vector3.Cross(x, y);
        Debug.Log("x " + x);
        Debug.DrawLine(trans.localPosition, trans.localPosition + 20 * x, Color.red);
        Debug.Log("y " + y);
        Debug.DrawLine(trans.localPosition, trans.localPosition + 20* y, Color.green);
        Debug.Log("z " + z);
        Debug.DrawLine(trans.localPosition, trans.localPosition + 20 *z, Color.blue);
    }
}

