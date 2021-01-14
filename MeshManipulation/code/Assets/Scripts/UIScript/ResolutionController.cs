using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionController : MonoBehaviour
{
    [SerializeField]
    Slider resolutionSlider;

    [SerializeField]
    Text sliderText;

    [SerializeField]
    MyMesh meshScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateMesh()
    {
        //Get an integer value from the slider, then set the slider
        //to match that value (to make sure they're the same)
        int sliderVal = Mathf.FloorToInt(resolutionSlider.value);
        resolutionSlider.value = sliderVal;

        //Update the text to display the slider value
        sliderText.text = sliderVal.ToString();

        //Update the mesh with the new slider value (i.e. the new mesh resolution)
        meshScript.UpdateMesh(sliderVal);
    }
}
