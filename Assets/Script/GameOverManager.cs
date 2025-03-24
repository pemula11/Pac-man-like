using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{

    private void Start()
    {
        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.None;
        // Show the cursor
        Cursor.visible = true;
    }
    public void Restart()
    {
        // Load the scene with the name "Gameplay"
        SceneManager.LoadScene("Gameplay");
    }

    public void MainMenu()
    {
        // Load the scene with the name "MainMenu"
        SceneManager.LoadScene("MainMenu");
    }
}
