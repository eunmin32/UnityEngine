using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtControl : MonoBehaviour
{
    public SliderWithEcho X, Y, Z;
    
    public Transform axisFrameLook;
    private Vector3 mPreviousSliderValues = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
       
        X.SetSliderListener(XValueChanged);
        Y.SetSliderListener(YValueChanged);
        Z.SetSliderListener(ZValueChanged);
        SetInitial();
    }
    private Vector3 ReadObjectXfrom()
    {
        Vector3 p;
        p = axisFrameLook.localPosition;
        return p;
    }

        void SetInitial()
    {
        Vector3 p = ReadObjectXfrom();
        mPreviousSliderValues = p;
        X.InitSliderRange(-20, 20, p.x);
        Y.InitSliderRange(-20, 20, p.y);
        Z.InitSliderRange(-20, 20, p.z);
    }
    void XValueChanged(float v)
    {
        Vector3 p = ReadObjectXfrom();
        // if not in rotation, next two lines of work would be wasted
        float dx = v - mPreviousSliderValues.x;
        mPreviousSliderValues.x = v;
        Quaternion q = Quaternion.AngleAxis(dx, Vector3.right);
        p.x = v;
        UISetObjectXform(ref p, ref q);
    }

    void YValueChanged(float v)
    {
        Vector3 p = ReadObjectXfrom();
        // if not in rotation, next two lines of work would be wasted
        float dy = v - mPreviousSliderValues.y;
        mPreviousSliderValues.y = v;
        Quaternion q = Quaternion.AngleAxis(dy, Vector3.up);
        p.y = v;
        UISetObjectXform(ref p, ref q);
    }

    void ZValueChanged(float v)
    {
        Vector3 p = ReadObjectXfrom();
        // if not in rotation, next two lines of work would be wasterd
        float dz = v - mPreviousSliderValues.z;
        mPreviousSliderValues.z = v;
        Quaternion q = Quaternion.AngleAxis(dz, Vector3.forward);
        p.z = v;
        UISetObjectXform(ref p, ref q);
    }
    private void UISetObjectXform(ref Vector3 p, ref Quaternion q)
    {
        axisFrameLook.localPosition = p;

    }
}
