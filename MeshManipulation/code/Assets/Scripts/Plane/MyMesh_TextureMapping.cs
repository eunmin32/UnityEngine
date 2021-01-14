using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class MyMesh : MonoBehaviour
{
    //[SerializeField]
    //Toggle translate, rotate, scale;
    //Toggle currentToggle;

    //[SerializeField]
    //Slider xSlider, ySlider, zSlider;

    public void UpdateTexture(Vector2 translation, float rotation, Vector2 scale)
    {

        //curTranslation += translation;
        //curScale *= scale;
        //curRotation += rotation;
        //Debug.Log("New translation: " + curTranslation + "; New rotation: " + curRotation + "; New scale: " + curScale);
        Matrix3x3 trsMatrix = Matrix3x3Helpers.CreateTRS(translation, rotation, scale);
        Matrix3x3 translateMatrix = Matrix3x3Helpers.CreateTranslation(translation);
        Matrix3x3 rotateMatrix = Matrix3x3Helpers.CreateRotation(rotation);
        Matrix3x3 scaleMatrix = Matrix3x3Helpers.CreateScale(scale);

        Mesh theMesh = GetComponent<MeshFilter>().mesh;
        Vector2[] uv = theMesh.uv;

        for(int i = 0; i < uv.Length; ++i)
        {
            //    uv[i] = trsMatrix * uv[i];
            uv[i] = translateMatrix * uv[i];
            uv[i] = rotateMatrix * uv[i];
            uv[i] = scaleMatrix * uv[i];
        }
        trsMatrix = Matrix3x3.identity;

        theMesh.uv = uv;
    }

    public Vector2 GetTranslation()
    {
        Debug.Log("Returning translation of " + curTranslation);
        return curTranslation;
    }

    public Vector2 GetScale()
    {
        Debug.Log("Returning scale of " + curScale);
        return curScale;
    }

    public float GetRotation()
    {
        Debug.Log("Returning rotation of " + curRotation);
        return curRotation;
    }
}
