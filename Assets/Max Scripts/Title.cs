using UnityEngine;
using UnityEngine.SceneManagement;  // To load scenes

public class TitleScreenManager : MonoBehaviour
{
    // Method to be called when the Start button is pressed
    public void OnStartButtonPressed()
    {
        // Load the next scene (Make sure the next scene is added in Build Settings)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
