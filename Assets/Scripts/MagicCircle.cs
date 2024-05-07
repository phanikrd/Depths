using UnityEngine;

public class MagicCircle : MonoBehaviour
{
    public GameObject shieldPrefab; // Reference to the shield prefab
    public float shieldDuration = 30f; // Duration of the shield in seconds

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the player
        if (other.CompareTag("Player"))
        {
            // Activate the shield on the player
            ActivateShield(other.gameObject);

            // Destroy the magic circle object after activating the shield
            Destroy(gameObject);
        }
    }

    void ActivateShield(GameObject player)
    {
        // Instantiate the shield prefab at the player's position
        GameObject shield = Instantiate(shieldPrefab, player.transform.position, Quaternion.identity);

        // Parent it to the player
        shield.transform.parent = player.transform;

        // Set its local position to (0, 0, 0) relative to the player
        shield.transform.localPosition = Vector3.zero;

        // Set the scale of the shield
        //shield.transform.localScale = new Vector3(3f, 3f, 3f);

        // Destroy the shield after the specified duration
        Destroy(shield, shieldDuration);
    }
}
