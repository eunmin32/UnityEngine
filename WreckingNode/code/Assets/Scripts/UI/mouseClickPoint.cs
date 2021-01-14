using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseClickPoint : MonoBehaviour
{
    public keyboardControl keyboardControl;
    [SerializeField]
    int sceneNodeIndex;
    // Start is called before the first frame update
    private void OnMouseDown()
    {
        keyboardControl.setNode(sceneNodeIndex);
    }
}
