using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderController : MonoBehaviour
{
    public CylinderMesh cylinderMesh;
    bool on = false;
    public Camera camera;
    int selectedPoint;
    GameObject controller;
    public GameObject vertexControllerPrefab;
    int vertexControlPoint = -1;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) )
        {
            on = !on;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            on = !on;
            Destroy(controller);
            //controllerPoint.transform.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        }
        cylinderMesh.EnableNormalDisplay(on);

        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
            
            if (Input.GetMouseButtonDown(0) && on)
            {
                if (cylinderMesh.isController(objectHit) != -1)
                {
                    vertexControlPoint = cylinderMesh.isController(objectHit);
                    creatVertexController();
                }
            }
            // Do something with the object that was hit by the raycast.
        }
        moveNormalDisplay();

    }

    void creatVertexController()
    {
        if (controller != null)
            Destroy(controller);
        controller = Instantiate(vertexControllerPrefab, cylinderMesh.controllerPosAt(vertexControlPoint), Quaternion.identity);
    }

    void moveNormalDisplay()
    {
        if (vertexControlPoint != -1 && controller != null)
            cylinderMesh.setControllerPosAt(vertexControlPoint, controller.transform.position);
    }

}
