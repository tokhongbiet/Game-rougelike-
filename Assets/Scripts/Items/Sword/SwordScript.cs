using UnityEngine;
using UnityEngine.InputSystem;

public class SwordScript : MonoBehaviour
{
    private Animator animator;
    private Collider2D swordCollider; // Thêm tham chiếu đến Collider của kiếm
    private bool isAttacking;

    [Header("Attack Settings")]
    [SerializeField] private float attackCooldown = 1f;
    private float nextAttackTime = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        swordCollider = GetComponent<Collider2D>();

        // Tắt Collider khi bắt đầu (chưa chém thì không gây sát thương)
        if (swordCollider != null)
        {
            swordCollider.enabled = false;
        }
    }

    void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (Time.time >= nextAttackTime)
            {
                Attack();
            }
            else
            {
                Debug.Log("Dang cooldown, vui long cho!");
            }
        }
    }

    private void Attack()
    {
        isAttacking = true;
        nextAttackTime = Time.time + attackCooldown;

        // Bật Collider lên khi vung kiếm
        if (swordCollider != null)
        {
            swordCollider.enabled = true;
        }

        Debug.Log("Da chem!");
        animator.SetTrigger("Attack");
    }

    // Gọi hàm này bằng Animation Event ở CUỐI Animation chém
    public void EndAttack()
    {
        isAttacking = false;

        // Tắt Collider đi khi chém xong
        if (swordCollider != null)
        {
            swordCollider.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAttacking && collision.CompareTag("Danger"))
        {
            ZombieScript zombie = collision.GetComponent<ZombieScript>();

            if (zombie != null)
            {
                Debug.Log("Zombie bi chem");
                zombie.HP -= 1;
            }
        }
    }
}