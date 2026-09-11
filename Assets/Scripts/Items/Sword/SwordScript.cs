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
}