using UnityEngine;

public class SimpleEnemyMove : MonoBehaviour
{
    public float speed = 0.01f; // Slow speed
    public Vector2 moveDirection = Vector2.left; // Move LEFT

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    public bool isKnockedBack = false;

    void FixedUpdate()
    {
        if (isKnockedBack)
        {
            rb.linearVelocity = Vector2.zero;
            Debug.Log("Knocked back, no movement.");
            return;
        }

        Vector2 velocity = moveDirection.normalized * speed;
        rb.linearVelocity = velocity;

        Debug.Log($"Speed: {speed}, Velocity: {velocity}, Position: {rb.position}");
    }


}
