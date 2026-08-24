using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;

    [Header("Range")]
    public float followRange = 15f;
    public float attackDistance = 3f;

    [Header("Movement")]
    public float speed = 3f;
    public float rotationSpeed = 8f;

    [Header("Animator")]
    public Animator animator;

  
    private bool isDead = false;
    private bool attacking = false;

    void Start()
    {
        

        if (animator == null)
            animator = GetComponent<Animator>();

        
    }

    void Update()
    {
        if (player == null || isDead)
            return;

        Vector3 direction = player.position - transform.position;
        direction.y = 0f;

        float distance = direction.magnitude;

        // OUTSIDE FOLLOW RANGE
        if (distance > followRange)
        {
            attacking = false;

            animator.SetBool("Walking", false);
            animator.SetBool("Attacking", false);

            return;
        }

        // =========================
        // ATTACK RANGE
        // =========================

        if (distance <= attackDistance)
        {
            // STOP
            animator.SetBool("Walking", false);

            // FACE PLAYER
            FacePlayer();

            // START ATTACK
            if (!attacking)
            {
                attacking = true;

                animator.SetBool("Attacking", true);

                // Force Attack state
                animator.Play("Attack", 0, 0f);

            
            }

            return;
        }

        // =========================
        // FOLLOW
        // =========================

        attacking = false;

        animator.SetBool("Attacking", false);
        animator.SetBool("Walking", true);

        // Rotate
        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        // Move only until attack distance
        float remainingDistance =
            distance - attackDistance;

        float moveAmount =
            Mathf.Min(
                speed * Time.deltaTime,
                remainingDistance
            );

        if (moveAmount > 0f)
        {
            transform.position +=
                direction.normalized * moveAmount;
        }
    }

    void FacePlayer()
    {
        Vector3 direction =
            player.position - transform.position;

        direction.y = 0f;

        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }



    public void Hit()
    {
        if (isDead)
            return;

        animator.SetTrigger("Hit");
    }

    public void Dead()
    {
        if (isDead)
            return;

        isDead = true;
        attacking = false;

        animator.SetBool("Walking", false);
        animator.SetBool("Attacking", false);
        animator.SetBool("Dead", true);
    }
}