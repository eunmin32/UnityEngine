using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WreckingBallCam : MonoBehaviour
{
    [SerializeField]
    GameObject wreckingBall;

    // Update is called once per frame
    void Update()
    {
    //    transform.forward = wreckingBall.transform.forward;
        transform.up = Vector3.up;
    }
}
