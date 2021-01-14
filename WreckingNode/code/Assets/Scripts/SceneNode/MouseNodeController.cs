using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseNodeController : MonoBehaviour
{
    Vector3 mouseOffset;
   // Quaternion rotationOffset;

    float mouseZCoord;

    [SerializeField]
    GameObject associatedNode;

    private void OnMouseDown()
    {
        mouseZCoord = Camera.main.WorldToScreenPoint(associatedNode.transform.localPosition).z;

        mouseOffset = associatedNode.transform.localPosition - GetMouseWorldPos();
    //    rotationOffset = transform.localRotation - associatedNode.transform.localRotation;
    }

    private void OnMouseDrag()
    {
        Vector3 newPos = GetMouseWorldPos() + mouseOffset;
        //    transform.position = newPos;
        associatedNode.transform.localPosition = newPos;
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mouseZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
