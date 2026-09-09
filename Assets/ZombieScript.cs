using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;


public class ZombieScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float HP = 10f;
    private Rigidbody2D rb;
    private Vector2 movement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
