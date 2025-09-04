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
    public float duration = 10f;
    // Start is called before the first frame update
    void Start()
    {
        generateButton.onClick.AddListener(GenerateSquare);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateSquare()
    {
        int squares;
        if (string.IsNullOrEmpty(squareNumberInput.text) || !int.TryParse(squareNumberInput.text, out squares) || squares <= 0)
        {
            Debug.Log("invalid");
            return;
        }
        for (int i = 0; i < squares; i++)
        {
            Vector3 start = new Vector3(i * (size + 0.1f), 0, 0);
            DrawSquare(start, size, Color.white);
        }
    }
    void DrawSquare(Vector3 center, float size, Color color)
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
