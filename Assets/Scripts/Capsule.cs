using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Capsule : MonoBehaviour
{
    public Vector3 velocity;
    //public Vector3 gravity;
    float time;
    private SpriteRenderer spriteRenderer;
    private GeometryPool pool;

    [SerializeField] private GameObject capsuleTarget;
    private float rotateSpeed = 2.0f;
    private float speed = 10.0f;

    private Rigidbody2D rb;
    private PlayerMovement playerScript;
    void Start()
    {
        time = 0.0f;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        pool = FindObjectOfType<GeometryPool>();
        capsuleTarget = GameObject.FindGameObjectWithTag("CapsuleTarget");
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        //LERP SPEED OF DIAMOND TO PLAYER AS IT IS IN A CERTAIN DISTANCE RANGE
    }

    void Update()
    {
        time += Time.deltaTime;

        //transform.up uses local space in which the diamonds are facing "forward" Points y value towards target

        transform.position = Vector2.MoveTowards(transform.position, capsuleTarget.transform.position, speed * Time.deltaTime);
        transform.up = capsuleTarget.transform.position - transform.position;


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
        if (collision.gameObject.CompareTag("Boundary"))
        {
            gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Physics2D.IgnoreLayerCollision(11, 12, true);
        }
        if (collision.gameObject.CompareTag("HexPoint"))
        {
            Physics2D.IgnoreLayerCollision(11, 10, true);
        }
        if (collision.gameObject.CompareTag("Circle"))
        {
            Physics2D.IgnoreLayerCollision(11, 9, true);
        }
        if (collision.gameObject.CompareTag("Lazer1"))
        {
            Physics2D.IgnoreLayerCollision(11, 7, true);
        }
        if (collision.gameObject.CompareTag("Lazer2"))
        {
            Physics2D.IgnoreLayerCollision(11, 7, true);
        }
        if (collision.gameObject.CompareTag("Lazer3"))
        {
            Physics2D.IgnoreLayerCollision(11, 7, true);
        }
        if (collision.gameObject.CompareTag("Lazer4"))
        {
            Physics2D.IgnoreLayerCollision(11, 7, true);
        }
        if (collision.gameObject.CompareTag("Lazer5"))
        {
            Physics2D.IgnoreLayerCollision(11, 7, true);
        }
        if (collision.gameObject.CompareTag("Lazer6"))
        {
            Physics2D.IgnoreLayerCollision(11, 7, true);
        }
        if (collision.gameObject.CompareTag("Lazer7"))
        {
            Physics2D.IgnoreLayerCollision(11, 7, true);
        }
        if (collision.gameObject.CompareTag("Lazer8"))
        {
            Physics2D.IgnoreLayerCollision(11, 7, true);
        }
    }







}