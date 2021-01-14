using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 *  Script to swap secondary cameras
 */

public class Camera_Swap : MonoBehaviour
{

    [SerializeField]
    GameObject[] camObjs;

    [SerializeField]
    Dropdown camDropdown;

    public void SwapCameras()
    {
        //Turn off all active secondary cameras
        foreach(GameObject camObj in camObjs)
        {
            camObj.SetActive(false);
        }

        //Enable the target camera
        int camIndex = camDropdown.value;
        camObjs[camIndex].SetActive(true);
    }
}
