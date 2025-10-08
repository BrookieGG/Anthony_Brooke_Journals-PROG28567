using UnityEngine;

public class Missile : MonoBehaviour
{
    private Vector3 target;
    private float moveSpeed;

    private float destroyX = 22f;
    private float destroyY = 12f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += target * moveSpeed * Time.deltaTime; //move towards that direction in every frame

        if (Mathf.Abs(transform.position.x) > destroyX || Mathf.Abs(transform.position.y) > destroyY) //destroys missile when off screen
        {
            Destroy(gameObject);
        }
    }
    public void TargetPosition(Vector3 targetPosition, float speed)
    {
        moveSpeed = speed;

        target = (targetPosition - transform.position).normalized; //direction towards player's last position
    }
}
