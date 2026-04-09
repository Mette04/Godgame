using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    public int totalEnemies = 0;

    void Start()
    {
        totalEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    void Awake()
    {
        // Singleton pattern (so enemies can access this easily)
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void EnemyKilled()
    {
        totalEnemies--;
        Debug.Log("Enemy killed. Enemies left: " + totalEnemies);

        if (totalEnemies <= 0)
        {
            WinGame();
        }
    }

    void WinGame()
    {
        Debug.Log("YOU WIN!");
        SceneManager.LoadScene("WinScene"); // or activate a win UI
    }
}
