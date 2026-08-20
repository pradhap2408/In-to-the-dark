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
    private Animator animator;
    private bool Dead = false;
    public float DeadTime = 4f;

    void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
            healthBar.SetActive(false);

        UpdateHealthBar();
    }

    public void ShowHealthBar()
    {
        if (healthBar = null)
            healthBar.SetActive(true);
    }

    public void HideHealthBar()
    {
       if(healthBar != null)
            healthBar.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        Debug.Log("Enemy Health: " + currentHealth);

        UpdateHealthBar();

        if (currentHealth <= 0)
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

    void Die()
    {
        Debug.Log("ENEMY DEAD!");

        // Health bar close
        if (healthBar != null)
            healthBar.SetActive(false);
        {
            animator = GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("Dead");
                Dead = true;
            }
            else
            {
                Debug.LogWarning("Animator component not found on the enemy.");
            }
        }
        if (Dead)
        {
           EnemyFollow enemyFollow = GetComponent<EnemyFollow>();
            if (enemyFollow != null)
            {
                enemyFollow.enabled = false;
            }
            Destroy(gameObject, DeadTime);
        }

        
    }
}