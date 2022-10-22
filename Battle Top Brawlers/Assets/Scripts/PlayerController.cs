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

    [SerializeField]
    private Rigidbody playerBody;
    [SerializeField]
    private float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        clangSound = GetComponent<AudioSource>();
        currentHealth = maxhealth;
        healthBar.SetMaxHealth(maxhealth);
        playerBody = GetComponent<Rigidbody>();
        moveSpeed = 10f;
    }

    private void Update()
    {

        if (transform.position.y < 1)
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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyController>().TakeDamageFromP(playerDamage);
            clangSound.Play();
        }
    }
}
