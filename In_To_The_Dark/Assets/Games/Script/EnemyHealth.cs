using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 100f;
    private float currentHealth;

   /* [Header("Health Bar")]
    public GameObject healthBar;
    public Image healthFill;*/

    [Header("Hit")]
    public float hitAnimationTime = 0.767f;

    [Header("Death")]
    public float deathTime = 2f;

    private Animator animator;
    private EnemyFollow enemyFollow;

    private bool isDead = false;
    private bool isHit = false;

    private AudioSource audioSource;
    public AudioClip monster;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        currentHealth = maxHealth;

        animator = GetComponent<Animator>();
        enemyFollow = GetComponent<EnemyFollow>();
      

        // Health bar OFF
        /*  if (healthBar != null)
          {
              healthBar.SetActive(false);
          }

          UpdateHealthBar();*/
    }

    public void TakeDamage(float damage)
    {
        if (isDead)
            return;

        // Reduce health
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(
            currentHealth,
            0f,
            maxHealth
        );

        Debug.Log("Enemy Health: " + currentHealth);
      

        // Show health bar
       /* if (healthBar != null)
       {
            healthBar.SetActive(true);
        }

       UpdateHealthBar();*/

        // Hit animation
        if (damage > 0f && currentHealth > 0f)
        {
            PlayHit();
        }

        // Death
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void PlayHit()
    {
        if (isDead)
            return;

        if (isHit)
            return;

        isHit = true;

        // Stop movement temporarily
        if (enemyFollow != null)
        {
            enemyFollow.enabled = false;
        }

        // Play Hit animation
        if (animator != null)
        {
            animator.ResetTrigger("Hit");
            animator.SetTrigger("Hit");

            Debug.Log("HIT ANIMATION PLAY");
        }

        // Resume after hit animation
        Invoke(nameof(ResumeAfterHit), hitAnimationTime);
    }

    void ResumeAfterHit()
    {
        if (isDead)
            return;

        isHit = false;

        // Resume enemy movement
        if (enemyFollow != null)
        {
            enemyFollow.enabled = true;
        }
    }

    //void UpdateHealthBar()
   /* {
        if (healthFill != null)
        {
            healthFill.fillAmount =
                currentHealth / maxHealth;
        }
    }

  /*  public void ShowHealthBar(bool show)
    {
        if (healthBar != null && !isDead)
        {
            healthBar.SetActive(show);
        }
    }*/

    void Die()
    {
        if (isDead)
            return;

        isDead = true;
        if(isDead)
        {
            audioSource.Stop();
            animator.StopPlayback();
        }


        Debug.Log("ENEMY DEAD!");

        // Cancel hit resume
        CancelInvoke(nameof(ResumeAfterHit));

        // Hide health bar
       /* if (healthBar != null)
        {
            healthBar.SetActive(false);
        }*/

        // Stop enemy movement
        if (enemyFollow != null)
        {
            enemyFollow.enabled = false;
        }

        // Death animation
        if (animator != null)
        {
            animator.ResetTrigger("Hit");
            animator.SetTrigger("Dead");
        }

        // Destroy
       // Destroy(gameObject, deathTime);
    }
}