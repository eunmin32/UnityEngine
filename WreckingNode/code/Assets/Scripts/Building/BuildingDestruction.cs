using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDestruction : MonoBehaviour
{
    [SerializeField]
    GameObject shatteredBuilding;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Wrecking ball")
        {
            Crumble();
        }
    }

    void Crumble()
    {
        shatteredBuilding.SetActive(true);
        gameObject.SetActive(false);
    }
}
