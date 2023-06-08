using System.Linq.Expressions;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class HexPoint : MonoBehaviour
{
    public Vector3 velocity;
    //public Vector3 gravity;
    float time;
    private SpriteRenderer spriteRenderer;
    private GeometryPool pool;
    private int counter = 0;

    [SerializeField] private GameObject hexPointTarget;
    //[SerializeField] private GameObject hexPointTarget3;
    //[SerializeField] private GameObject hexPointTarget4;
    //[SerializeField] private GameObject hexPointTarget5;
    //[SerializeField] private GameObject hexPointTarget6;
    //[SerializeField] private GameObject hexPointTarget7;
    //[SerializeField] private GameObject hexPointTarget8;
    //[SerializeField] private GameObject hexPointTarget9;
    //[SerializeField] private GameObject hexPointTarget10;
    //[SerializeField] private GameObject hexPointTarget11;
    //[SerializeField] private GameObject hexPointTarget12;
    //[SerializeField] private GameObject[] hexPointTargets;
    private float rotateSpeed = 2.0f;
    private float speed = 5.0f;
    private float spawnInterval = 0;
    private Rigidbody2D rb;
    private PlayerMovement playerScript;
    private bool isSpawned;
    void Start()
    {
        time = 0.0f;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        pool = FindObjectOfType<GeometryPool>();
        hexPointTarget = GameObject.FindGameObjectWithTag("Target");
        //hexPointTarget2 = GameObject.FindGameObjectWithTag("Target2");
        //hexPointTarget3 = GameObject.FindGameObjectWithTag("Target3");
        //hexPointTarget4 = GameObject.FindGameObjectWithTag("Target4");
        //hexPointTarget5 = GameObject.FindGameObjectWithTag("Target5");
        //hexPointTarget6 = GameObject.FindGameObjectWithTag("Target6");
        //hexPointTarget7 = GameObject.FindGameObjectWithTag("Target7");
        //hexPointTarget8 = GameObject.FindGameObjectWithTag("Target8");
        //hexPointTarget9 = GameObject.FindGameObjectWithTag("Target9");
        //hexPointTarget10 = GameObject.FindGameObjectWithTag("Target10");
        //hexPointTarget11 = GameObject.FindGameObjectWithTag("Target11");
        //hexPointTarget12 = GameObject.FindGameObjectWithTag("Target12");
        //hexPointTargets = GameObject.FindWithTag("Targets").transform;
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        //LERP SPEED OF DIAMOND TO PLAYER AS IT IS IN A CERTAIN DISTANCE RANGE
 
        //MAKE MULTIPLE INSTANCES OF PREFAB
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    string tag = collision.gameObject.tag;
    //    switch (tag)
    //    {
    //        case "Target1":
    //            transform.position = Vector2.MoveTowards(transform.position, hexPointTarget2.transform.position, speed * Time.deltaTime);
    //            transform.up = hexPointTarget2.transform.position - transform.position;
    //            break;
    //        case "Target2":
    //            transform.position = Vector2.MoveTowards(transform.position, hexPointTarget3.transform.position, speed * Time.deltaTime);
    //            transform.up = hexPointTarget3.transform.position - transform.position;
    //            break;
    //        case "Target3":
    //            transform.position = Vector2.MoveTowards(transform.position, hexPointTarget4.transform.position, speed * Time.deltaTime);
    //            transform.up = hexPointTarget4.transform.position - transform.position;
    //            break;

    //    }
    //}
    void Update()
    {
        
        time += Time.deltaTime;
        //spawnInterval += Time.deltaTime;
        //if (gameObject.activeSelf)
        //{
        //    isSpawned = true;
        transform.position = Vector2.MoveTowards(transform.position, hexPointTarget.transform.position, speed * Time.deltaTime);
        transform.up = hexPointTarget.transform.position - transform.position;
        //    isSpawned = false;
        //}
        //else if (isSpawned == false)
        //{
        //    return;
        //}
       // string tag = gameObject.tag;
        
        //if (spawnInterval >= 0 && spawnInterval <=3)
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, hexPointTarget1.transform.position, speed * Time.deltaTime);
        //    transform.up = hexPointTarget1.transform.position - transform.position;
        //}
        //if (spawnInterval >= 4 && spawnInterval <= 7)
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, hexPointTarget2.transform.position, speed * Time.deltaTime);
        //    transform.up = hexPointTarget2.transform.position - transform.position;
        //}
        //if (spawnInterval >= 8 && spawnInterval <= 11)
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, hexPointTarget3.transform.position, speed * Time.deltaTime);
        //    transform.up = hexPointTarget3.transform.position - transform.position;
        //}
        //if (spawnInterval >= 12 && spawnInterval <= 15)
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, hexPointTarget4.transform.position, speed * Time.deltaTime);
        //    transform.up = hexPointTarget4.transform.position - transform.position;
        //}
        //if (spawnInterval >= 16 && spawnInterval <= 19)
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, hexPointTarget5.transform.position, speed * Time.deltaTime);
        //    transform.up = hexPointTarget5.transform.position - transform.position;
        //}
        //if (spawnInterval >= 20 && spawnInterval <= 23)
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, hexPointTarget6.transform.position, speed * Time.deltaTime);
        //    transform.up = hexPointTarget6.transform.position - transform.position;
        //}
        //if (spawnInterval >= 24 && spawnInterval <= 27)
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, hexPointTarget7.transform.position, speed * Time.deltaTime);
        //    transform.up = hexPointTarget7.transform.position - transform.position;
        //}
        //if (spawnInterval >= 28 && spawnInterval <= 31)
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, hexPointTarget8.transform.position, speed * Time.deltaTime);
        //    transform.up = hexPointTarget8.transform.position - transform.position;
        //}
        //if (spawnInterval >= 32 && spawnInterval <= 35)
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, hexPointTarget9.transform.position, speed * Time.deltaTime);
        //    transform.up = hexPointTarget9.transform.position - transform.position;
        //}
        //if (spawnInterval >= 36 && spawnInterval <= 39)
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, hexPointTarget10.transform.position, speed * Time.deltaTime);
        //    transform.up = hexPointTarget10.transform.position - transform.position;
        //}
        //if (spawnInterval >= 40 && spawnInterval <= 43)
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, hexPointTarget10.transform.position, speed * Time.deltaTime);
        //    transform.up = hexPointTarget10.transform.position - transform.position;
        //}
        //if (spawnInterval >= 44 && spawnInterval <= 47)
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, hexPointTarget10.transform.position, speed * Time.deltaTime);
        //    transform.up = hexPointTarget10.transform.position - transform.position;
        //}
        //if (spawnInterval >= 48 && spawnInterval <= 51)
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, hexPointTarget11.transform.position, speed * Time.deltaTime);
        //    transform.up = hexPointTarget11.transform.position - transform.position;
        //}
        //if (spawnInterval >= 52 && spawnInterval <= 55)
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, hexPointTarget12.transform.position, speed * Time.deltaTime);
        //    transform.up = hexPointTarget12.transform.position - transform.position;
        //}
        //transform.up uses local space in which the diamonds are facing "forward" Points y value towards target
        //Loop through targets








        //velocity = velocity * speed * Time.deltaTime;
        // kill height 
        if (transform.position.y < -10.0f)
        {
            if (pool == null)
            {
                Destroy(gameObject);

            }
            else
            {
                pool.ReturnObject(gameObject);          //Return object to pool
            }

        }
    }
    //Move PARTICLE IN ONE DIRECTION 
    public void Init(Vector3 pos, Vector3 vel)
    {
        transform.position = pos;
        velocity = vel;
        gameObject.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Boundary"))
        {
            gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Physics2D.IgnoreLayerCollision(10, 12, true);
        }
        if (collision.gameObject.CompareTag("Circle"))
        {
            Physics2D.IgnoreLayerCollision(10, 9, true);
        }
        if (collision.gameObject.CompareTag("Capsule"))
        {
            Physics2D.IgnoreLayerCollision(10, 11, true);
        }
        if (collision.gameObject.CompareTag("Lazer1"))
        {
            Physics2D.IgnoreLayerCollision(10, 7, true);
        }
        if (collision.gameObject.CompareTag("Lazer2"))
        {
            Physics2D.IgnoreLayerCollision(10, 7, true);
        }
        if (collision.gameObject.CompareTag("Lazer3"))
        {
            Physics2D.IgnoreLayerCollision(10, 7, true);
        }
        if (collision.gameObject.CompareTag("Lazer4"))
        {
            Physics2D.IgnoreLayerCollision(10, 7, true);
        }
        if (collision.gameObject.CompareTag("Lazer5"))
        {
            Physics2D.IgnoreLayerCollision(10, 7, true);
        }
        if (collision.gameObject.CompareTag("Lazer6"))
        {
            Physics2D.IgnoreLayerCollision(10, 7, true);
        }
        if (collision.gameObject.CompareTag("Lazer7"))
        {
            Physics2D.IgnoreLayerCollision(10, 7, true);
        }
        if (collision.gameObject.CompareTag("Lazer8"))
        {
            Physics2D.IgnoreLayerCollision(10, 7, true);
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Target1"))
    //    {
    //        counter++;
    //    }
    //    else if (counter == 1)
    //    {

    //    }
    //}







}