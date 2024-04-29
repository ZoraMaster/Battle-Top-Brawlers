using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


public class player1controller : MonoBehaviour
{
    player1controller Player;
    public AudioSource clangSound;
    public int maxhealth = 100;
    public int currentHealth;
    public int playerDamage = 10;
    public HealthBar healthBar;

    public InputAction moveInput1P;

    Vector3 moveDirection;

    [SerializeField] private Rigidbody playerBody;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float pushForce;

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
        clangSound = GetComponent<AudioSource>();
        currentHealth = maxhealth;
        healthBar.SetMaxHealth(maxhealth);
        playerBody = GetComponent<Rigidbody>();
        pushForce = 100f;
        moveSpeed = 50f;
    }

    private void Update()
    {
        moveDirection = moveInput1P.ReadValue<Vector3>();

        if (transform.position.y < 700)
        {
            Destroy(Player);
            SceneManager.LoadScene("GameOver");
        }
    }

    void FixedUpdate()
    {
        playerBody.AddForce(moveDirection * moveSpeed);
    }

    public void TakeDamageFrom2P(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Destroy(Player);
            SceneManager.LoadScene("GameOver");
        }
    }

    // Function to push the player away from the collision point
    public void PushPlayerAway(Vector3 collisionPoint, float pushForce)
    {
        // Calculate the direction to push the player
        Vector3 pushDirection = transform.position - collisionPoint;
        
        // Apply force to the player
        playerBody.AddForce(pushDirection.normalized * pushForce, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("2P"))
        {
            PushPlayerAway(collision.contacts[0].point, pushForce);

            collision.gameObject.GetComponent<player2controller>().TakeDamageFrom1P(playerDamage);
            clangSound.Play();
        }
    }
}
