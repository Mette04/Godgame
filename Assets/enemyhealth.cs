using System.Collections;
using UnityEngine;

public class enemyhealth : MonoBehaviour
{
    public int maxHealth = 4;
    public int currentHealth;

    public float knockbackForce = 1f;
    private Rigidbody2D rb;

    public Sprite idleSprite;
    public Sprite hurtSprite;
    private SpriteRenderer spriteRenderer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log(gameObject.name + " took damage: " + amount + ", current health: " + currentHealth);

        if (spriteRenderer != null && hurtSprite != null)
        {
            spriteRenderer.sprite = hurtSprite;
            StartCoroutine(ResetSprite());
        }

        if (currentHealth <= 0)
        {
            Debug.Log(gameObject.name + " died.");
            EnemyManager.Instance.EnemyKilled(); // ← Add this line
            Destroy(gameObject);
        }
    }



    public void TakeDamage(int amount, Vector2 knockbackDirection)
    {
        TakeDamage(amount);

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(knockbackDirection.normalized * knockbackForce, ForceMode2D.Impulse);

            // <<-- THIS IS WHERE YOU ADD THE KNOCKBACK FLAG SETTING FROM POINT 3
            enemy enemyScript = GetComponent<enemy>(); // lowercase 'enemy' script
            if (enemyScript != null && !enemyScript.isKnockedBack)
            {
                enemyScript.isKnockedBack = true;
                StartCoroutine(ResetKnockback(enemyScript, 0.3f));
            }
        }
    }



    private IEnumerator ResetKnockback(enemy enemyScript, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            Debug.Log("Knockback ended, velocity reset.");
        }

        if (enemyScript != null)
        {
            enemyScript.isKnockedBack = false;
            Debug.Log("Knockback flag cleared.");
        }
    }

    private IEnumerator ResetSprite()
    {
        yield return new WaitForSeconds(0.2f); // Time to show the hurt sprite

        if (spriteRenderer != null && idleSprite != null)
        {
            spriteRenderer.sprite = idleSprite;
        }
    }
}
