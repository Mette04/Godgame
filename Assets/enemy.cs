using UnityEngine;

public class enemy : MonoBehaviour
{
    public float speed = 0.01f;
    public Vector2 moveDirection = Vector2.left;

    private Rigidbody2D rb;
    private EnemyAttack attackScript;

    [HideInInspector]
    public bool isKnockedBack = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        attackScript = GetComponent<EnemyAttack>();
    }

    void FixedUpdate()
    {
        // Clamp speed to be between 0 and 1 every frame
        speed = Mathf.Clamp(speed, 0f, 1f);

        // Stop movement if knocked back
        if (isKnockedBack)
        {
            rb.linearVelocity = Vector2.zero;
            Debug.Log("Enemy is knocked back, skipping movement.");
            return;
        }

        // Stop movement if attacking or paused
        if (attackScript != null && !attackScript.CanMove())
        {
            rb.linearVelocity = Vector2.zero;
            Debug.Log("Enemy can't move (attacking or paused).");
            return;
        }

        // Normal movement
        Vector2 velocity = moveDirection.normalized * speed;
        rb.linearVelocity = velocity;

        Vector2 pos = rb.position;

        if (pos.y < -2.5f || pos.y > 1f)
        {
            pos.y = Mathf.Clamp(pos.y, -2.5f, 1f);
            rb.position = pos; // Set directly, not MovePosition
        }

        Debug.Log($"Speed: {speed} | Velocity: {velocity} | Position: {rb.position}");
    }
}
