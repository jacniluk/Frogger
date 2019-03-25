using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Call actions for buttons
public class MainMenu : MonoBehaviour
{
    // Play the game
    public void PressPlayButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Cursor.visible = false;
    }

    // Exit game
    public void PressExitButton()
    {
        Application.Quit();
    }
}
