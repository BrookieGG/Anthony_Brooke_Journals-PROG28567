using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    public float arrivalDistance;
    public float maxFloatDistance;
    private Vector3 randomDist;

    // Start is called before the first frame update
    void Start()
    {
        float ang = Mathf.Deg2Rad * (Random.Range(0f, 90f));
        Vector3 direction = new Vector3(Mathf.Cos(ang), Mathf.Sin(ang), 0f);
        randomDist = transform.position + direction * maxFloatDistance;
        Debug.Log("arrival: " + arrivalDistance);
        Debug.Log("max: " + maxFloatDistance);
        Debug.Log("speed: " + moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        AsteroidMovement();

    }

    private void AsteroidMovement()
    {

        //Debug.Log("ang: " + ang);
        //transform.position = randomDist;
        Vector3 dir = (randomDist - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, randomDist);
        //Debug.Log("distance: " + (distance - arrivalDistance));


        //transform.position += randomDist * moveSpeed * Time.deltaTime;

        if (distance > arrivalDistance) 
        {
            transform.position += dir * moveSpeed * Time.deltaTime;
           

        }
        else
        {
            float ang = Mathf.Deg2Rad * (Random.Range(0f, 90f));
            Vector3 direction = new Vector3(Mathf.Cos(ang), Mathf.Sin(ang), 0f);
            Debug.Log("ang: " + ang);
            randomDist = transform.position + direction * maxFloatDistance;
        }
        
        
    }
}
