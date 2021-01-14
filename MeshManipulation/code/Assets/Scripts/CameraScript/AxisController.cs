using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisController : MonoBehaviour
{
    GameObject origin;

    Vector3 mouseOffset, originOffset;

    float mouseZCoord;

    [SerializeField]
    bool xAxis, yAxis, zAxis;

    [SerializeField]
    Material selectedMat, originalMat;

    MeshRenderer axisRenderer;

    private void Start()
    {
        origin = transform.parent.gameObject;
        originOffset = origin.transform.position - transform.position;

        axisRenderer = GetComponent<MeshRenderer>();
        originalMat = axisRenderer.material;
    }

    private void OnMouseDown()
    {
        mouseZCoord = Camera.main.WorldToScreenPoint(transform.position).z;

        mouseOffset = transform.position - GetMouseWorldPos();

        //Set the axis color to the selected color
        axisRenderer.material = selectedMat;
    }

    private void OnMouseDrag()
    {
        //Move the axis according to the mouse movement, dependant on the
        //axis clicked on by the user
        if (!Input.GetKey(KeyCode.LeftAlt))
        {

            if (xAxis)
            {
                float newX = GetMouseWorldPos().x + mouseOffset.x;
                transform.position = new Vector3(newX, transform.position.y, transform.position.z);
            }
            else if (yAxis)
            {
                float newY = GetMouseWorldPos().y + mouseOffset.y;
                transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            }
            else
            {
                float newZ = GetMouseWorldPos().z + mouseOffset.z;
                transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
            }


            //Move the origin according to its predetermined offset with the axis
            origin.transform.position = transform.position + originOffset;
        }
    }

    void OnMouseUp()
    {
        //Set the axis color to the selected color
        axisRenderer.material = originalMat;
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mouseZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
