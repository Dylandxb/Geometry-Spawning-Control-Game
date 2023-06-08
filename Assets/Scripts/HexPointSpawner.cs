using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class HexPointSpawner : MonoBehaviour
{
    [SerializeField] private int numHexPoints;
    [SerializeField] Transform HexPointPrefab;
    [SerializeField] private GameObject HexPointTarget;
    private GeometryPool pool;
    private FPS fpsScript;
    private Vector2 hexPointPositions;

    void Start()
    {
        pool = FindObjectOfType<GeometryPool>();
        fpsScript = FindObjectOfType<FPS>();
        hexPointPositions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(SpawnHexPoints());

    }

    void Update()
    {
       // StartCoroutine(SpawnHexPoints());
    }
    private IEnumerator SpawnHexPoints()
    {
        yield return new WaitForSeconds(0.4f);
        Stopwatch sw = new Stopwatch();
        sw.Start();
        for (int i = 0; i < numHexPoints; i++)
        {
            HexPoint p = null;
            if (pool == null)
            {
                p = Instantiate(HexPointPrefab).gameObject.GetComponent<HexPoint>();
            }
            else
            {
                GameObject newObj = pool.GetObjectHexPoint();
                if (newObj != null)                             //Prevents null reference exception
                {
                    p = newObj.GetComponent<HexPoint>();
                    p.transform.position = new Vector2(Random.Range(-hexPointPositions.x, hexPointPositions.x), Random.Range(-hexPointPositions.y, hexPointPositions.y));
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
                p.GetComponent<HexPoint>().Init(new Vector2(transform.position.x, transform.position.y), new Vector2(-2.0f + 4.0f * Random.value, -2.0f + 4.0f * Random.value));

            //p.GetComponent<Particle>().AttackPlayer(Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime));

        }
        sw.Stop();
        float timeMS = 1000.0f * (float)sw.ElapsedTicks / Stopwatch.Frequency;
        UnityEngine.Debug.Log("spawned " + numHexPoints.ToString() + " in " + timeMS.ToString() + " milliseconds ");
        yield return new WaitForSeconds(0.4f);
        StartCoroutine(SpawnHexPoints());                //Call coroutine after 15 seconds

    }

}

