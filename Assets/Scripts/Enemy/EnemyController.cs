using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 5f;
    public float stoppingDistance = 30f;

    public float shootingInterval = 5f;

    private Transform player;
    private float shootingTimer = 0f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player == null)
        {
            Debug.LogError("Player not found. Make sure the player has the correct tag.");
        }
    }

    private void Update()
    {
        if (player == null)
            return;

        Vector3 direction = player.position - transform.position;

        if (direction.magnitude > stoppingDistance)
        {
            transform.position += direction.normalized * moveSpeed * Time.deltaTime;
        }

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        shootingTimer += Time.deltaTime;

        if (shootingTimer >= shootingInterval)
        {
            ShootMissile();
            shootingTimer = 0f;
        }
    }

    void ShootMissile()
    {
        GameObject missilePrefab = GameObject.FindGameObjectWithTag("Enemy Missile");

        if (missilePrefab != null)
        {
            GameObject missile = Instantiate(missilePrefab, transform.position, Quaternion.identity);
            missile.tag = "Enemy Missile"; // Just to ensure the tag is set correctly, though redundant in this case
            missile.GetComponent<HomingMissile>().SetTarget(player);
        }
        else
        {
            Debug.LogError("Missile prefab not found. Make sure the prefab has the correct tag.");
        }
    }
}
