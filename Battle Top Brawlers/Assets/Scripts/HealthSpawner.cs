using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSpawner : MonoBehaviour
{
    public Transform[] m_SpawnPoints;
    public GameObject m_HeatlhDropPrefab;
    public int m_MaxHealthDrops = 3;
    private int m_CurrentHealthDrops = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Spawn a health drop immediately
        SpawnHealthDrop();

        // Start the spawn timer
        StartCoroutine(SpawnHealthDropRoutine());
    }

    // Coroutine to spawn health drops every 10 seconds
    IEnumerator SpawnHealthDropRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);

            // Check if there are fewer than the maximum allowed health drops in the scene
            if (m_CurrentHealthDrops < m_MaxHealthDrops)
            {
                SpawnHealthDrop();
            }
        }
    }

    void SpawnHealthDrop()
    {
        if (m_SpawnPoints.Length == 0)
        {
            Debug.LogWarning("No spawn points assigned to HealthSpawner!");
            return;
        }

        // Choose a random spawn point
        int randomNumber = Random.Range(0, m_SpawnPoints.Length);

        // Instantiate the health drop prefab at the chosen spawn point
        Instantiate(m_HeatlhDropPrefab, m_SpawnPoints[randomNumber].position, Quaternion.identity);

        // Increment the current health drops count
        m_CurrentHealthDrops++;
    }

    // Function to be called when a health drop is picked up or destroyed
    public void DecreaseHealthDropsCount()
    {
        m_CurrentHealthDrops--;
    }
}
