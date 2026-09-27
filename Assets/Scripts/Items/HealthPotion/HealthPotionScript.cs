using UnityEngine;

public class HealthPotionScript : MonoBehaviour
{

    public int healthAmount = 10; //so mau hoi
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CharacterScript player = collision.gameObject.GetComponent<CharacterScript>();

            if (player != null)
            {
                player.AddHealth(healthAmount);
            }

            Destroy(gameObject);
        }
    }
}
