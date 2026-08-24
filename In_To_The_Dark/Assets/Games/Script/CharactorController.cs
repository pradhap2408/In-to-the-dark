using UnityEngine;

public class CharactorController : MonoBehaviour
{
    public CharacterController controller;

    [Header("Movement")]
    public float walk = 5f;
    public float runSpeed = 10f;
    public float gravity = -9.81f;

    [Header("Gun Animator")]
    public Animator Gun;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip walkSound;
    public AudioClip runSound;

    [Header("Gun Rotation")]
    public GameObject rotationObject;
    public float gunTiltAngle = 3f;
    public float gunRotationSpeed = 8f;

    private float verticalVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
        }
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        bool moving = h != 0 || v != 0;
        bool running = moving && Input.GetKey(KeyCode.LeftShift);

        // =================================
        // SPEED
        // =================================

        float currentSpeed = running ? runSpeed : walk;

        // =================================
        // MOVEMENT
        // =================================

        Vector3 move =
            transform.right * h +
            transform.forward * v;

        if (move.magnitude > 1f)
        {
            move.Normalize();
        }

        // =================================
        // GRAVITY
        // =================================

        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        verticalVelocity += gravity * Time.deltaTime;

        move.y = verticalVelocity;

        controller.Move(
            move * currentSpeed * Time.deltaTime
        );

        // =================================
        // GUN ANIMATION
        // =================================

        if (Gun != null)
        {
            Gun.SetBool("Walk", moving);
        }

        // =================================
        // FOOTSTEP
        // =================================

        if (running)
        {
            PlaySound(runSound);
        }
        else if (moving)
        {
            PlaySound(walkSound);
        }
        else
        {
            if (audioSource != null)
            {
                audioSource.Stop();
            }
        }

        // =================================
        // GUN SIDE ROTATION
        // =================================

        RotateGun();
    }

    // =================================
    // ROTATE GUN
    // =================================

    void RotateGun()
    {
        if (rotationObject == null)
            return;

        float targetZ = 0f;

        // A = + value
        if (Input.GetKey(KeyCode.A))
        {
            targetZ = 4f;
        }

        // D = - value
        else if (Input.GetKey(KeyCode.D))
        {
            targetZ = -12f;
        }

        Quaternion targetRotation =
            Quaternion.Euler(0f, 0f, targetZ);

        rotationObject.transform.localRotation =
            Quaternion.Slerp(
                rotationObject.transform.localRotation,
                targetRotation,
                gunRotationSpeed * Time.deltaTime
            );
    }

    // =================================
    // SOUND
    // =================================

    void PlaySound(AudioClip clip)
    {
        if (audioSource == null || clip == null)
            return;

        if (audioSource.clip != clip)
        {
            audioSource.clip = clip;
            audioSource.loop = true;
            audioSource.Play();
        }
        else if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}