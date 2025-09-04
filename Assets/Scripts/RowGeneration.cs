using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RowGeneration : MonoBehaviour
{
    public Button generateButton;
    public TMP_InputField squareNumberInput;
    public float size = 1f;
    public float duration = 10f; //How long the lines stay visible
    // Start is called before the first frame update
    void Start()
    {
        generateButton.onClick.AddListener(GenerateSquare); //on click event
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateSquare()
    {
        int squares;
        if (string.IsNullOrEmpty(squareNumberInput.text) || !int.TryParse(squareNumberInput.text, out squares) || squares <= 0)
            //makes sure the input field is not empty or invalid (greater than 0)
        {
            Debug.Log("invalid"); //debug checker
            return;
        }
        for (int i = 0; i < squares; i++) //loops through the number of squares put in the input field
        {
            Vector3 start = new Vector3(i * (size + 0.1f), 0, 0); //calculate the position for each square and each new square is shifted to the right by 0.1
            DrawSquare(start, size, Color.white); //draw the square at that calculated location
        }
    }
    void DrawSquare(Vector3 center, float size, Color color) //drawing square from last task
    {
        float half = size / 2f;

        Vector3 tl = new Vector3(center.x - half, center.y + half, 0);
        Vector3 tr = new Vector3(center.x + half, center.y + half, 0);
        Vector3 bl = new Vector3(center.x - half, center.y - half, 0);
        Vector3 br = new Vector3(center.x + half, center.y - half, 0);

        Debug.DrawLine(tl, tr, color, duration);
        Debug.DrawLine(tr, br, color, duration);
        Debug.DrawLine(br, bl, color, duration);
        Debug.DrawLine(bl, tl, color, duration);
    }
}
