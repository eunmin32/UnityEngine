using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    public Transform LookAtPosition = null;
    Vector3 V;
    Vector3 U;
    Vector3 W;
    // Use this for initialization
    private bool mouseInput = false;
    private Vector3 org;
    void Start()
    {
        Debug.Assert(LookAtPosition != null);
       
    }

    // Update is called once per frame
    void Update()
    {
       
        // Viewing vector is from transform.localPosition to the lookat position
        V = LookAtPosition.localPosition - transform.localPosition;
        W = Vector3.Cross(-V, Vector3.up);
        U = Vector3.Cross(W, -V);
        // transform.localRotation = Quaternion.LookRotation(V, U);
        transform.localRotation = Quaternion.FromToRotation(Vector3.up, U);
        Quaternion alignU = Quaternion.FromToRotation(transform.forward, V);
        transform.localRotation = alignU * transform.localRotation;
        interaction();


    }

    void interaction()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            //zoom in and out
            Vector3 pos = transform.position;
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");
            pos += (scroll * V);
            transform.position = pos;
        }
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButton(1))
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");
            Vector3 posLook = LookAtPosition.position;
            Vector3 posnow = transform.position;
            posLook -= U.normalized * y * 10f * Time.deltaTime;
            posnow -= U.normalized * y * 10f * Time.deltaTime;
            posLook -= W.normalized * x * 10f * Time.deltaTime;
            posnow -= W.normalized * x * 10f * Time.deltaTime;
            LookAtPosition.position = posLook;
            transform.position = posnow;
        }



    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButton(0))
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");
            Vector3 posnow = transform.position;
            posnow -= U.normalized * y * 50f * Time.deltaTime;
            posnow -= W.normalized * x * 50f * Time.deltaTime;
            Vector3 direction = -(LookAtPosition.localPosition - posnow).normalized;
            posnow = LookAtPosition.localPosition + V.magnitude * direction;
            transform.position = posnow;
        }

    }
}
