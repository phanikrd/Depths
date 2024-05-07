using UnityEngine;

public class FreezeCircle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the player
        if (other.CompareTag("Player"))
        {
            // Find all enemy missiles in the scene
            HomingMissile[] enemyMissiles = FindObjectsOfType<HomingMissile>();

            // Loop through each enemy missile and freeze them
            foreach (HomingMissile missile in enemyMissiles)
            {
                missile.FreezeMissile();
            }

            // Destroy the freeze circle object after freezing enemy missiles
            Destroy(gameObject);
        }
    }
}
