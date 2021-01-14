using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class cameraNode : MonoBehaviour
{

    protected Matrix4x4 mCombinedParentXform;
    
    public Vector3 NodeOrigin = Vector3.zero;
    public Vector3 AngleOrigin = Vector3.zero;

    public Transform scenenode;
    public List<SmallViewCamera> PrimitiveList;
    public Transform direction = null;

    private Quaternion rotation;
    // Use this for initialization
    protected void Start()
    {
        InitializeSceneNode();
        // Debug.Log("PrimitiveList:" + PrimitiveList.Count);
    }

    // Update is called once per frame
    void Update()
    {
        //Quaternion.Euler(new Vector3(sliderX.value, selectedObject.rotation.eulerAngles.y, selectedObject.rotation.eulerAngles.z)
        rotation = Quaternion.Euler(AngleOrigin);
    }

    private void InitializeSceneNode()
    {
        mCombinedParentXform = Matrix4x4.identity;
    }

    // This must be called _BEFORE_ each draw!! 
    public void CompositeCamform()
    {
        Matrix4x4 orgT = Matrix4x4.Translate(NodeOrigin);
        Matrix4x4 orgR = Matrix4x4.Rotate(rotation);
        //Debug.Log("localPos:" + transform.localPosition + " localRot: " + transform.localRotation + " localScale: " + transform.localScale);
        Matrix4x4 trs = Matrix4x4.TRS(transform.localPosition, transform.localRotation, transform.localScale);

       // mCombinedParentXform = scenenode.GetComponent<SceneNode>().returnCombined() * orgT * orgR * trs;

        
        // disenminate to primitives
        foreach (SmallViewCamera p in PrimitiveList)
        {
            //p.LoadShaderMatrix(ref mCombinedParentXform);
        }

        // Compute AxisFrame 
        
        if (direction != null)
        {
            direction.localPosition = mCombinedParentXform.MultiplyPoint(Vector3.zero);

            Vector3 up = mCombinedParentXform.GetColumn(1).normalized;
            Vector3 forward = mCombinedParentXform.GetColumn(2).normalized;
            float angle = Mathf.Acos(Vector3.Dot(Vector3.up, up)) * Mathf.Rad2Deg;
            Vector3 axis = Vector3.Cross(Vector3.up, up);
            direction.localRotation = Quaternion.AngleAxis(angle, axis);

            angle = Mathf.Acos(Vector3.Dot(direction.transform.forward, forward)) * Mathf.Rad2Deg;
            axis = Vector3.Cross(direction.transform.forward, forward);
            direction.localRotation = Quaternion.AngleAxis(angle, axis) * direction.localRotation;
        }

    }
}
 