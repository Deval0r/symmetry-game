using UnityEngine;

public class EndGame : MonoBehaviour
{
    // Method to end the game
    public void EndTheGame()
    {
        Debug.Log("Ending the game...");
        // Add any additional logic here (e.g., saving game state, showing a game over screen, etc.)

        // Exit the application
        Application.Quit();
        
        // Note: Application.Quit() does not work in the editor. To test in the editor, you can use:
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}