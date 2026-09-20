using UnityEngine;

// Script nay gan cho prefab "vat pham dan" bi zombie rot ra khi chet.
// Player di ngang qua se tu dong nhat dan.
public class AmmoDropScript : MonoBehaviour
{
    public int ammoAmount = 10; // so dan nhat duoc moi lan

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CharacterScript player = collision.gameObject.GetComponent<CharacterScript>();

            if (player != null)
            {
                player.AddAmmo(ammoAmount);
            }

            Destroy(gameObject);
        }
    }
}