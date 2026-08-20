using UnityEngine;

public class CharactorController : MonoBehaviour
{
    public CharacterController controller;

    [Header("Movement")]
    public float speed = 15f;

    [Header("Gravity")]
    public float gravity = -9.81f;

    private float verticalVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Input
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // Movement direction
        Vector3 move = transform.right * h + transform.forward * v;

        // Prevent diagonal movement from being faster
        if (move.magnitude > 1f)
        {
            move.Normalize();
        }

        // Gravity
        if (controller.isGrounded)
        {
            if (verticalVelocity < 0)
            {
                verticalVelocity = -2f;
            }
        }

        verticalVelocity += gravity * Time.deltaTime;

        // Final movement
        Vector3 finalMove = move * speed;
        finalMove.y = verticalVelocity;

        controller.Move(finalMove * Time.deltaTime);
    }
}