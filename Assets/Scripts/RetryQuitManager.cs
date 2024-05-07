using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryQuitManager : MonoBehaviour
{
    // Method to handle retry functionality
    public void Retry()
    {
        // Replace "RetryScene" with the actual name of your retry scene
        SceneManager.LoadScene("Shop");
    }

    // Method to handle quit functionality
    public void Quit()
    {
        // Quit the application
        Application.Quit();
        Debug.Log("Application quit");
    }
}
