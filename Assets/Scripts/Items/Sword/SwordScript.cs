using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwordScript : MonoBehaviour
{
    private Animator animator;
    private bool isAttacking;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            isAttacking = true;
            animator.SetTrigger("Attack");
        }
    }

    public void EndAttack()
    {
        isAttacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Danger"))
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