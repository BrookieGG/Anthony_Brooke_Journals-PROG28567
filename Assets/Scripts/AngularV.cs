
using UnityEngine;

public class AngularV : MonoBehaviour
{
    public Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Angular Velocity
     rb = GetComponent<Rigidbody2D>();
     rb.angularVelocity = 5f;
     //Debug.Log(rb.angularVelocity);

        //Angular Damping
    rb.angularDamping = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
     Debug.Log(rb.angularVelocity);

    }
}
