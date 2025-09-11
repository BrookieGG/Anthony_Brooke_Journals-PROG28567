using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public List<Transform> asteroidTransforms;

    public float bombTrailSpacing = 10;
    public int numberOfTrailBombs = 5;
    //private float inBombSpacing = 0;
    //private int inNumberOfBombs;
    
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
}
