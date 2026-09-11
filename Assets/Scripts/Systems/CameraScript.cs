using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public Transform player;
    public float smoothSpeed = 5f;

    void LateUpdate()
    {

        Vector3 targetPosition = new Vector3(
            player.position.x,
            player.position.y,
            transform.position.z
        );

        transform.position = new Vector3(
            player.position.x,
            player.position.y,
            transform.position.z
        );
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
