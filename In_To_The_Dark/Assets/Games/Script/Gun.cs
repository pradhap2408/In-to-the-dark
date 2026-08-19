using UnityEngine;

public class Gun : MonoBehaviour
{
    public Camera playerCamera;
    public float damage = 100f;
    public float range = 100f;

    public LayerMask enemyLayer;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
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