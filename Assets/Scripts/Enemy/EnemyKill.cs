using UnityEngine;

public class EnemyKill : MonoBehaviour
{
    public GameObject explosionPrefab; // Reference to the explosion prefab
    public AudioClip explosionSound; // Sound to play when the explosion occurs

    // Example method for handling missile collision
    void OnCollisionEnter(Collision collision)
    {
        // Check if the enemy submarine is hit by a missile
        if (collision.gameObject.CompareTag("Projectile"))
        {
            // Instantiate explosion effect
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            // Play the explosion sound
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);

            // Destroy the enemy submarine
            Destroy(gameObject);
        }
    }
}
