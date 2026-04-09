using System.Collections;
using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class EnemyAttack : MonoBehaviour
{
    public float detectionRadius = 5f;
    public float attackRange = 2f;
    public float attackRate = 1f;
    public int damage = 1;
    public LayerMask playerLayer;

    public Sprite idleSprite;
    public Sprite attackSprite;

    public float attackPauseTime = 0.5f;

    private float nextAttackTime = 0f;
    private SpriteRenderer spriteRenderer;

    private bool canMove = true;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Ensure we start with the idle sprite
        if (spriteRenderer != null && idleSprite != null)
        {
            spriteRenderer.sprite = idleSprite;
        }
    }

    public bool CanMove()
    {
        return canMove;
    }

    void Update()
    {

        if (!canMove)
        {
            GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            return;
        }

        Transform targetPlayer = GetClosestPlayer();

        if (targetPlayer != null)
        {
            float distance = Vector2.Distance(transform.position, targetPlayer.position);

            if (distance <= attackRange && Time.time >= nextAttackTime)
            {
                StartCoroutine(AttackRoutine(targetPlayer));
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    Transform GetClosestPlayer()
    {
        Collider2D[] playersInRange = Physics2D.OverlapCircleAll(transform.position, detectionRadius, playerLayer);
        Transform closestPlayer = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Collider2D playerCollider in playersInRange)
        {
            float distance = Vector2.Distance(transform.position, playerCollider.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closestPlayer = playerCollider.transform;
            }
        }

        return closestPlayer;
    }

    
    IEnumerator AttackRoutine(Transform targetPlayer)
    {

        canMove = false;

        // Switch to attack sprite
        if (spriteRenderer != null && attackSprite != null)
        {
            spriteRenderer.sprite = attackSprite;
        }

        // Deal damage
        Gods playerHealth = targetPlayer.GetComponent<Gods>();
        if (playerHealth != null)
        {
            Vector2 knockbackDir = (targetPlayer.position - transform.position).normalized;
            playerHealth.TakeDamage(damage, knockbackDir);
        }

        // Wait a short time, then return to idle
        yield return new WaitForSeconds(0.3f);

        if (spriteRenderer != null && idleSprite != null)
        {
            spriteRenderer.sprite = idleSprite;
        }

        canMove = true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

