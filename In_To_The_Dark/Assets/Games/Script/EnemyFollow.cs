using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;

    [Header("Follow")]
    public float followRange = 15f;
    public float stopRange = 2f;
    public float speed = 3f;
    public float rotationSpeed = 8f;

    [Header("Animator")]
    public Animator animator;

    private bool Walking = false;
    private bool Attacking = false;

    void Update()
    {
        if (player == null)
            return;

        float distance = Vector3.Distance(
            transform.position,
            player.position
        );

        // FOLLOW
        if (distance <= followRange && distance > stopRange)
        {
            Vector3 direction = player.position - transform.position;
            direction.y = 0f;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation =
                    Quaternion.LookRotation(direction);

                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    targetRotation,
                    rotationSpeed * Time.deltaTime
                );

                transform.position +=
                    direction.normalized *
                    speed *
                    Time.deltaTime;

                Walking = true;
            }
        }
        else
        {
            Walking = false;
        }

        // ATTACK
        if (distance <= stopRange)
        {
            Attacking = true;
        }
        else
        {
            Attacking = false;
        }

        // Animator
        if (animator != null)
        {
            animator.SetBool("Walking", Walking);
            animator.SetBool("Attacking", Attacking);
        }
    }
}