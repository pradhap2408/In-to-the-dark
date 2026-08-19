using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;

    public float followRange = 15f;
    public float stopRange = 2f;
    public float speed = 3f;
    public float rotationSpeed = 8f;

    void Update()
    {
        if (player == null)
            return;

        float distance = Vector3.Distance(
            transform.position,
            player.position
        );

        // Player அருகில் இருந்தால் follow
        if (distance <= followRange && distance > stopRange)
        {
            Vector3 direction = player.position - transform.position;

            // Y movement வேண்டாம்
            direction.y = 0f;

            if (direction != Vector3.zero)
            {
                // Player-ஐ நோக்கி திரும்பு
                Quaternion targetRotation =
                    Quaternion.LookRotation(direction);

                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    targetRotation,
                    rotationSpeed * Time.deltaTime
                );

                // Player-ஐ follow செய்
                transform.position +=
                    direction.normalized *
                    speed *
                    Time.deltaTime;
            }
        }
    }
}