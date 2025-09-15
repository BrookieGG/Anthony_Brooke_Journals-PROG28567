using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public List<Transform> asteroidTransforms;

    public float bombTrailSpacing = 10;
    public int numberOfTrailBombs = 5;
    public float bombCorner = 3;

    public float warpDist = 0.5f;
    public float radarDist = 10f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            SpawnBombAtOffset(new Vector3(0, 1));
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            SpawnBombTrail(bombTrailSpacing, numberOfTrailBombs);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            SpawnBombOnRandomCorner(bombCorner);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            WarpPlayer(enemyTransform, warpDist);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            DetectAsteroids(radarDist, asteroidTransforms);
        }
    }
    private void SpawnBombAtOffset(Vector3 inOffset) //Spawn Bomb at Offset
    {
        Vector3 spawnPosition = transform.position + inOffset;
        Instantiate(bombPrefab, spawnPosition, Quaternion.identity);
    }

    private void SpawnBombTrail(float inBombSpacing, int inNumberOfBombs)
    {
        for (int i = 1; i <= inNumberOfBombs; i++)
        {
            Vector3 spawnPos = transform.position - transform.up * (inBombSpacing * i);
            Instantiate(bombPrefab, spawnPos, Quaternion.identity);
        }
    }

    private void SpawnBombOnRandomCorner(float inDistance)
    {
        Vector3[] corners = new Vector3[]
        {
            (Vector3.up + Vector3.left).normalized, //corners
            (Vector3.up + Vector3.right).normalized,
            (Vector3.down + Vector3.left).normalized,
            (Vector3.down + Vector3.right).normalized
        };

        int random = Random.Range(0, corners.Length); //random corner in the list
        Vector3 direction = corners[random];

        Vector3 spawnPosition = transform.position + direction * inDistance; //calculate spawn position

        Instantiate(bombPrefab, spawnPosition, Quaternion.identity);

    }
    private void WarpPlayer(Transform target, float ratio)
    {
        if (ratio > 1)
        {
            ratio = 1;
            //Debug.Log("ratio = ratio");
        }
        if (ratio < 0)
        {
            ratio = 0;
        }
        Vector3 interpolatedPosition = Vector3.Lerp(transform.position, target.position, ratio);
        transform.position = interpolatedPosition;
    }

    private void DetectAsteroids(float inMaxRange, List<Transform> inAsteroids)
    {
        foreach(Transform asteroid in inAsteroids)
        {
            float dist = Vector3.Distance(asteroid.position, transform.position);
            if(dist <= inMaxRange)
            {
                //Debug.Log("in range");
                Color bluecolor = new Color(0f, 0f, 1f, 0.5f);
                Vector3 end = (asteroid.position - transform.position).normalized * 2.5f + transform.position;
                Debug.DrawLine(transform.position, end, bluecolor, 2f, false);
            }
        }
    }
}

//private void ColouredAsteroids(List<Transform> asteroidTransforms, Color aColor)
//when a asteroid is detected by the radar using the same logic as the DetectAsteriods function it will colour those detected asteroids which ever colour the player has chosen
//this will help the player know which asteroids they have already detected

//private void CheckBombCount(int bombCount)
//i will adapt every bomb creating function to update a public bomb count variable that will be displayed using a TMP_Text UI in the game screen
//this will help the player know how many bombs they have created incase there is a limit and stops them from spawning more when they run out of bombs
