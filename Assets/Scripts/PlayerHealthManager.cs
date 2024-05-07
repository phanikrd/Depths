using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour
{
    public float maxHealth = 250f; // Maximum health of the player
    public float currentHealth; // Current health of the player

    public Image healthBarFill; // Reference to the health bar fill image

    void Start()
    {
        // Initialize current health to maximum health at the start
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    // Method to reduce player's health
    public void TakeDamage(float damage)
    {
        // Reduce current health by the amount of damage
        currentHealth -= damage;

        // Ensure health doesn't go below zero
        currentHealth = Mathf.Max(currentHealth, 0f);

        // Update the health bar
        UpdateHealthBar();

        // Check if player's health has reached zero
        if (currentHealth <= 0)
        {
            // Perform any actions when player dies
            // For example, you can show a game over screen or respawn the player
            Debug.Log("Player has died!");
            SceneManager.LoadScene("RetryScene");
        }
    }

    // Method to restore player's health
    public void RestoreHealth(float amount)
    {
        // Increase current health by the specified amount
        currentHealth += amount;

        // Ensure health doesn't exceed maximum health
        currentHealth = Mathf.Min(currentHealth, maxHealth);

        // Update the health bar
        UpdateHealthBar();

        // Print player's current health to the console
        Debug.Log("Player's health restored to: " + currentHealth);
    }

    // Method to update the health bar UI
    void UpdateHealthBar()
    {
        // Calculate fill amount based on current health
        float fillAmount = currentHealth / maxHealth;

        // Update the fill amount of the health bar
        healthBarFill.fillAmount = fillAmount;

        // Print player's current health to the console
        Debug.Log("Player's current health: " + currentHealth);
    }
    private void Update()
    {
        // Check if all mine sample objects are present in the scene
        GameObject[] mines = GameObject.FindGameObjectsWithTag("MineSample");
        // If all mine sample objects are destroyed (not present in the scene)
        if (mines.Length == 0)
        {
            // Load the game win scene
            SceneManager.LoadScene("GameWinScene");
        }
    }
}
