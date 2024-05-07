using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MineSample : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Array of enemy prefabs
    public GameObject[] goodiesPrefabs; // Array of goodies prefabs
    public int hitsToDestroy = 10; // Number of hits required to destroy the mine
    public int enemiesToRelease = 1; // Number of enemies to release when hit
    public Image healthBarFill; // Reference to the health bar fill image
    public int goodiesToSpawn = 2; // Number of goodies to spawn after mine is destroyed
    public GameObject destructionEffectPrefab; // Prefab for destruction effect

    private int currentHits = 0; // Current number of hits received by the mine


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            
            // Check if the mine is destroyed
            if (currentHits >= hitsToDestroy)
            {
                DestroyMine();
            }
            else
            {
                // Release enemies
                ReleaseEnemies();
            }

            // Destroy the missile
            Destroy(other.gameObject);
        }
    }

    private void UpdateHealthBar()
    {
        // Calculate fill amount based on remaining hits
        float fillAmount = 1f - (float)currentHits / hitsToDestroy;

        // Update health bar fill amount
        healthBarFill.fillAmount = fillAmount;
    }

    private void ReleaseEnemies()
    {

        // Check if there are any alive enemies present
        GameObject[] existingEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (existingEnemies.Length > 0)
        {
            Debug.LogWarning("Cannot release enemies: Existing enemies are still alive.");
            return;
        }

        // Increase the hit count
        currentHits++;
        // Update health bar UI
        UpdateHealthBar();

        // Get the position of the mine sample
        Vector3 minePosition = transform.position;

        for (int i = 0; i < enemiesToRelease; i++)
        {
            // Randomly select an enemy prefab from the array
            GameObject selectedEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            Vector3 spawnPosition;
            bool positionValid = false;
            int attempts = 0;
            float minDistance = 5f; // Minimum distance between enemy spawns, adjust as needed

            do
            {
                // Randomly generate a spawn position near the mine sample
                spawnPosition = minePosition + new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
                // Check if the spawn position is too close to any existing enemy
                positionValid = IsPositionValid(spawnPosition, minDistance);
                attempts++;
            } while (!positionValid && attempts < 10); // Limit attempts to prevent infinite loop

            if (positionValid)
            {
                Instantiate(selectedEnemyPrefab, spawnPosition, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("Failed to find a valid spawn position for the enemy.");
            }
        }
    }

    // Check if the spawn position is too close to any existing enemy or the mine sample
    private bool IsPositionValid(Vector3 position, float minDistance)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            if (Vector3.Distance(position, enemy.transform.position) < minDistance)
            {
                return false; // Position is too close to an existing enemy
            }
        }

        // Check if the position is too close to the mine sample
        if (Vector3.Distance(position, transform.position) < minDistance)
        {
            return false; // Position is too close to the mine sample
        }

        return true; // Position is valid
    }

    private void DestroyMine()
    {
        // Instantiate destruction effect
        if (destructionEffectPrefab != null)
        {
            GameObject destructionEffect = Instantiate(destructionEffectPrefab, transform.position, Quaternion.identity);

            //Set the scale of the destruction effect
            destructionEffect.transform.localScale = new Vector3(10f, 10f, 10f);

            // Destroy the destruction effect after 2 seconds
            Destroy(destructionEffect, 2f);
        }

        // Destroy the mine object
        Destroy(gameObject);

        // Spawn goodies near the mine sample
        Vector3 minePosition = transform.position;
        for (int i = 0; i < goodiesToSpawn; i++)
        {
            GameObject selectedGoodiesPrefab = goodiesPrefabs[Random.Range(0, goodiesPrefabs.Length)];
            Vector3 spawnPosition = minePosition + new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f));
            Instantiate(selectedGoodiesPrefab, spawnPosition, Quaternion.identity);
        }
    }



}
