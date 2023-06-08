//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System.Diagnostics;
//using static UnityEditor.PlayerSettings;

//public class CircleSpawner : MonoBehaviour
//{
//    [SerializeField] private int numCircles;
//    [SerializeField] Transform circlePrefab;
//    [SerializeField] private GameObject player;
//    private GeometryPool pool;
//    private FPS fpsScript;
//    private Vector2 circlePositions;


//    void Start()
//    {
//        pool = FindObjectOfType<GeometryPool>();
//        fpsScript = FindObjectOfType<FPS>();
//        circlePositions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

//    }


//    private IEnumerator SpawnCircles()
//    {
//        yield return new WaitForSeconds(5.0f);
//        Stopwatch sw = new Stopwatch();
//        sw.Start();
//        for (int i = 0; i < numCircles; i++)
//        {
//            Circle p = null;
//            if (pool == null)
//            {

//                p = Instantiate(circlePrefab).gameObject.GetComponent<Circle>();

//            }
//            else
//            {
//                GameObject newObj = pool.GetObjectCircle();
//                if (newObj != null)                             //Prevents null reference exception
//                {

//                    p = newObj.GetComponent<Circle>();
//                    p.transform.position = new Vector2(Random.Range(-circlePositions.x, circlePositions.x), Random.Range(-circlePositions.y, circlePositions.y));
//                    fpsScript.DisplayFPS();

//                    // ONCE POOL IS SPAWNED, WAIT 3 SECONDS BEFORE RETURNING GAME OBJECTS TO POOL OFF SCENE
//                }
//                else
//                {
//                    //yield return new WaitForSeconds(3.0f);
//                    //newObj.SetActive(false);
//                    //pool.ReturnObject(newObj);
//                    //IF TIME ELAPSED IS 3 SECONDS THEN SET NEWOBJ ACTIVE TO FALSE
//                }


//            }
//            if (p != null)
//                p.GetComponent<Circle>().Init(new Vector2(transform.position.x, transform.position.y), new Vector2(-2.0f + 4.0f * Random.value, -2.0f + 4.0f * Random.value));
//        }
//        sw.Stop();
//        float timeMS = 1000.0f * (float)sw.ElapsedTicks / Stopwatch.Frequency;
//        UnityEngine.Debug.Log("spawned " + numCircles.ToString() + " in " + timeMS.ToString() + " milliseconds ");
//        //gameObject.SetActive(false); Disables spawning of further diamonds in pool
//        yield return new WaitForSeconds(10.0f);
//        StartCoroutine(SpawnCircles());
//        //Spawn particles here, call coroutine in update
//        //Check if pool is already active then dont spawn next
//    }

//}

