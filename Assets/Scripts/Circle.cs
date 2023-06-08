//using UnityEngine;

//[RequireComponent(typeof(SpriteRenderer))]
//public class Circle : MonoBehaviour
//{
//    public Vector3 velocity;
//    //public Vector3 gravity;
//    float time;
//    private SpriteRenderer spriteRenderer;
//    private GeometryPool pool;

//    [SerializeField] private GameObject playerTarget;
//    private float rotateSpeed = 2.0f;
//    private float speed = 3.5f;

//    private Rigidbody2D rb;
//    private PlayerMovement playerScript;
//    void Start()
//    {
//        time = 0.0f;
//        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
//        rb = GetComponent<Rigidbody2D>();
//        pool = FindObjectOfType<GeometryPool>();
//        playerTarget = GameObject.FindGameObjectWithTag("Player");
//        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
//        //LERP SPEED OF DIAMOND TO PLAYER AS IT IS IN A CERTAIN DISTANCE RANGE
//    }

//    void Update()
//    {
//        time += Time.deltaTime;

//        //transform.up uses local space in which the diamonds are facing "forward" Points y value towards target
//        if (playerScript.isInvis == false)                                                                                              //Checks condition of when player is visible in scene
//        {
//            transform.position = Vector2.MoveTowards(transform.position, playerTarget.transform.position, speed * Time.deltaTime);
//            transform.up = playerTarget.transform.position - transform.position;
//        }
//        else
//        {
//            transform.position = transform.position;                                                                                    //If player is invisible then set the new transform position to its current
//        }

//        //velocity = velocity * speed * Time.deltaTime;
//        // kill height 
//        if (transform.position.y < -10.0f)
//        {
//            if (pool == null)
//            {
//                Destroy(gameObject);

//            }
//            else
//            {
//                pool.ReturnObject(gameObject);          //Return object to pool
//            }

//        }
//    }
//    //Move PARTICLE IN ONE DIRECTION 
//    public void Init(Vector3 pos, Vector3 vel)
//    {
//        transform.position = pos;
//        velocity = vel;
//        gameObject.SetActive(true);
//    }

//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        if (collision.gameObject.CompareTag("Player"))
//        {
//            gameObject.SetActive(false);
//        }
//    }







//}