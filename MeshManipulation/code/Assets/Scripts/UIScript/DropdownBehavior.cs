using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropdownBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject cylindermesh;
    public GameObject planemesh;
    public GameObject cubemesh;
    private void Start()
    {
        cylindermesh.SetActive(false);
        cubemesh.SetActive(false);
    }

    private void selectCylinder()
    {
        planemesh.SetActive(false);
        cylindermesh.SetActive(true);
        cubemesh.SetActive(false);
    }

    private void selectPlane()
    {
        planemesh.SetActive(true);
        cylindermesh.SetActive(false);
        cubemesh.SetActive(false);
    }

    private void selectCube()
    {
        planemesh.SetActive(false);
        cylindermesh.SetActive(false);
        cubemesh.SetActive(true);
    }

    public void changeDropdownValue(int val)
    {
        if (val == 0)
            selectPlane();
        else if (val == 1)
            selectCylinder();
        else
            selectCube();
    }
}
