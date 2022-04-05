using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumping : MonoBehaviour
{
    private Rigidbody rb;
    public bool onG;
    float repeats;
    public float amount;
    public float rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        onG = true;
        repeats = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (repeats < amount)
        {
            if (onG)
            {
                rb.velocity = new Vector3(0, 1, 1);
                onG = false;
                repeats = +1;
            }
        }
        if (repeats == amount)
        {
            transform.Rotate(0, rotateSpeed, 0);
            repeats = 0;
        }
    }

    void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag("ground"))
        {
            onG = true;
        }
    }
}
