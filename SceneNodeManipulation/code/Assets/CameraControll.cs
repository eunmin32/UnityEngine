using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    public Camera mainCamera;
    public Transform LookAxisFrame;
    Vector2 origMousePosition = Vector2.zero;
    // Start is called before the first frame update

    public float maxy;
    public float miny;
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
            moveCamAndAxis();
        if (Input.GetKeyUp(KeyCode.LeftAlt))
            //origMousePosition = Vector2.zero;
        if (Input.GetMouseButtonDown(1)) {  //Right Click

            
        }
    }

    private void moveCamAndAxis()
    {
        // if (origMousePosition == Vector2.zero)
        //     origMousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        // Vector2 mousePosition = Vector2.zero;
        // if (Input.GetMouseButton(0))
        //{
        //    mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y); 
        //  }
        //Debug.Log("orig: " + origMousePosition + " curr: " + mousePosition);
        //Debug.Log("curr - orig: " + (mousePosition - origMousePosition));
        if (Input.GetMouseButton(0))
        {
            //mainCamera.transform.position.x = mainCamera.transform.position.x - Input.mousePosition.x *Time.deltaTime;
        }
    }

}
