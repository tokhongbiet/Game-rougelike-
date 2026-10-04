using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System;
using System.Reflection;

public class CharacterScript : MonoBehaviour
{

    //Base Stats
    public float moveSpeed = 5f;
    public float HP = 10f;
    public float maxHP = 10;
    public int bulletCount = 10;

    //Effects
    public bool invulnerability = false;
    public event Action<float> OnHealthChanged;// de cap nhat mau qua ben UI
    public event Action<int> OnAmmoChanged; // de cap nhat so dan qua ben UI neu can

    //Huong xoay
    [Header("Facing")]
    public bool isFacingLeft = false;
    public bool faceMouse = true;
    public float facingDeadZone = 0.2f;

    [Header("Weapon")]
    public Transform weaponHolder;


    //Movements
    private Vector2 movement;
    private bool canDash = true;

    //Components
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    //time
    private float lastDamageTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        HandleMovement();

        if (faceMouse)
        {
            HandleMouseFacing();
        }

        if (canDash == true)
        {
            if (Keyboard.current.shiftKey.wasPressedThisFrame)
            {
                animator.SetTrigger("Dash");

                StartCoroutine(Dash());
            }
        }

        if (HP <= 0)
        {
            Die();
        }

    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    //di chuyen
    void HandleMovement()
    {
        movement = Vector2.zero;

        if (Keyboard.current.wKey.isPressed) movement.y += 1;
        if (Keyboard.current.sKey.isPressed) movement.y -= 1;
        if (Keyboard.current.aKey.isPressed) movement.x -= 1;
        if (Keyboard.current.dKey.isPressed) movement.x += 1;

        movement = movement.normalized;

        if (animator != null && animator.isInitialized)
        {
            animator.SetBool("isMoving", movement != Vector2.zero);
        }
    }

    //Quay mat theo chuot
    void HandleMouseFacing()
    {
        if (Mouse.current == null || Camera.main == null) return;

        Vector3 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = 0f;

        float diffX = mouseWorldPos.x - transform.position.x;

        if (diffX < -facingDeadZone)
        {
            SetFacing(true);
        }
        else if (diffX > facingDeadZone)
        {
            SetFacing(false);
        }
    }

    public void SetFacing(bool facingLeft)
    {
        isFacingLeft = facingLeft;
        ApplyFacing();
    }

    public void Flip()
    {
        SetFacing(!isFacingLeft);
    }

    void ApplyFacing()
    {
        spriteRenderer.flipX = isFacingLeft;
    }


    //
    IEnumerator Dash()
    {

        canDash = false;

        moveSpeed = 20f;

        yield return new WaitForSeconds(0.2f);

        moveSpeed = 5f;

        yield return new WaitForSeconds(2f);

        canDash = true;
    }

    //Gay dame
    private float damageInterval = 1f;

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Danger"))
        {
            if (Time.time - lastDamageTime >= damageInterval)
            {
                lastDamageTime = Time.time;
                HP -= 1;
            }

            animator.SetTrigger("Damaged");
            OnHealthChanged?.Invoke(HP);//goi cho UI de cap nhat mau
            Debug.Log("Health: " + HP);
        }
    }
    public void AddAmmo(int amount)
    {
        bulletCount += amount;
        OnAmmoChanged?.Invoke(bulletCount);
        Debug.Log("Ammo: " + bulletCount);
    }

    public void AddHealth(int amount)
    {
        HP += amount;

        if (HP > maxHP)
        {
            HP = maxHP;
        }

        OnHealthChanged?.Invoke(HP);
        Debug.Log("Health: " + HP);
    }
    void Die()
    {
        // Gọi GameManager hiển thị UI Game Over
        if (GameManager.Instance != null)
        {
            GameManager.Instance.GameOver();
        }

        // Tắt GameObject hoặc Disable Script để ngưng di chuyển/nhận Input
        gameObject.SetActive(false);
    }

}