using UnityEngine;

public class ClosestPoint : MonoBehaviour
{

    public Rigidbody2D enemyrb;
    public Transform enemy;

    private Vector2 previousPoint;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 closestPoint = enemyrb.ClosestPoint(transform.position); //gets the closest point on the enemy to the player's position

        if (closestPoint != previousPoint) //makes it so the console doesn't get filled as much with the debug log
        {
            float dist = Vector2.Distance(transform.position, closestPoint);

            Debug.Log($"Closest Point: {closestPoint} | Distance: {dist}");

            previousPoint = closestPoint;
        }
        
        
    }
}
