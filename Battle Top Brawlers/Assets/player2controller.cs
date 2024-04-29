using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class player2controller : MonoBehaviour
{
    player2controller Player;
    public AudioSource clangSound;
    public int maxhealth = 100;
    public int currentHealth;
    public int playerDamage = 10;
    public HealthBar healthBar;
    public InputAction moveInput2P;
    Vector3 moveDirection;

    [SerializeField] private Rigidbody playerBody;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float pushForce;

    private void OnEnable()
    {
        moveInput2P.Enable();
    }

    private void OnDisable()
    {
        moveInput2P.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        clangSound = GetComponent<AudioSource>();
        currentHealth = maxhealth;
        healthBar.SetMaxHealth(maxhealth);
        playerBody = GetComponent<Rigidbody>();
        pushForce = 40f;
        moveSpeed = 25f;
    }

    private void Update()
    {
        moveDirection = moveInput2P.ReadValue<Vector3>();

        if (transform.position.y < 700)
        {
            Destroy(Player);
            SceneManager.LoadScene("Player1wins");
        }
    }
    void FixedUpdate()
    {
        playerBody.AddForce(moveDirection * moveSpeed);
    }

    public void TakeDamageFrom1P(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Destroy(Player);
            SceneManager.LoadScene("Player1wins");
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
        if (collision.gameObject.CompareTag("1P"))
        {
            PushPlayerAway(collision.contacts[0].point, pushForce);

            collision.gameObject.GetComponent<player1controller>().TakeDamageFrom2P(playerDamage);
            clangSound.Play();
        }
    }
}
