using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    public List<Transform> starTransforms;
    public float drawingTime;
    private int firstStar = 0;
    private int secondStar = 1;
    private float lineTime = 0f;

    // Update is called once per frame
    void Update()
    {
        DrawConstellation();
    }

    private void DrawConstellation()
    {
        Transform star1 = starTransforms[firstStar];
        Transform star2 = starTransforms[secondStar];

        lineTime += Time.deltaTime;

        float progress = Mathf.Clamp01(lineTime / drawingTime);
        Vector3 nextPoint = Vector3.Lerp(star1.position, star2.position, progress); //where we want it to reach

        Debug.DrawLine(star1.position, nextPoint);

        if (progress >= 1)
        {
            firstStar = secondStar;
            lineTime = 0f;
            secondStar = (secondStar + 1) % starTransforms.Count;
        }
    }

   
}
