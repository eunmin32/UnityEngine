using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoConnectJoint : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<CharacterJoint>().connectedBody = transform.parent.GetComponent<Rigidbody>();
    }
}
