using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CameraManipulation : MonoBehaviour {

    public Transform LookAtPosition = null;
    Vector3 V;
    Vector3 U;
    Vector3 W;
    Vector3 initialPos;
    Vector3 initialAxisPos;
    float CameraMoveBorderLength = 15f;
    float CameraSpeed = 15f;
    public Vector2 cameraBound; 
    // Use this for initialization
    private bool mouseInput = false;
    private Vector3 org;
    void Start () {
        Debug.Assert(LookAtPosition != null);
        initialPos = transform.position;
        initialAxisPos = LookAtPosition.position;
    }
	
	// Update is called once per frame
	void Update () {

        V = LookAtPosition.localPosition - transform.localPosition;
        W = Vector3.Cross(-V, Vector3.up);
        U = Vector3.Cross(W, -V);
                // transform.localRotation = Quaternion.LookRotation(V, U);
                transform.localRotation = Quaternion.FromToRotation(Vector3.up, U);
                Quaternion alignU = Quaternion.FromToRotation(transform.forward, V);
                transform.localRotation = alignU * transform.localRotation;
        interaction();
        revertToInitialPosition();

    }

    private bool mouseInsideScreen()
    {
        Rect screenRect = new Rect(0, 0, Screen.width, Screen.height);
        if (!screenRect.Contains(Input.mousePosition))
            return false;
        return true;
    }

    void interaction()
    {
        Vector3 posLook = LookAtPosition.position;
        Vector3 posnow = transform.position;

        //zoom in and out
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        posnow += (scroll * V);

            if (mouseInsideScreen() && Input.mousePosition.y >= Screen.height - CameraMoveBorderLength && !Input.GetMouseButton(2))
        {
            Vector3 forwardDir = -(posnow - posLook).normalized;
            forwardDir.y = 0;
            posnow += CameraSpeed * Time.deltaTime * forwardDir;
            posLook += CameraSpeed * Time.deltaTime * forwardDir;
            
        }
        if (mouseInsideScreen() && Input.mousePosition.y <= CameraMoveBorderLength && !Input.GetMouseButton(2))
        {
            Vector3 backwardDir = (posnow - posLook).normalized;
            backwardDir.y = 0;
            posnow += CameraSpeed * Time.deltaTime * backwardDir;
            posLook += CameraSpeed * Time.deltaTime * backwardDir;
        }

        if (mouseInsideScreen() && Input.mousePosition.x <= CameraMoveBorderLength && !Input.GetMouseButton(2))
        {
            Vector3 leftDir = (posnow - posLook).normalized;
            leftDir.y = 0;
            leftDir = Quaternion.Euler(0, 90, 0) * leftDir;
            posnow += CameraSpeed * Time.deltaTime * leftDir;
            posLook += CameraSpeed * Time.deltaTime * leftDir;
        }
        if (mouseInsideScreen() && Input.mousePosition.x >= Screen.width - CameraMoveBorderLength && !Input.GetMouseButton(2))
        {
            Vector3 rightDir = -(posnow - posLook).normalized;
            rightDir.y = 0;
            rightDir = Quaternion.Euler(0, 90, 0) * rightDir;
            posnow += CameraSpeed * Time.deltaTime * rightDir;
            posLook += CameraSpeed * Time.deltaTime * rightDir;
        }
        //posLook.x = Mathf.Clamp(posLook.x, -cameraBound.x, cameraBound.x);
        //posLook.z = Mathf.Clamp(posLook.y, -cameraBound.y, cameraBound.y);
        //posnow.x = Mathf.Clamp(posnow.x, -cameraBound.x, cameraBound.x);
       // posnow.z = Mathf.Clamp(posnow.y, -cameraBound.y, cameraBound.y);
        LookAtPosition.position = posLook;
        transform.position = posnow;
        
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(2))
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");
            Vector3 posnow = transform.position;
            posnow -= U.normalized * y * 70f * Time.deltaTime;
            posnow -= W.normalized * x * 70f * Time.deltaTime;
            Vector3 direction = -(LookAtPosition.localPosition - posnow).normalized;
            posnow = LookAtPosition.localPosition + V.magnitude * direction;
            transform.position = posnow;
        }

    }

    private void revertToInitialPosition()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            LookAtPosition.position = initialAxisPos;
            transform.position = initialPos;
        }
    }
    
}
