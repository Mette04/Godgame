using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void Quit()
    {
        Debug.Log("Game is quitting...");

        // Quits the application (only works in a build, not the editor)
        Application.Quit();
    }
}
