using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
public class CylinderControlPanel : MonoBehaviour
{
    [SerializeField]
    Slider rotationSlider;
    [SerializeField]
    Slider resolutionSlider;
    [SerializeField]
    Slider radiusSlider;
    [SerializeField]
    Slider heightSlider;
    [SerializeField]
    Text rotationValue;
    [SerializeField]
    Text resolutionValue;
    [SerializeField]
    Text radiusValue;
    [SerializeField]
    Text heightValue;

    [SerializeField]
    CylinderMesh mesh;
    // Start is called before the first frame update
    void Start()
    {/*
        Assert.IsNotNull(mesh);
        rotationChanged(rotationSlider.value);
        resolutionChanged(resolutionSlider.value);
        radiusChanged(radiusSlider.value);
        heightChanged(heightSlider.value);
        */
        rotationSlider.onValueChanged.AddListener(rotationChanged);
        resolutionSlider.onValueChanged.AddListener(resolutionChanged);
        radiusSlider.onValueChanged.AddListener(radiusChanged);
        heightSlider.onValueChanged.AddListener(heightChanged);
    } 

    void rotationChanged(float val)
    {
        mesh.changeDegree(val);
        rotationValue.text = val.ToString();
    }
    void resolutionChanged(float val)
    {
        mesh.changeResolution((int)val);
        resolutionValue.text = val.ToString();
    }
    void radiusChanged(float val)
    {
        mesh.changeRad(val);
        radiusValue.text = val.ToString();
    }
    void heightChanged(float val)
    {
        mesh.changeHeight(val);
        heightValue.text = val.ToString();
    }
}
