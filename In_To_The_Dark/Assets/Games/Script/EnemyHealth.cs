using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("Health Bar")]
    public GameObject healthBar;
    public Image healthFill;

    [Header("Death")]
    public float deathTime = 2f;

    private Animator animator;
    private EnemyFollow enemyFollow;
    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;

        animator = GetComponent<Animator>();
        enemyFollow = GetComponent<EnemyFollow>();

        // Health bar OFF at start
        if (healthBar != null)
        {
            healthBar.SetActive(false);
        }

        UpdateHealthBar();
    }

    public void TakeDamage(float damage)
    {
        if (isDead)
            return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        Debug.Log("Enemy Health: " + currentHealth);

        // Show health bar when damaged
        if (healthBar != null)
        {
            healthBar.SetActive(true);
        }

        UpdateHealthBar();

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        if (healthFill != null)
        {
            healthFill.fillAmount = currentHealth / maxHealth;
        }
    }

    public void ShowHealthBar(bool show)
    {
        if (healthBar != null && !isDead)
        {
            healthBar.SetActive(show);
        }
    }

    void Die()
    {
        if (isDead)
            return;

        isDead = true;

        Debug.Log("ENEMY DEAD!");

        // Hide health bar
        if (healthBar != null)
        {
            healthBar.SetActive(false);
        }

        // Stop enemy movement
        if (enemyFollow != null)
        {
            enemyFollow.enabled = false;
        }

        // Death animation
        if (animator != null)
        {
            animator.SetTrigger("Dead");
        }

        // Destroy enemy after animation
        Destroy(gameObject, deathTime);
    }
}