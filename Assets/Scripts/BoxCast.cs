using UnityEngine;

public class BoxCast : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float distance = 0.2f;
    public bool inMelee = false;
    public GameObject player;

    //boxcastout.collider
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       RaycastHit2D boxcastout = Physics2D.BoxCast(transform.position, new Vector2(distance, distance),0f, Vector2.zero, LayerMask.GetMask("Default"));
       //Debug.Log(boxcastout.collider.name);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
        //RaycastHit2D boxcastout = Physics2D.BoxCast(transform.position, new Vector2(distance, distance), 0f, Vector2.zero, LayerMask.GetMask("Default"));
        //Debug.Log(boxcastout.collider.name);
    //}
}
