using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceWithMouse : MonoBehaviour
{
    bool mouseDown = false;
    // Start is called before the first frame update
    Rigidbody rb;
    public float power = 100f;
    public CameraManipulation MainCam;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    

    private void addForceToMouseDir()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");
        Vector3 forceDirection = (MainCam.transform.up * y + MainCam.transform.right * x) * power;
        rb.AddForce(forceDirection.x, forceDirection.y, forceDirection.z, ForceMode.Force);
    }
    private void OnMouseDrag()
    {
        addForceToMouseDir();
    }
}
