using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float lifetime = 3f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
            Destroy(gameObject);
    }
}