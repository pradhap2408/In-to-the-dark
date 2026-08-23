using UnityEngine;

public class Gun : MonoBehaviour
{
    public Camera playerCamera;
    public float damage = 100f;
    public float range = 100f;

    public Animator animator;
    public LayerMask enemyLayer;

    public AudioClip shotClip;
    public AudioClip reloadClip;
    public AudioSource audioSource;

    [Header("Ammo")]
    public int maxShots = 4;
    private int currentShots;

    private bool isReloading = false;
    public float reloadClipTime = 2f;

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        currentShots = maxShots;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isReloading)
            {
                Shoot();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!isReloading)
            {
                Reload();
            }
            
        }

        // Detect reload animation completion
        if (isReloading && animator != null)
        {
            AnimatorStateInfo state =
                animator.GetCurrentAnimatorStateInfo(0);

            if (state.IsName("Reload") && state.normalizedTime >= 1f)
            {
                FinishReload();
            }
        }
    }

    void Shoot()
    {
        if (currentShots <= 0)
        {
            Reload();
            return;
        }

        Debug.Log("SHOOT FUNCTION");

        if (animator != null)
        {
            animator.SetTrigger("Shot");
            Debug.Log("SHOT TRIGGER FIRED");
        }

        // Sound
        if (audioSource != null && shotClip != null)
        {
            audioSource.PlayOneShot(shotClip);
        }

        // Raycast
        if (playerCamera != null)
        {
            RaycastHit hit;

            if (Physics.Raycast(
                playerCamera.transform.position,
                playerCamera.transform.forward,
                out hit,
                range,
                enemyLayer))
            {
                Debug.Log("HIT: " + hit.collider.name);

                EnemyHealth enemy =
                    hit.collider.GetComponentInParent<EnemyHealth>();

                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
            }
        }

        currentShots--;

        Debug.Log("Shots Left: " + currentShots);

        if (currentShots <= 0)
        {
            Reload();
        }
    }

    void Reload()
    {
        if (isReloading)
            return;
        audioSource.PlayOneShot(reloadClip);
        isReloading = true;

        Debug.Log("RELOAD START");

        if (animator != null)
        {
            animator.ResetTrigger("Shot");
            animator.SetTrigger("Reload");
        }
    }

    void FinishReload()
    {
        currentShots = maxShots;
        isReloading = false;

        Debug.Log("RELOAD COMPLETE");
        Debug.Log("Shots: " + currentShots);

        if (animator != null)
        {
            animator.Play("Idle", 0, 0f);
        }
       
    }
}