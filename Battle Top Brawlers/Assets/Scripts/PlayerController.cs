using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public AudioSource clangSound;
    public int maxhealth = 100;
    public int currentHealth;
    public int playerDamage = 10;
    public HealthBar healthBar;

    [SerializeField] private Rigidbody playerBody; // Keep it private and assign in Start()
    [SerializeField] private float moveSpeed;
    [SerializeField] private float pushForce;

    void Start()
    {
        clangSound = GetComponent<AudioSource>();
        currentHealth = maxhealth;
        healthBar.SetMaxHealth(maxhealth);
        moveSpeed = 50f;
        pushForce = 100f;
        
        // Ensure Rigidbody is assigned
        playerBody = GetComponent<Rigidbody>();
        if (playerBody == null)
        {
            Debug.LogError("Rigidbody is missing from the Player GameObject!");
        }
    }

    private void Update()
    {
        if (transform.position.y < 700)
        {
            Destroy(gameObject); // Fix incorrect reference
            SceneManager.LoadScene("GameOver");
        }
    }

    void FixedUpdate()
    {
        if (playerBody == null) return; // Prevents crash if Rigidbody is missing

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        playerBody.AddForce(movement * moveSpeed);
    }

    public void TakeDamageFromE(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Destroy(gameObject); // Fix incorrect reference
            SceneManager.LoadScene("GameOver");
        }
    }

    public void PushPlayerAway(Vector3 collisionPoint, float pushForce)
    {
        if (playerBody == null) return; // Prevents crash if Rigidbody is missing
        
        Vector3 pushDirection = transform.position - collisionPoint;
        playerBody.AddForce(pushDirection.normalized * pushForce, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            PushPlayerAway(collision.contacts[0].point, pushForce);
            collision.gameObject.GetComponent<EnemyController>().TakeDamageFromP(playerDamage);
            clangSound.Play();
        }
    }
}
