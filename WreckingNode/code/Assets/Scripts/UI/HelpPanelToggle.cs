using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpPanelToggle : MonoBehaviour
{
    public GameObject HelpPanel;
    // Start is called before the first frame update
    public void turnOff()
    {
        HelpPanel.active = false;
    }

    public void turnOn()
    {
        HelpPanel.active = true;
    }
}
