using UnityEngine;

public class HealingCircle : MonoBehaviour
{
    public float healAmount = 100f; // Amount of health to restore
    public GameObject healingAnimationPrefab; // Reference to the healing animation prefab
    public float animationDuration = 2f; // Duration of the healing animation

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the player
        if (other.CompareTag("Player"))
        {
            // Access the PlayerHealthManager component of the player object
            PlayerHealthManager playerHealth = other.GetComponent<PlayerHealthManager>();

            // If the PlayerHealthManager component is found, restore the player's health
            if (playerHealth != null)
            {
                playerHealth.RestoreHealth(healAmount);
            }
            
            // Instantiate the healing animation object at position (0, 0, 0)
            GameObject healingAnimation = Instantiate(healingAnimationPrefab, other.gameObject.transform.position, Quaternion.identity);

            // Parent it to the player
            healingAnimation.transform.parent = other.gameObject.transform;

            // Set its local position to (0, 0, 0) relative to the player
            healingAnimation.transform.localPosition = Vector3.zero;

            // Destroy the healing animation object after the specified duration
            Destroy(healingAnimation, animationDuration);

            // Destroy the healing circle object after healing the player
            Destroy(gameObject);
        }
    }
}
