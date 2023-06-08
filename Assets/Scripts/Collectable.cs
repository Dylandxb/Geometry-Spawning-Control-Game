using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] CollectableData data;
    [SerializeField] GameEvent pickupEvent;
    public int stackSize;                       //How many of current item stored in inventory
    private Inventory invScript;

    private void Start()
    {
        invScript = FindObjectOfType<Inventory>();
    }
    private void OnValidate()
    {
        GetComponent<SpriteRenderer>().sprite = data.sprite;
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            pickupEvent.Raise();
            //Debug.Log("Game listener event raised (collectable sound");
            invScript.Add(data);                            //Gets a reference to the inventory script Add method, accepts argument of data of type CollectableData

        }
    }
    public Collectable(CollectableData collect)             //Constructor to pass in collectableData value
    {
        data = collect;                                     //Set CollectableData variable "data" to collect
        AddToStack();
    }

    public void AddToStack()
    {
        //Cap the inv to 3 items at a time, can only have 1 of each powerup at any given time
        stackSize++;
    }

    public void RemoveFromStack()
    {
        stackSize--;
    }
}

