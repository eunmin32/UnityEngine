using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocateAtNode : MonoBehaviour
{
    public SceneNode TopSceneNode;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (TopSceneNode != null) {
        transform.localPosition = TopSceneNode.returnLocation();
        transform.localRotation = TopSceneNode.returnRotationPartOne(transform);
        transform.localRotation = TopSceneNode.returnRotationPartTwo(transform);
       transform.localRotation = TopSceneNode.returnRotationPartThree(transform);
        }
    }
}
