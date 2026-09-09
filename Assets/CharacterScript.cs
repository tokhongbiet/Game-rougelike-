using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class CharacterScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float HP = 10f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private bool canDash = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement = Vector2.zero;

        if (Keyboard.current.wKey.isPressed)
            movement.y += 1;

        if (Keyboard.current.sKey.isPressed)
            movement.y -= 1;

        if (Keyboard.current.aKey.isPressed)
            movement.x -= 1;

        if (Keyboard.current.dKey.isPressed)
            movement.x += 1;

        movement = movement.normalized;

        if (canDash == true)
        { if (Keyboard.current.shiftKey.wasPressedThisFrame)
            {
                StartCoroutine(Dash());
            }
        }

    }


    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
    IEnumerator Dash()
    {

        canDash = false;

        moveSpeed = 20f;

        yield return new WaitForSeconds(0.2f);

        moveSpeed = 5f;

        yield return new WaitForSeconds(2f);

        canDash = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Danger"))
        {
            HP -= 1;

            Debug.Log("Health: " + HP);
        }
    }
}