using UnityEngine;

public class Chest : MonoBehaviour
{
    public int health = 2; //mau cua ruong
    public GameObject[] itemsInside; //vat pham ben trong ruong
    RaycastHit hit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private bool isDestroyed = false;

    public void TakeDamage(int damage) // ruong bi nhan sat thuong
    {
        if (isDestroyed) return;
        health -= damage;
        if (health <= 0)
        {
            DestroyChest();
        }
    }
    void DestroyChest()
    {
        isDestroyed = true;
        foreach (var item in itemsInside) //drop trang bi
        {
            Instantiate(item, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerGunBullet")) // pha ruong bang sung
        {
            TakeDamage(1); //moi dan tru 1 mau
        }
        if (collision.gameObject.CompareTag("Sword"))
            TakeDamage(1);
    }
}