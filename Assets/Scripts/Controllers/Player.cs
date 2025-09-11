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
}
