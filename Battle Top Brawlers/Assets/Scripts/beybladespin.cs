using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beybladespin : MonoBehaviour
{
    float moveSpeed = 10f;
    float xAngle = 0f, yAngle = 5f;

    private void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        transform.Rotate(xAngle, yAngle, xAngle, Space.Self);
    }

}
