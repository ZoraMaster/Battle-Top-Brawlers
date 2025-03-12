using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    PlayerController Player;
    public AudioSource clangSound;
    public int maxhealth = 100;
    public int currentHealth;
    public int playerDamage = 10;
    public HealthBar healthBar;

    [SerializeField] private Rigidbody playerBody;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float pushForce;
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

        if (transform.position.y < 700)
        {
            Destroy(Player);
            SceneManager.LoadScene("GameOver");
        }
    }

    void FixedUpdate()
    {
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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            PushPlayerAway(collision.contacts[0].point, pushForce);

            collision.gameObject.GetComponent<EnemyController>().TakeDamageFromP(playerDamage);
            clangSound.Play();
        }
    }
}
