using UnityEngine;
using UnityEngine.InputSystem;

public class GunScript : MonoBehaviour
{
    
    [Header("Player")]
    public Transform playerTransform;
    public Vector2 offset = new Vector2(0.3f, 0f);

    [Header("Gun")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public CharacterScript playerScript;

    private SpriteRenderer gunSprite;

    private Vector3 originalLocalPosition; //vi tri ban dau cua sung

    void Start()
    {
        gunSprite = GetComponent<SpriteRenderer>();

        originalLocalPosition = transform.localPosition;
    }
    void Update()
    {
        if (Mouse.current == null || Camera.main == null 
            || playerTransform == null || playerScript == null) return;

        // 1. Đặt vị trí súng theo player
        float dirX = playerScript.isFacingLeft ? -1f : 1f;

        transform.localPosition = new Vector3(
            Mathf.Abs(originalLocalPosition.x) * dirX,
            originalLocalPosition.y,
            originalLocalPosition.z
        );

        // 2. Lấy vị trí chuột trong world space
        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        mousePosition.z = 0f;

        // 3. Tính hướng + góc xoay
        Vector3 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        /////////////////////Ca doan nay bi loi

        // 4. ⭐ BÙ GÓC KHI CHĨA SANG TRÁI
        //    Nếu góc > 90° hoặc < -90° → súng đang chĩa sang trái
        //    → cộng 180° để lộn lại đúng hướng

        //if (angle > 90f || angle < -90f)
        //{
           //angle += 180f;
        //}

        /////////////////////////////////////////////////////

        // 5. Áp dụng góc xoay
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        gunSprite.flipY = direction.x < 0f;

        // 6. Bắn
        if (Mouse.current.leftButton.wasPressedThisFrame && playerScript.bulletCount > 0)
        {
            GameObject bullet = Instantiate(
                bulletPrefab,
                firePoint.position,
                Quaternion.identity
            );

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            // Đạn vẫn bay theo `direction` gốc (về phía chuột), KHÔNG dùng angle đã bù
            rb.linearVelocity = direction.normalized * bulletSpeed;

            playerScript.bulletCount--;
        }
    }
}