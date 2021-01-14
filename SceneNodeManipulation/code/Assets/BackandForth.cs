using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BackandForth : MonoBehaviour
{
    public char rotationDir;
    public float interval = 4f;

    private Vector3 Axis;
    private float speed = 1f;
    private float remainingTime;
   
    // Start is called before the first frame update
    void Start()
    {
        setRotationDirection();
        remainingTime = interval / 2;
    }


    // Update is called once per frame
    void Update() {
        rotate();
        checkTime();
    }

    void setRotationDirection()
    {
        if (rotationDir == 'X' || rotationDir == 'x')
            Axis = Vector3.right;
        else if (rotationDir == 'Y' || rotationDir == 'y')
            Axis = Vector3.up;
        else if (rotationDir == 'Z' || rotationDir == 'z')
            Axis = Vector3.forward;
    }

    void rotate()
    {
        Quaternion q = Quaternion.AngleAxis(speed, Axis);
        transform.localRotation *= q;
    }

    void checkTime()
    {
         if (remainingTime > 0){
                remainingTime -= Time.deltaTime;
         } else
        {
            speed = -speed;
            remainingTime = interval;
        }
        
    }

   
}
