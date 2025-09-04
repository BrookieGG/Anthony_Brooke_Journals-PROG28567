using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SqaureSpawner : MonoBehaviour
{

    private float s_size = 1f;
    private Vector3 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0;

            draw_square(mouseWorldPos, s_size, Color.white);
            
        }

        float s = Input.GetAxis("Mouse ScrollWheel");
        if (s != 0f)
        {
            s_size += s;
        }
    }

    void draw_square(Vector3 mp, float size, Color color)
    {
        float h = size / 2;
        Vector3 tl = new Vector3(mp.x - h, mp.y + h, mp.z);
        Vector3 tr = new Vector3(mp.x + h, mp.y + h, mp.z);
        Vector3 bl = new Vector3(mp.x - h, mp.y - h, mp.z);
        Vector3 br = new Vector3(mp.x + h, mp.y - h, mp.z);

        Debug.DrawLine(tl, tr, color, 10f);
        Debug.DrawLine(tr, br, color, 10f);
        Debug.DrawLine(br, bl, color, 10f);
        Debug.DrawLine(bl, tl, color, 10f);
    }
}


//Create a script called SquareSpawner.cs. When you click on the screen, it should draw a white square at the position you clicked on the screen using Debug.DrawLine.

//Draw a semi-transparent square at the position of the mouse at all times.

//When you scroll using the mouse wheel, it should increase/decrease the size of the semi-transparent square and any squares that you spawn into the scene.