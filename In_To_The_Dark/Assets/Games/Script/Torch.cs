using UnityEngine;

public class Torch : MonoBehaviour
{
    public Light torch;
    void Start()
    {
        torch.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            torch.enabled = !torch.enabled;
        }
    }
}