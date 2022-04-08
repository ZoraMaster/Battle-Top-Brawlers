using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr : MonoBehaviour
{
    float moveSpeed = 10f;
    float rotationSpeed = 10f;
    public float turnSpeed = 2f;

    float xAngle = 0f, yAngle = 5f, zAngle = 0f;

    // Update is called once per frame
    void Update()
    {
       transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        //transform.Rotate(xAngle, yAngle, xAngle, Space.Self);

        transform.Translate(Vector3.right * Time.deltaTime * turnSpeed);


    }
}
