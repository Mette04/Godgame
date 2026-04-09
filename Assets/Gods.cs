using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public class Gods : MonoBehaviour
{
    public UnityEvent Attack, Die, Hurt;
    public int Health;
    public int maxhealth = 3;
    public float coolDown;

    public GameObject attackPoint;
    public float radius;
    public LayerMask enemies;
    public float damage;

    public float knockbackForce = 5f; // How strong the knockback is

    [HideInInspector] public float coolDownLeft;

    private Rigidbody2D rb;

    private HashSet<enemyhealth> damagedEnemies = new HashSet<enemyhealth>();


    private void Start()
    {
        Health = maxhealth;
        rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        if(coolDownLeft>0)
        { coolDownLeft -= Time.deltaTime; }
    }
    public void GodAttack()
    {
        if (coolDownLeft > 0) return;

        Attack.Invoke();

        damagedEnemies.Clear();

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, enemies);

        foreach (Collider2D enemy in hitEnemies)
        {


            enemyhealth eh = enemy.GetComponent<enemyhealth>();
            if (eh != null && !damagedEnemies.Contains(eh))
            {
                damagedEnemies.Add(eh);
                Vector2 knockbackDir = (enemy.transform.position - transform.position).normalized;
                eh.TakeDamage((int)damage, knockbackDir);
                
            }
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            enemyhealth eh = collision.gameObject.GetComponent<enemyhealth>();
            if (eh != null)
            {
                Vector2 knockbackDir = (collision.transform.position - transform.position).normalized;
                eh.TakeDamage(1, knockbackDir);  // Use knockback version
            }
        }
    }



    public void TakeDamage(int amount)
    {
        Health-=amount;

        if (Health <= 0)
        {
            Die.Invoke();
        }
        else
        {
            Hurt.Invoke();
        }
    }
   

    public void TakeDamage(int amount, Vector2 knockbackDirection)
    {
        TakeDamage(amount); // Call the basic version above to handle damage

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero; // Stop any current movement (optional)
            rb.AddForce(knockbackDirection.normalized * knockbackForce, ForceMode2D.Impulse);
            StartCoroutine(StopKnockback());
        }
    }

    private IEnumerator StopKnockback()
    {
        yield return new WaitForSeconds(0.2f);
        rb. linearVelocity = Vector2.zero;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
    }
}
