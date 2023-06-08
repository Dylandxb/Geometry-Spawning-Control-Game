using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawn : MonoBehaviour
{
    public GameObject speedPrefab;
    public GameObject invisPrefab;

    private float speedRespawnTime = 10.0f;
    private float invisRespawnTime = 20.0f;

    //Could use a list of game obejcts to add prefabs. Add a max size on the list
    private Vector2 powerUpPositions;
    void Start()
    {
        powerUpPositions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(SpeedSpawn());
        StartCoroutine(InvisSpawn());

    }
    private void speedPos()
    {
        if ( GameObject.Find("speedPrefab") == null)
        {
            GameObject speed = Instantiate(speedPrefab) as GameObject;
            speed.transform.position = new Vector2(Random.Range(-powerUpPositions.x, powerUpPositions.x), Random.Range(-powerUpPositions.y, powerUpPositions.y));
        }

    }
    private void invisPos()
    {
        if (GameObject.Find("invisPrefab") == null)
        {
            GameObject invis = Instantiate(invisPrefab) as GameObject;
            invis.transform.position = new Vector2(Random.Range(-powerUpPositions.x, powerUpPositions.x) * 2, Random.Range(-powerUpPositions.y, powerUpPositions.y));
        }
 
    }

    private IEnumerator SpeedSpawn()
    {
        while(true)
        {
            yield return new WaitForSeconds(speedRespawnTime);
            speedPos();
        }
    }

    private IEnumerator InvisSpawn()
    {
        while(true)
        {
            yield return new WaitForSeconds(invisRespawnTime);
            invisPos();
        }
    }
}
