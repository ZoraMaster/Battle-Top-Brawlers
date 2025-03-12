using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public delegate void EnemyKilled();
    public static event EnemyKilled OnEnemyKilled;
    public int maxHealth = 50;
    public int currentHealth;
    public int enemyDamage = 10;
    private Rigidbody enemyRb; 
    private Transform playerTransform;
    public float movementSpeed;
    [SerializeField] private float pushForce;

    void Start()
    {
        currentHealth = maxHealth;
        enemyRb = GetComponent<Rigidbody>();
        playerTransform = GameObject.Find("Player").transform;
        pushForce = 500f;
        movementSpeed = 50f;
        enemyRb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    private void Update()
    {
        if (transform.position.y < 700)
        {
            ScoreManager.scoreValue += 10;
            Die();
        }
    }

    private void FixedUpdate()
    {
        Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
        directionToPlayer.y = 0; // Keep the enemy on the same plane as the player

        // Move the enemy towards the player
        transform.position += directionToPlayer * movementSpeed * Time.deltaTime;
    }

    public void TakeDamageFromP(int amount)
    {
        currentHealth -= amount; 
        if (currentHealth == 0)
        {
            ScoreManager.scoreValue += 10;
            Die();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PushEnemyAway(collision.contacts[0].point);
            collision.gameObject.GetComponent<PlayerController>().TakeDamageFromE(enemyDamage);
        }
    }

    private void PushEnemyAway(Vector3 collisionPoint)
    {
        // Calculate the direction to push the enemy
        Vector3 pushDirection = (transform.position - collisionPoint);

        // Apply force to the enemy
        enemyRb.AddForce(pushDirection.normalized * pushForce, ForceMode.Impulse);
    }

    public void Die()
    {

        if (OnEnemyKilled != null)
        {
            OnEnemyKilled();
            Destroy(this.gameObject);
        }
    }

}
