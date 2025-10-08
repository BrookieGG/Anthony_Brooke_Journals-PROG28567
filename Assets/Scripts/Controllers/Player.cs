using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public GameObject powerUp;
    public GameObject missile;
    public List<Transform> asteroidTransforms;

    public float bombTrailSpacing = 10;
    public int numberOfTrailBombs = 5;
    public float bombCorner = 3;

    public float warpDist = 0.5f;
    public float radarDist = 10f;

    [Space(15)]
    public float speed = 2f;
    public float maxSpeed = 7f;
    public float accelerationTime = 1f;
    public float decelerationTime = 1f;

    [Space(15)]
    public float enemySpeed = 5f;
    public float enemyMaxDist = 0.9f;
    public int sec = 0;

    private Vector3 velocity = Vector3.zero;

    public float spawnInterval = 8f;
    public float timer = 0f;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval) //calls method if the timer is the spawnInterval
        {
            MissileLock(7, 300);
            timer = 0f;
        }

        PlayerMovement();

        EnemyMovement();

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

        if( Input.GetKeyDown(KeyCode.R))
        {
            EnemyRadar(2, 8);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            SpawnPowerUps(2, 4);
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

    private void PlayerMovement()
    {
        float acceleration = maxSpeed / accelerationTime;
        float deceleration = maxSpeed / decelerationTime;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            velocity += acceleration * Time.deltaTime * Vector3.left;
            //transform.position += Vector3.left * speed;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            velocity += acceleration * Time.deltaTime * Vector3.right;
            //transform.position += Vector3.right * speed;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            velocity += acceleration * Time.deltaTime * Vector3.up;
            //transform.position += Vector3.up * speed;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            velocity += acceleration * Time.deltaTime * Vector3.down;
            //transform.position += Vector3.down * speed;
        }

        //if not moving / arrow keys aren't being pressed
        if (!Input.GetKey(KeyCode.LeftArrow) &&
           !Input.GetKey(KeyCode.RightArrow) &&
           !Input.GetKey(KeyCode.UpArrow) &&
           !Input.GetKey(KeyCode.DownArrow))
        {
            float reduceVelocity = velocity.magnitude - deceleration * Time.deltaTime;

            if (reduceVelocity < 0)
            {
                reduceVelocity = 0;
            }

            velocity = velocity.normalized * reduceVelocity;
        }

            velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
            transform.position += velocity * Time.deltaTime;
    }

    private void EnemyMovement()
    {
        Vector3 direction = (transform.position - enemyTransform.position).normalized;

        float distance = Vector3.Distance(transform.position, enemyTransform.position); //distance between enemy and player

        if (distance > enemyMaxDist) //move enemy unless its at the set distance
        {
            enemyTransform.position += direction * enemySpeed * Time.deltaTime;
        }

        //Debug.Log("Enemy: " + enemyTransform.position);
        //Debug.Log("Enemy Speed: " + enemySpeed);
        //Debug.Log("Enemy Dist: " + enemyMaxDist);
    }

    private void EnemyRadar(float radius, int circlePoints)
    {
        float angle = 360 / circlePoints;
        if (circlePoints < 3)
        {
            return;
        }
        Color radarCol = Color.green;
        
        float distance = Vector3.Distance(transform.position, enemyTransform.position);
        if (distance <= radius)
        {
            radarCol = Color.red;
        }

        Vector3 pointOne = new Vector3(0, 0, 0);
        Vector3 pointTwo = new Vector3(0, 0, 0);

        for (int i = 0; i < circlePoints; i++)
        {
            float ang = angle * i;
            float rad = Mathf.Deg2Rad * ang;
            Vector3 point = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0) * radius + transform.position;
            if (i == 0)
            {
                pointOne = point;
            }
            else
            {
                Debug.DrawLine(pointTwo, point, radarCol, 10);
            }
            pointTwo = point;
        }

        Debug.DrawLine(pointTwo, pointOne, radarCol, 10);
    }
    private void SpawnPowerUps(float radius, int numberOfPowerups)
    {
        float angle = 360f / numberOfPowerups;
        Vector3 pointOne = new Vector3(0, 0, 0);
     

        for (int i = 0; i < numberOfPowerups; i++)
        {
            float ang = angle * i;
            float rad = Mathf.Deg2Rad * ang;
            pointOne = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0) * radius + transform.position;
            Instantiate(powerUp, pointOne, Quaternion.identity);
        }

    }

    //Final Assignment Proposal 1
    private void MissileLock(float speed, int rate)
    {
      
        float maxY = 10;
        float minY = -10;
        float maxX = 19;
        float minX = -19;
        float Xchoice = Random.Range(0, 3);
        float Ychoice;
        float XPos = 0;
        float YPos = 0;

        if (Xchoice == 0)
        {
            Ychoice = Random.Range(1, 3);
            XPos = Random.Range(minX, maxX);
        }
        else
        {
            Ychoice = Random.Range(0, 3);
        }

        if (Xchoice == 1)
        {
            XPos = maxX;
        }
        if (Xchoice == 2)
        {
            XPos = minX;
        }

        if (Ychoice == 1)
        {
            YPos = maxY;
        }
        else if (Ychoice == 2)
        {
            YPos = minY;
        }
        else
        {
            YPos = Random.Range(minY, maxY);
        }

        Vector3 randomPos = new Vector3(XPos, YPos,0);
        Vector3 playerLastKnownPos = transform.position;

        GameObject newMissile = Instantiate(missile, randomPos, Quaternion.identity);
        newMissile.GetComponent<Missile>().TargetPosition(playerLastKnownPos, speed); //calls method in Missile script
       
          

    }

}

//private void ColouredAsteroids(List<Transform> asteroidTransforms, Color aColor)
//when a asteroid is detected by the radar using the same logic as the DetectAsteriods function it will colour those detected asteroids which ever colour the player has chosen
//this will help the player know which asteroids they have already detected

//private void CheckBombCount(int bombCount)
//i will adapt every bomb creating function to update a public bomb count variable that will be displayed using a TMP_Text UI in the game screen
//this will help the player know how many bombs they have created incase there is a limit and stops them from spawning more when they run out of bombs
