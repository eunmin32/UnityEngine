using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CubeController : MonoBehaviour
{
    GameObject myMesh;
    CubeMesh cubeMeshScript;

    [SerializeField]
    GameObject xAxis, yAxis, zAxis, normal;

    bool selected = false;

    [SerializeField]
    Material xMat, yMat, zMat, selectedMat;

    // Start is called before the first frame update
    void Start()
    {
        myMesh = GameObject.FindGameObjectWithTag("CubeMesh");
        cubeMeshScript = myMesh.GetComponent<CubeMesh>();

        //Initialize axes to disabled
        ToggleAxes(false);
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.transform.gameObject == gameObject)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    //Tell the mesh that this is the selected vertex
                    cubeMeshScript.SetSelectedVert(gameObject);
                }
            }
        }

    }

    public void ToggleAxes(bool state)
    {
        xAxis.SetActive(state);
        yAxis.SetActive(state);
        zAxis.SetActive(state);
    }

    //public void DisplayNormal(Vector3 position, Vector3 direction)
    //{
    //    normal.transform.localPosition = direction;
    //    normal.transform.localRotation = Quaternion.FromToRotation(Vector3.up, direction);
    //}

    //public void ToggleNormal(bool state)
    //{
    //    MeshRenderer normalRenderer = normal.GetComponent<MeshRenderer>();
    //    normalRenderer.enabled = state;
    //}

}
