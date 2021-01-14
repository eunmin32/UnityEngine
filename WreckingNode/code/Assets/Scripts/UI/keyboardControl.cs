using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class keyboardControl : MonoBehaviour
{
    
    List<Transform> sceneNodeList;
    public int sceneNodeIndex = 0;
    int axisIndex = 0; //(0: y-axis, 1: x-axis 1: z-axis)

    public SceneNode Root;
    public Text NodeName;
    public Text AxisName;
    public Transform axisFrame;

    // Start is called before the first frame update
    void Start()
    {
        sceneNodeList = new List<Transform>();
        //Always only one child each 
        //add all sceneNode to the list
        sceneNodeList.Add(Root.transform);
        int i = 0;
        while (sceneNodeList[i].transform.childCount != 0)
        {
            sceneNodeList.Add(sceneNodeList[i].GetChild(0));
            i++;
        }
        setNodeName();
        setAxisName();
        setAxis();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        keyboardInputControl();
        
    }
    
    public void setNode(int newNodeNum)
    {
        sceneNodeList[sceneNodeIndex].GetComponent<SceneNode>().AxisFrame = null;
        sceneNodeIndex = newNodeNum;
        sceneNodeList[sceneNodeIndex].GetComponent<SceneNode>().AxisFrame = axisFrame.transform;
        axisFrame.GetComponent<LocateAtNode>().TopSceneNode = sceneNodeList[sceneNodeIndex].GetComponent<SceneNode>();
        setNodeName();
        setAxis();
    }

    private void keyboardInputControl()
    { 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            //space bar changes sceneNode selection 
            sceneNodeList[sceneNodeIndex].GetComponent<SceneNode>().AxisFrame = null;
            sceneNodeIndex++;
            if (sceneNodeIndex == sceneNodeList.Count) //you don't control the last node
                sceneNodeIndex = 0;
            setNodeName();
            setAxis();
            if (sceneNodeIndex == 0)
            {
                axisIndex = 0; setAxisName();
            }

            sceneNodeList[sceneNodeIndex].GetComponent<SceneNode>().AxisFrame = axisFrame.transform;
            axisFrame.GetComponent<LocateAtNode>().TopSceneNode = sceneNodeList[sceneNodeIndex].GetComponent<SceneNode>();
            setNodeName();
        }

        //we can controll 0: y rotation, 1,2: all rotation and 3: no control
        //'w' and 's' change axis
        //A and D change angle 
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (sceneNodeIndex == 0)
            {
                axisIndex = 0; setAxisName(); return;
            }
            axisIndex++;
            if (axisIndex == 3)
                axisIndex = 0;
            setAxisName();

        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (sceneNodeIndex == 0)
            {
                axisIndex = 0; setAxisName(); return;
            }
            axisIndex--;
            if (axisIndex == -1)
                axisIndex = 2;
            setAxisName();
        }
        if (Input.GetKey(KeyCode.A))
        {
            changeAngle(-2);
        }
        if (Input.GetKey(KeyCode.D))
        {
            changeAngle(2);
        }

    }

    public void clickAxis(int axisIndex)
    {
        this.axisIndex = axisIndex;
        setAxisName();
    }

    private void setAxisName()
    {//(0: y-axis, 1: x-axis 2: z-axis)
        string axisName;
        if (axisIndex == 0)
            axisName = "y-axis";
        else if (axisIndex == 1)
            axisName = "x-axis";
        else
            axisName = "z-axis";
        AxisName.text = axisName;
    }

    public void setNodeName()
    {
        NodeName.text = sceneNodeList[sceneNodeIndex].name;
    }

    public void setAxis()
    {
        foreach(Transform child in axisFrame)
        {
            child.GetComponent<RotationController>().sceneNode = sceneNodeList[sceneNodeIndex].gameObject;
            if(sceneNodeIndex == 0)
            {
                if(child.GetComponent<RotationController>().xAxis == true || child.GetComponent<RotationController>().zAxis == true)
                {
                    child.gameObject.SetActive(false);
                }
                axisIndex = 0;
            } else
                child.gameObject.SetActive(true);
        }
        setAxisName();
    }

    private void changeAngle(float angle)
    {
        Quaternion q;
        if (axisIndex == 0) //y
        {
            q = Quaternion.AngleAxis(angle, Vector3.up);
        }
        else if (axisIndex == 1) //x
        {
            if (sceneNodeIndex == 0)
                return;
            q = Quaternion.AngleAxis(angle, Vector3.right);
        }
        else //z
        {
            if (sceneNodeIndex == 0)
                return;
            q = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        sceneNodeList[sceneNodeIndex].transform.localRotation *= q;
    }
}
