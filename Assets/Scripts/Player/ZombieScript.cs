using UnityEngine;
public class ZombieScript : MonoBehaviour
{
    [Header("Drop khi chet")]
    public GameObject bulletDropPrefab;
    public float bulletDropChance = 0.3f; // 0.3 = 30%

    public float moveSpeed = 2f;
    public float HP = 10f;
    public Transform player;

    private Vector2 movement;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;

        animator.SetBool("isMoving", rb.linearVelocity != Vector2.zero);

        if (direction.x > 0)
            spriteRenderer.flipX = false;
        else if (direction.x < 0)
            spriteRenderer.flipX = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerGunBullet"))
        {
            HP -= 1;
            Debug.Log("Zombie bi ban");

            if (HP <= 0)
            {
                Die();
            }
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            CharacterScript player = collision.gameObject.GetComponent<CharacterScript>();

            if (player.moveSpeed > 10)
            {
                HP -= 1;

                Debug.Log("Zombie health: " + HP);
            }

            if (HP <= 0)
            {
                Die();
            }
        }
    }
    private void Die()
    {
        // Tung xac suat de quyet dinh co rot dan hay khong
        if (bulletDropPrefab != null && Random.value <= bulletDropChance)
        {
            Instantiate(bulletDropPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}