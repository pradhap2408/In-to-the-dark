using UnityEngine;

public class Gun : MonoBehaviour
{
    public Camera playerCamera;
    public float damage = 100f;
    public float range = 100f;
    public Animator animator;
    public LayerMask enemyLayer;
    public AudioClip shotClip;
    public AudioSource audioSource;


    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        if(Input.GetMouseButtonDown(0)&& audioSource != null && shotClip != null) {
            animator.SetTrigger("Shot");
            audioSource.PlayOneShot(shotClip);
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            animator.SetTrigger("Reload");
            
        } 
    }

    void Shoot()
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

            EnemyHealth enemy =hit.collider.GetComponentInParent<EnemyHealth>();

            if (enemy != null)
            {
                Debug.Log("ENEMY FOUND!");
                enemy.TakeDamage(damage);
            }

        }
    }
}