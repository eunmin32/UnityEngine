using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//Class for selecting a toggle
public class Toggle_Data : MonoBehaviour, IPointerClickHandler
{
    GameObject mainController;

    Controller_Data dataScript;

    [SerializeField]
    float min, max;

    void Start ()
    {
        mainController = GameObject.FindGameObjectWithTag("Main Controller");
        dataScript = mainController.GetComponent<Controller_Data>();
    }

    //Function to select a current toggle when it's clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        dataScript.SetCurrentToggle(gameObject);
    }
}
