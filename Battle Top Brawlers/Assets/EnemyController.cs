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
    private GameObject player;
    public float speed = 25;

    void Start()
    {
        currentHealth = maxHealth;
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (transform.position.y < 1)
        {
            ScoreManager.scoreValue += 10;
            Die();
        }
    }

    private void FixedUpdate()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(2 * speed * lookDirection);
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

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamageFromE(enemyDamage);
        }
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
