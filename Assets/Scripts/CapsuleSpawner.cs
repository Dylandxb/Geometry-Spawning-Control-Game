using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;


public class CapsuleSpawner : MonoBehaviour
{
    [SerializeField] private int numCapsules;
    [SerializeField] Transform capsulePrefab;
    [SerializeField] private GameObject capsuleTarget;
    private GeometryPool pool;
    private FPS fpsScript;
    private Vector2 capsulePositions;

    void Start()
    {
        pool = FindObjectOfType<GeometryPool>();
        fpsScript = FindObjectOfType<FPS>();
        capsulePositions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(SpawnCapsules());

    }

    // TODO**************
    // Speed diamonds up as they close in on player
    //Spawn 4 at a time
    //Despawn on collide, call cam shake
    //despawn after 3 seconds
    //despawn stray ones
    //Spawn them stray and only begin chase after one seconds, use vec3 movetowards
    void Update()
    {
        //Calling it in Update causes FPS drop
    }
    private IEnumerator SpawnCapsules()
    {
        yield return new WaitForSeconds(2.0f);
        Stopwatch sw = new Stopwatch();
        sw.Start();
        for (int i = 0; i < numCapsules; i++)
        {
            Capsule p = null;
            if (pool == null)
            {
                p = Instantiate(capsulePrefab).gameObject.GetComponent<Capsule>();
            }
            else
            {
                GameObject newObj = pool.GetObjectCap();
                if (newObj != null)                             //Prevents null reference exception
                {
                    p = newObj.GetComponent<Capsule>();
                    p.transform.position = new Vector2(Random.Range(-capsulePositions.x, capsulePositions.x), Random.Range(-capsulePositions.y, capsulePositions.y));
                    fpsScript.DisplayFPS();
                    //If stopwatch.time is <=3 then spawn new objects otherwise enter else statement
                    //make a for loop to spawn new Obj every second
                    //check for elapsed ticks
                    // ONCE POOL IS SPAWNED, WAIT 3 SECONDS BEFORE RETURNING GAME OBJECTS TO POOL OFF SCENE
                }
                else
                {
                    yield return new WaitForSeconds(5.0f); //Try this with capsule, return each one after 5 seconds
                    newObj.SetActive(false);
                    pool.ReturnObject(newObj);
                }
            }
            if (p != null) //CHANGING TO TRANSFORM.POSITION SPAWNS INN CENTER OF SCREEN, diamondPositions//ADD OFFSET TO DIAMOND POSITIONS TO MAKE IT CLEANER
                p.GetComponent<Capsule>().Init(new Vector2(transform.position.x, transform.position.y), new Vector2(-2.0f + 4.0f * Random.value, -2.0f + 4.0f * Random.value));
                
            //p.GetComponent<Particle>().AttackPlayer(Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime));

        }
        sw.Stop();
        float timeMS = 1000.0f * (float)sw.ElapsedTicks / Stopwatch.Frequency;
        UnityEngine.Debug.Log("spawned " + numCapsules.ToString() + " in " + timeMS.ToString() + " milliseconds ");
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(SpawnCapsules());                //Call coroutine after 15 seconds

    }

}

