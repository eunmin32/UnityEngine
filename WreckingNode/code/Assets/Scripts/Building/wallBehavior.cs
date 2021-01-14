using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallBehavior : MonoBehaviour
{
    float fragmentsCount = 0;
    float fragmentsOnGroundCount = 0;
    float Minfragments;
    float MinfragmentsOnGround;
    bool wallEnded = false;
    public MainUIController mainUIController;
    public float hardness = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        fragmentsCount = transform.childCount;
        Minfragments = fragmentsCount * 0.99f;
        MinfragmentsOnGround = fragmentsCount * 0.80f;

    }

    public float fractureVelocity()
    {
        return hardness;
    }

     public float otherVelocity()
     {
        return hardness;
    }

        public void childBroke()
    {
        fragmentsCount++;
        wallBrokeCheck();
    }


    public void childToGround()
    {
        fragmentsOnGroundCount++;
        wallBrokeCheck();
    }

    private void wallBrokeCheck()
    {
        if (fragmentsCount > Minfragments)
            Debug.Log("fragment");
        if (fragmentsOnGroundCount > MinfragmentsOnGround)
            Debug.Log("ground");
        if (fragmentsCount > Minfragments && fragmentsOnGroundCount > MinfragmentsOnGround && !wallEnded)
        {
            wallEnded = !wallEnded;
            Debug.Log("Wall Ended");
            mainUIController.brokewall();
        }
    }
}
