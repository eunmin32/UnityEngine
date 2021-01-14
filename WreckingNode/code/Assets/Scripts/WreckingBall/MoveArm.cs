using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveArm : MonoBehaviour
{
    [SerializeField]
    float damping = 0.1f;

    // Update is called once per frame
    void Update()
    {
        changePositionSecond();
    }

    private void changePosition()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float newX = transform.position.x + horizontal * damping;
        float newZ = transform.position.z + vertical * damping;

        transform.position = new Vector3(newX, transform.position.y, newZ);
    }

    private void changePositionSecond()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position = transform.position + new Vector3(-1, 0, 0) * Time.deltaTime * 10f;
        } else if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position = transform.position + new Vector3(1, 0, 0) * Time.deltaTime * 10f;
        } else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = transform.position + new Vector3(0, 0, 1) * Time.deltaTime * 10f;
        } else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = transform.position + new Vector3(0, 0, -1) * Time.deltaTime * 10f;
        }
    }
}
