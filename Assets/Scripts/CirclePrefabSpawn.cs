using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclePrefabSpawn : MonoBehaviour
{
    //  [SerializeField] Transform circlePrefab;
    [SerializeField] public GameObject circlePrefab;
    public float radius = 5.0f;
    public float speed = 1.25f;
    private float numberOfSpawns = 8.0f;

    private float nextSpawnTime = 0.2f;
    private float spawnTimer = 0;
    private float timeElapsed = 0;
    SpriteRenderer renderer;
    private DecimalConverter decConvert;
    Renderer rend;

    //BOSS SPAWN
    void Start()
    {
        decConvert = GameObject.Find("Decimals").GetComponent<DecimalConverter>();
        rend = GetComponent<Renderer>();
        rend.enabled = false;
        StartCoroutine(spawnSpawner());
       // WaitForSeconds(5.0f);
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            StartCoroutine(SpawnCircles());
            spawnTimer = nextSpawnTime;
        }


    }

    private IEnumerator spawnSpawner()
    {
        yield return new WaitForSeconds(45.0f);              //Render the object on screen at same time as the circle coroutine is triggered
        rend = GetComponent<Renderer>();
        rend.enabled = true;
    }
    private IEnumerator SpawnCircles()
    {
        //SPAWN AFTER 45 SECONDS
        yield return new WaitForSeconds(45.0f);
        float nextAngle = 2 * Mathf.PI / numberOfSpawns;                                        //Moves onto nex angle, divides the size of the circle by number of sections, 2*pr is the circumference
        float angle = 0;                                                                        //set current angle to 0
        for (int i = 0; i < numberOfSpawns; i++)                                                //Loop through each spawn
        {
            float x = Mathf.Cos(angle) * radius;                                                //Trig to find adjacent/hypotenuse of the angle by radius
            float y = Mathf.Sin(angle) * radius;                                                //O/H * radius to find Y component
            var obj = Instantiate(circlePrefab, transform.position, Quaternion.identity);       //Spawn the prefab
            var rb = obj.AddComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;                                            //add kinematic type of rb to give it velocity
            rb.velocity = new Vector2(x, y) * speed;                                            //fire from angle
            rb.mass = 0.0001f;                          //Low mass so doesnt move player on collision
            angle += nextAngle;                                                                 //Move onto next angle in loop
            //yield return new WaitForSeconds(1.0f);
            
            //Destroy(obj, 1.0f);


        }
    }
        //if (collision.otherCollider.)
        //If collide with player target/bboundary then set circlePrefab to inactive
        //Take away health, immobilize
    
}
