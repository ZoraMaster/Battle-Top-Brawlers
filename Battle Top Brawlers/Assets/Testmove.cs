using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Testmove : MonoBehaviour
{
    public InputAction moveInput1P;

    Vector3 moveDirection;

    [SerializeField] private Rigidbody playerBody;
    public float moveSpeed;
    public float pushForce;

    private void OnEnable()
    {
        moveInput1P.Enable();
    }

    private void OnDisable()
    {
        moveInput1P.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = moveInput1P.ReadValue<Vector3>();
    }

    void FixedUpdate()
    {
        playerBody.AddForce(moveDirection * moveSpeed);
    }
}
