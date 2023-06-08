using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class DiamondSpawn : MonoBehaviour
{
    [SerializeField] private int numDiamonds;
    [SerializeField] Transform diamondPrefab;
    [SerializeField] private GameObject player;
    private GeometryPool pool;
    private FPS fpsScript;
    private Vector2 diamondPositions;

    void Start()
    {
        pool = FindObjectOfType<GeometryPool>();
        fpsScript = FindObjectOfType<FPS>();
        diamondPositions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(SpawnDiamonds());
        
    }

    //Spawn 5 at a time
    //Despawn on collide, call cam shake
    //despawn after 3 seconds
    //despawn stray ones
    //Spawn them stray and only begin chase after one seconds, use vec3 movetowards
    void Update()
    {
        //Calling it in Update causes FPS drop
    }
    private IEnumerator SpawnDiamonds()
    {
        yield return new WaitForSeconds(5.0f);
        Stopwatch sw = new Stopwatch();
        sw.Start();
        for (int i = 0; i < numDiamonds; i++)
        {
            Particle p = null;
            if (pool == null)
            {
                p = Instantiate(diamondPrefab).gameObject.GetComponent<Particle>();
            }
            else
            {
                GameObject newObj = pool.GetObject();
                if (newObj != null)                             //Prevents null reference exception
                {
                    p = newObj.GetComponent<Particle>();
                    p.transform.position = new Vector2(Random.Range(-diamondPositions.x, diamondPositions.x ), Random.Range(-diamondPositions.y , diamondPositions.y ));
                    fpsScript.DisplayFPS();
                  
                    // ONCE POOL IS SPAWNED, WAIT 3 SECONDS BEFORE RETURNING GAME OBJECTS TO POOL OFF SCENE
                }
                else
                {
                    //yield return new WaitForSeconds(3.0f);
                    //newObj.SetActive(false);
                    //pool.ReturnObject(newObj);
                    //IF TIME ELAPSED IS 3 SECONDS THEN SET NEWOBJ ACTIVE TO FALSE
                }


            }
            if (p != null) 
                p.GetComponent<Particle>().Init(new Vector2(transform.position.x, transform.position.y), new Vector2(-2.0f + 4.0f * Random.value, -2.0f + 4.0f * Random.value));
        }
        sw.Stop();
        float timeMS = 1000.0f * (float)sw.ElapsedTicks / Stopwatch.Frequency;
        UnityEngine.Debug.Log("spawned " + numDiamonds.ToString() + " in " + timeMS.ToString() + " milliseconds ");
        //gameObject.SetActive(false); Disables spawning of further diamonds in pool
        yield return new WaitForSeconds(10.0f);
        StartCoroutine(SpawnDiamonds());
        //Spawn particles here, call coroutine in update
        //Check if pool is already active then dont spawn next
    }

}

