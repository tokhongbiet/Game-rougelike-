using UnityEngine;
public class ZombieScript : MonoBehaviour
{
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
        if (collision.gameObject.CompareTag("Player"))
        {
            CharacterScript player = collision.gameObject.GetComponent<CharacterScript>();

            if (player.moveSpeed > 10)
            {
                HP -= 1;

                Debug.Log("Zombie health: " + HP);
            }
        }
    }
}