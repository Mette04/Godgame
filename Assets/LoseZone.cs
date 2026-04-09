using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger entered by: " + other.gameObject.name);
        if (other.CompareTag("Enemy"))
        {
            SceneManager.LoadScene("LoseScene");
        }
    }



}
