using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Controller_Data : MonoBehaviour
{
    //Index 0 = Translation toggle
    //Index 1 = Scaling toggle
    //Index 2 = Rotation toggle
    [SerializeField]
    GameObject[] toggleObjects;
    GameObject currentToggleObj = null;

    //Index 0 = x slider
    //Index 1 = y slider
    //Index 2 = z slider
    [SerializeField]
    GameObject[] sliderObjs;
    Slider[] sliders;
    [SerializeField]
    GameObject[] sliderTextObjs;
    Text[] sliderValueText;

    [SerializeField]
    float sliderMin, sliderMax;

    [SerializeField]
    MyMesh meshScript;

    float prevSliderVal0, prevSliderVal1, prevSliderVal2;

    Vector2 curTranslation, curScale;
    float curRotation;

    //A flag to tell if a toggle was just swapped
    //This is meant to keep from re-updating textures any time
    //a toggle gets swapped
    bool toggleSwapped;

    // Start is called before the first frame update
    void Start()
    {
        //Instantiate the slider and slider value arrays
        sliders = new Slider[sliderObjs.Length];
        sliderValueText = new Text[sliderTextObjs.Length];

        //Populate the arrays
        for(int i = 0; i < sliderObjs.Length; ++i)
        {
            sliders[i] = sliderObjs[i].GetComponent<Slider>();
            sliderValueText[i] = sliderTextObjs[i].GetComponent<Text>();
        }

        currentToggleObj = toggleObjects[0];

        prevSliderVal0 = sliders[0].value;
        prevSliderVal1 = sliders[1].value;
        prevSliderVal2 = sliders[2].value;

        curTranslation = new Vector2(0, 0);
        curScale = new Vector2(1, 1);
        curRotation = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Update the text above each slider
        for (int i = 0; i < sliders.Length; ++i)
        {
            sliderValueText[i].text = sliders[i].value.ToString();
        }

        //Track slider values based on the current toggle, and save the data
        if (currentToggleObj == toggleObjects[0])
        {
            curTranslation = new Vector2(sliders[0].value, sliders[2].value);
        }
        else if (currentToggleObj == toggleObjects[1])
        {
            curScale = new Vector2(sliders[0].value, sliders[2].value);
        }
        else
        {
            curRotation = sliders[1].value;
        }
    }

    public void UpdateTexture()
    {
        if(toggleSwapped == false)
        {
            //Update the appropriate part of the plane's transform based on the chosen toggle and slider values
            if (currentToggleObj == toggleObjects[0])
            {
                Vector2 newTranslation = new Vector2(sliders[0].value - prevSliderVal0, sliders[2].value - prevSliderVal2);
                meshScript.UpdateTexture(newTranslation, 0, new Vector2(1, 1));
                //    curTranslation = newTranslation;
            }
            else if (currentToggleObj == toggleObjects[1])
            {
                Vector2 newScale = new Vector2(prevSliderVal0 / sliders[0].value, prevSliderVal2 / sliders[2].value);
                meshScript.UpdateTexture(new Vector2(0, 0), 0, newScale);
                //    curScale = newScale;
            }
            else
            {
                float newRotation = sliders[1].value - prevSliderVal1;
                meshScript.UpdateTexture(new Vector2(0, 0), newRotation, new Vector2(1, 1));
                //    curRotation = newRotation;
            }


            prevSliderVal0 = sliders[0].value;
            prevSliderVal1 = sliders[1].value;
            prevSliderVal2 = sliders[2].value;
        }

    }

    //Function to change the current toggle (checkbox)
    public void SetCurrentToggle(GameObject newToggleObj)
    {
        toggleSwapped = true;

        //First deactivate all toggles
        foreach(GameObject toggleObj in toggleObjects)
        {
            toggleObj.GetComponent<Toggle>().isOn = false;
        }

        //Then turn on the current toggle
        newToggleObj.GetComponent<Toggle>().isOn = true;

        currentToggleObj = newToggleObj;

        //Update slider limits based on the toggle
        if (currentToggleObj == toggleObjects[0])
        {
            SetSliderLimits(-4, 4);

            //Reset appropriate previous slider values
            prevSliderVal0 = curTranslation.x;
            prevSliderVal2 = curTranslation.y;

            //Disable unnecessary sliders
            ToggleSliders(true, false, true);
        }
        else if (currentToggleObj == toggleObjects[1])
        {
            SetSliderLimits(.1f, 10f);

            //Reset appropriate previous slider values
            prevSliderVal0 = curScale.x;
            prevSliderVal2 = curScale.y;

            //Disable unnecessary sliders
            ToggleSliders(true, false, true);
        }
        else
        {
            SetSliderLimits(-180, 180);

            //Reset appropriate previous slider values
            prevSliderVal1 = curRotation;

            //Disable unnecessary sliders
            ToggleSliders(false, true, false);
        }

        GetSelectedData();

        toggleSwapped = false;

    }

    void ToggleSliders(bool toggle1, bool toggle2, bool toggle3)
    {
        sliders[0].interactable = toggle1;
        sliders[1].interactable = toggle2;
        sliders[2].interactable = toggle3;
    }

    //Function to change slider limits
    public void SetSliderLimits(float min, float max)
    {
        //Change the slider min/max values to new parameter values
        sliderMin = min;
        sliderMax = max;

        //Apply the modified min/max to each slider
        foreach(GameObject sliderObj in sliderObjs)
        {
            Slider curSlider = sliderObj.GetComponent<Slider>();
            curSlider.minValue = sliderMin;
            curSlider.maxValue = sliderMax;
        }
    }

    //Function to get transform data 
    void GetSelectedData()
    {
        //Get the appropriate part of the transform's data
        //based on the current toggle
        if (currentToggleObj == toggleObjects[0])
        {
        //    Vector2 translation = meshScript.GetTranslation();
            sliders[0].value = curTranslation.x;
            sliders[1].value = 0;
            sliders[2].value = curTranslation.y;
        }
        else if (currentToggleObj == toggleObjects[1])
        {
        //    Vector2 scale = meshScript.GetScale();
            sliders[0].value = curScale.x;
            sliders[1].value = 0.1f;
            sliders[2].value = curScale.y;
        }
        else
        {
        //    float rotation = meshScript.GetRotation();
            sliders[0].value = 0;
            sliders[1].value = curRotation;
            sliders[2].value = 0;
        }

    }

}
