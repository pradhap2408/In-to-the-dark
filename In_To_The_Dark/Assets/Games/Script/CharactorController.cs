using UnityEngine;

public class CharactorController : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 15f;
    public float gravity = -9.81f;
    public Animator Gun;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");

        float v = Input.GetAxisRaw("Vertical");

        bool walking = h != 0 || v != 0;

        Gun.SetBool("Walk", walking);

        Vector3 move = transform.right * h + transform.forward * v;

        controller.Move(move.normalized * speed * Time.deltaTime);
        controller.Move(move * speed * Time.deltaTime);

    }
}
