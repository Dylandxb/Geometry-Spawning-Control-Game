using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerControl : MonoBehaviour
{
    
    //GET REFERENCE TO PARENT OBJECT LAZERS AND FIND ALL ITS DIAG CHILDREN NTO DISABLE THEM IN A FUNCTION CALLED AFTER EACH COROUTINE. 
    //Lazer Data
    public Transform[] lazerWaypoints;
    private int currentWaypointIndex = 0;
    private float lazerMoveSpeed = 1.8f;
    private float waypointTime = 1.0f;
    private int numCyclesX = 0;
    private int numCyclesY = 0;
    private Coroutine previousXCoroutine;
    private Coroutine previousYCoroutine;
    //Lazer Game object parents
    private List<GameObject> lazers;
    private GameObject lazerY1;
    private GameObject lazerY2;
    private GameObject lazerX1;
    private GameObject lazerX2;
    public GameObject diagLazer;
    void Start()
    {
        //previousCoroutine = StartCoroutine(moveToWaypointX());   
        lazerY1 = GameObject.FindGameObjectWithTag("Lazer1");
        lazerY2 = GameObject.FindGameObjectWithTag("Lazer2");
        lazerX1 = GameObject.FindGameObjectWithTag("Lazer3");
        lazerX2 = GameObject.FindGameObjectWithTag("Lazer4");
        diagLazer =  this.gameObject.transform.GetChild(0).GetChild(0).gameObject; //Gets grandchild of parent objects to find the diagonal lazers
        XLazers();
        //YLazers();
    }

    void Update()
    {
       // XLazers();
    }

    private void XLazers()
    {
        previousXCoroutine = StartCoroutine(moveToWaypointX());
    }

    private void YLazers() //Causes increased speed and chaos at it stops the xLazer coroutine
    {
        previousYCoroutine = StartCoroutine(moveToWaypointY());
    }
    private IEnumerator moveToWaypointX()
    {
        //yield return new WaitForSeconds(5.0f);
        Transform wayPoint = lazerWaypoints[currentWaypointIndex];

        while (Vector2.Distance(transform.position,wayPoint.position) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, wayPoint.position, lazerMoveSpeed * Time.deltaTime);
            yield return null;
                
        }

        //Debug.Log("cylce X count " + numCyclesX); //Debugging purposes
        transform.position = wayPoint.position;
        numCyclesX++;
        yield return new WaitForSeconds(waypointTime);
        currentWaypointIndex = (currentWaypointIndex + 1) % lazerWaypoints.Length;
        //Debug.Log("cylce X count " + numCyclesX);

        StopCoroutine(previousXCoroutine);
        previousXCoroutine = StartCoroutine(moveToWaypointX());
        if (numCyclesX >= 5)
        {
            gameObject.SetActive(false);
            diagLazer.SetActive(false); 

        }
    }

    private IEnumerator moveToWaypointY()
    {
        Transform wayPoint = lazerWaypoints[currentWaypointIndex];

        while (Vector2.Distance(transform.position, wayPoint.position) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, wayPoint.position, lazerMoveSpeed * Time.deltaTime);
            yield return null;
            
        }

       // Debug.Log("cylce Y count " + numCyclesY); //Debugging purposes
        transform.position = wayPoint.position;
        numCyclesY++;
        yield return new WaitForSeconds(waypointTime);
        currentWaypointIndex = (currentWaypointIndex + 1) % lazerWaypoints.Length;
       // Debug.Log("cylce Y count " + numCyclesY);

        StopCoroutine(previousXCoroutine);
        previousYCoroutine = StartCoroutine(moveToWaypointY());
        if (numCyclesY >= 7)
        {
            gameObject.SetActive(false);
        }
    }
}
