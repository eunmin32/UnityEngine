using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
public class FractureBehavior : MonoBehaviour
{
    float fractureRelativeVelocity = 0.2f;
    float BallRelativeVelocity = 1f;
    Rigidbody rigidbody;
    wallBehavior wall;
 
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = transform.GetComponent<Rigidbody>();
        while (wall == null)
            wall = transform.parent.GetComponent<wallBehavior>();
        Assert.IsNotNull(wall);
        fractureRelativeVelocity = wall.fractureVelocity();
        BallRelativeVelocity = wall.otherVelocity();
    }


    void OnCollisionEnter(Collision collision)
    {
        breakMaterialByVelocity(collision);
    }

    void breakMaterialByVelocity(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            wall.childToGround();
        }
        else if (collision.gameObject.layer == 8)
        {
            
            if (collision.relativeVelocity.magnitude > fractureRelativeVelocity)
            {
                //Debug.Log("collision with other fracture: " + collision.relativeVelocity.magnitude);
                if (rigidbody.constraints != RigidbodyConstraints.None)
                {
                    rigidbody.constraints = RigidbodyConstraints.None;
                    wall.childBroke();
                }

                rigidbody.AddExplosionForce(5f, transform.parent.localPosition, 5f);
            }
        }
        else
        {
            
            if (collision.relativeVelocity.magnitude > BallRelativeVelocity)
            {
                //Debug.Log("collision with other object: " + collision.relativeVelocity.magnitude);
                if (rigidbody.constraints != RigidbodyConstraints.None)
                {
                    rigidbody.constraints = RigidbodyConstraints.None;
                    wall.childBroke();
                }
                rigidbody.AddExplosionForce(20f, transform.parent.localPosition, 5f);
            }
        }

    }
}
