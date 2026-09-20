using UnityEngine;
using UnityEngine.InputSystem;

public class GunScript : MonoBehaviour
{

    public CharacterScript playerScript;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        mousePosition.z = 0f;

        Vector3 direction = mousePosition - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        if (Mouse.current.leftButton.wasPressedThisFrame && playerScript.bulletCount > 0)
        {
            GameObject bullet = Instantiate(
                bulletPrefab,
                firePoint.position,
                Quaternion.identity
            );

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.linearVelocity = direction.normalized * bulletSpeed;

            playerScript.bulletCount--;
        }
    }
}
