using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public partial class MyMesh : MonoBehaviour
{

    GameObject[] mControllers;

    [SerializeField]
    GameObject selectedController, vertexPrefab;

    bool showingControllers = false;

    [SerializeField]
    Material selectedMaterial, defaultMaterial;

    void InitControllers(Vector3[] v)
    {
        mControllers = new GameObject[v.Length];
        for (int i = 0; i < v.Length; i++)
        {
            //mControllers[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            //mControllers[i].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            mControllers[i] = Instantiate(vertexPrefab);

            mControllers[i].transform.localPosition = v[i];
            mControllers[i].transform.parent = this.transform;

            //Add a vertex control script to the vertex
            //    mControllers[i].AddComponent<VertexController>();

            //Tag the vertex
            mControllers[i].transform.tag = "Vertex";
        }

    //    ToggleControllers(false);
    }

    //Script to destroy the vertices (called when changing resolution)
    void ClearControllers()
    {
        foreach(GameObject controller in mControllers)
        {
            Destroy(controller);
        }
    }

    //Script to toggle the control points for vertices on/off
    void ToggleControllers(bool state)
    {
        foreach(GameObject controller in mControllers)
        {
            MeshRenderer controlRend = controller.GetComponent<MeshRenderer>();
            VertexController controlScript = controller.GetComponent<VertexController>();

            controlRend.enabled = state;
            controlScript.enabled = state;

            if (controller == selectedController)
                controlScript.ToggleAxes(state);
            else
                controlScript.ToggleAxes(false);

            controlScript.ToggleNormal(state);
        }
    }

    //Script to tell the mesh which vertex is currently selected
    public void SetSelectedVert(GameObject vertex)
    {
        foreach(GameObject controller in mControllers)
        {
            VertexController controlScript = controller.GetComponent<VertexController>();

            //If the current game object in the loop is the newly selected vertex,
            //update its color. If not, set it to the default color (white)
            if (GameObject.ReferenceEquals(controller, vertex))
            {
                selectedController = controller;
                MeshRenderer controlRenderer = controller.GetComponent<MeshRenderer>();
                controlRenderer.material = selectedMaterial;
                controlScript.ToggleAxes(true);
            }
            else
            {
                MeshRenderer controlRenderer = controller.GetComponent<MeshRenderer>();
                controlRenderer.material = defaultMaterial;
                controlScript.ToggleAxes(false);
            }
        }
    }
}
