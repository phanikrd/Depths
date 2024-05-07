using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 20f; // Amount of damage the missile deals
    public GameObject explosionPrefab; // Reference to the explosion prefab

    private Transform target;
    private bool isFrozen = false; // Flag to indicate if the missile is frozen
    private float freezeDuration = 10f; // Duration of freeze in seconds
    private float freezeTimer = 0f; // Timer to track freeze duration

    private bool hasShield = false; // Flag to indicate if the missile has encountered a shield
    private GameObject shieldObject; // Reference to the shield object

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void Update()
    {
        // If the missile is frozen, increment freeze timer and return
        if (isFrozen)
        {
            freezeTimer += Time.deltaTime;

            if (freezeTimer >= freezeDuration)
            {
                // Unfreeze the missile after freeze duration
                isFrozen = false;
                freezeTimer = 0f;
            }
            return;
        }

        // If there's no target, do nothing
        if (target == null)
            return;

        // Calculate the direction towards the target
        Vector3 direction = (target.position - transform.position).normalized;

        // Rotate towards the target
        transform.rotation = Quaternion.LookRotation(direction);

        // Move towards the target
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Check if the missile has encountered a shield
        if (hasShield && shieldObject != null)
        {
            // Destroy the missile
            Destroy(gameObject);

            // Show blast effect
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the missile hits the player
        if (other.CompareTag("Player"))
        {
            // Deal damage to the player submarine
            other.GetComponent<PlayerHealthManager>().TakeDamage(damage);

            // Show blast effect
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            // Play audio
            AudioSource audioSource = GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.Play();
            }

            // Destroy the missile
            Destroy(gameObject);
        }
        // Check if the missile hits the freeze circle
        else if (other.CompareTag("FreezeCircle"))
        {
            // Freeze the missile
            FreezeMissile();
        }
        // Check if the missile hits the magic circle
        else if (other.CompareTag("MagicCircle"))
        {
            // Activate shield for the missile
            ActivateShield();
        }
    }

    // Method to freeze the missile
    public void FreezeMissile()
    {
        isFrozen = true;
    }

    // Method to activate shield for the missile
    private void ActivateShield()
    {
        hasShield = true;

        // Set shield object reference if available
        shieldObject = GameObject.FindGameObjectWithTag("MagicCircle");
    }
}
