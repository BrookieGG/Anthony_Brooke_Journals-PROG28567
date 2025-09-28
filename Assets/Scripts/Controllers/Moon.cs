using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    public Transform planetTransform;
    private float angle = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OrbitalMotion(5,20,planetTransform);
    }
    private void OrbitalMotion(float radius, float speed, Transform target)
    {
        angle = angle + speed * Time.deltaTime;
        float ang = Mathf.Deg2Rad * angle;
        transform.position = new Vector3(Mathf.Cos(ang), Mathf.Sin(ang), 0) * radius + planetTransform.position;
        Debug.Log("angle: " + ang);
        

    }
}
