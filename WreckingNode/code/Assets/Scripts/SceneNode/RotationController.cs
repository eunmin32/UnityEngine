using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour
{
    [SerializeField]
    //GameObject sceneNode, controllerPosition;
    public GameObject sceneNode;
    [SerializeField]
    public bool xAxis = false, yAxis = false, zAxis = false;
    [SerializeField]
    public keyboardControl keyboard;
    float rotationSpeedDown = 10f;

    float xRot, yRot, zRot, sceneNodeXRot, sceneNodeYRot, sceneNodeZRot;

    Vector3 startPosition;
    float rotationDamping = .2f;


    private void OnMouseDown()
    {
        startPosition = Input.mousePosition;
        if (yAxis == true)
            keyboard.clickAxis(0);
        else if (xAxis == true)
            keyboard.clickAxis(1);
        else
            keyboard.clickAxis(2);
    }

    private void OnMouseDrag()
    {
        Vector3 mouseDelta = Input.mousePosition - startPosition;
        float sign = 0;

        if (mouseDelta.x < 0)
            sign = 1;
        else
            sign = -1;

        Debug.Log("Dragging");
        if (xAxis)
        {
            Debug.Log("x change");
            //Rotates around the z-axis because the rotated model from Blender has an axis mismatch
            //xRot = transform.localEulerAngles.z + Input.GetAxis("Mouse X") * rotationSpeed * Mathf.Deg2Rad;
            //transform.rotation = Quaternion.AngleAxis(Input.GetAxis("Mouse X"), transform.right);
            //sceneNode.transform.rotation = transform.rotation;
            //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, xRot);

            //sceneNode.transform.rotation *= Quaternion.AngleAxis(Input.GetAxis("Mouse X"), Vector3.right);
            sceneNode.transform.rotation *= Quaternion.AngleAxis(mouseDelta.magnitude * rotationDamping * sign / rotationSpeedDown, Vector3.right);

            //sceneNodeXRot = sceneNode.transform.localEulerAngles.z + Input.GetAxis("Mouse X") * rotationSpeed * Mathf.Deg2Rad;
            //sceneNode.transform.localEulerAngles = new Vector3(sceneNode.transform.localEulerAngles.x, sceneNode.transform.localEulerAngles.y, sceneNodeXRot);
        }
        else if (yAxis)
        {
            Debug.Log("y change");
            //yRot = transform.localEulerAngles.y - Input.GetAxis("Mouse X") * rotationSpeed * Mathf.Deg2Rad;
            //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, yRot, transform.localEulerAngles.z);

         //   sceneNode.transform.rotation *= Quaternion.AngleAxis(Input.GetAxis("Mouse X"), Vector3.up);
            sceneNode.transform.rotation *= Quaternion.AngleAxis(mouseDelta.magnitude * rotationDamping * sign / rotationSpeedDown, Vector3.up);

            //sceneNodeYRot = sceneNode.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * rotationSpeed * Mathf.Deg2Rad;
            //sceneNode.transform.localEulerAngles = new Vector3(sceneNode.transform.localEulerAngles.x, sceneNodeYRot, sceneNode.transform.localEulerAngles.z);
            //sceneNode.transform.rotation 
        }
        else
        {
            //zRot = transform.localEulerAngles.z - Input.GetAxis("Mouse Y") * rotationSpeed * Mathf.Deg2Rad;
            //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, zRot);

        //    sceneNode.transform.rotation *= Quaternion.AngleAxis(Input.GetAxis("Mouse X"), Vector3.forward);
            sceneNode.transform.rotation *= Quaternion.AngleAxis(mouseDelta.magnitude * rotationDamping * sign / rotationSpeedDown, Vector3.forward);

            //sceneNodeZRot = sceneNode.transform.localEulerAngles.z + Input.GetAxis("Mouse X") * rotationSpeed * Mathf.Deg2Rad;
            //sceneNode.transform.localEulerAngles = new Vector3(sceneNode.transform.localEulerAngles.x, sceneNode.transform.localEulerAngles.y, sceneNodeZRot);
        }
    }
}
