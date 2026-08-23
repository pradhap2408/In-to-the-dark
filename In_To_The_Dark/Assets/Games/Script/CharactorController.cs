using UnityEngine;

public class CharactorController : MonoBehaviour
{
    public CharacterController controller;

    public float walk = 5f;
    public float runSpeed = 10f;
    public float gravity = -9.81f;

    public Animator Gun;

    public AudioSource audioSource;
    public AudioClip walkSound;
    public AudioClip runSound;

    private float verticalVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        bool moving = h != 0 || v != 0;
        bool running = moving && Input.GetKey(KeyCode.LeftShift);

        // Speed
        float currentSpeed = running ? runSpeed : walk;

        // Movement
        Vector3 move = transform.right * h + transform.forward * v;

        if (move.magnitude > 1f)
            move.Normalize();

        // Gravity
        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        verticalVelocity += gravity * Time.deltaTime;

        move.y = verticalVelocity;

        controller.Move(move * currentSpeed * Time.deltaTime);

        // Gun Animator
        Gun.SetBool("Walk", moving);

        // Footstep sound
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
            audioSource.Stop();
        }
    }

    void PlaySound(AudioClip clip)
    {
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