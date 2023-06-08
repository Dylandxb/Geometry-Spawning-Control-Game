using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    //Rendering
    public ParticleSystem airDust;
    SpriteRenderer renderer;
    //Boundary
    private Vector2 screenBoundary;
    private float spriteWidth;
    private float spriteHeight;
    //Player Data
    private float horizontal;
    private float vertical;
    private float movementSpeed = 6.0f;
    private bool facingRight = true;
    private bool facingUp = true;
    private int hitCounter = 0;                                     
    public float playerHealth = 30.0f;
    public float totalTime = 100.0f;                                //Game time is 2 minutes
    //Speeding
    private bool canSpeed = true;
    public bool isSpeeding;
    private float speedIncrease = 8.0f;
    private float speedTime = 5.0f;
    private float speedCooldown = 20.0f;
    //Dashing
    private bool canDash = true;
    private bool isDash;
    private float dashSpeed = 20.0f;
    private float dashTime = 0.4f;
    private float dashCooldown = 2.0f;
    //Invisibility
    private bool canInvis = true;
    public bool isInvis;
    private float invisTime = 6.0f;
    private float invisCooldown = 20.0f;
    //Components
    [SerializeField] Rigidbody2D rb;
    [SerializeField] TrailRenderer trailRenderer;
    [SerializeField] public CollectableData Speed_Powerup;
    [SerializeField] public CollectableData Invisible_Powerup;
    public Camera mainCamera;
    //External Scripts
    private Inventory inventory;
    private DecimalConverter decConvert;
    private Animations animations;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        decConvert = GameObject.Find("Decimals").GetComponent<DecimalConverter>();
        animations = FindObjectOfType<Animations>();
        renderer = GetComponent<SpriteRenderer>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        screenBoundary = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));                //Gets screen size of world space, changes with aspect ratio
        spriteWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;                                                                   //Size of the player sprite with its min max values normalized from 0 to 1
        spriteHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        isSpeeding = false;
        isInvis = false;
    }
    void Update()
    {
        if (isDash)
        {
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");                    //Gets keyboard WASD inputs
        vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && canDash)                 //If key is pressed and canDash is true then enter coroutine
        {
            StartCoroutine(Dash());
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && inventory.HasSpeed(Speed_Powerup) && canSpeed && !isInvis)               //Check if player can speed is true, key press and item is stored in inventory
        {
            inventory.Remove(Speed_Powerup);                                                                    //Remove item from inv after key press
            StartCoroutine(IncreaseSpeed());                                                                    //Coroutine to increase speed for time t
        }


        if (Input.GetKeyDown(KeyCode.I) && inventory.HasInvis(Invisible_Powerup) && canInvis && !isSpeeding)        //If powerup already active in inventory then cant use same one after its just been used

        {
            inventory.Remove(Invisible_Powerup);
            StartCoroutine(Invisibility());
        }
        Flip();
        CreateAirDust();

        Vector3 viewPositionScreen = transform.position;                                                                                            //Get current position of object on screen
        viewPositionScreen.x = Mathf.Clamp(viewPositionScreen.x, screenBoundary.x * -1 + spriteWidth, screenBoundary.x - spriteWidth);              //Clamp x and y to the screenBoundaries - obj dimensions
        viewPositionScreen.y = Mathf.Clamp(viewPositionScreen.y, screenBoundary.y * -1 + spriteHeight, screenBoundary.y - spriteHeight);
        transform.position = viewPositionScreen;                                                                                                    //Reset the new transform position with its clamped values
        if (playerHealth <= 0.02)
        {
            gameObject.SetActive(false);                                                                                    //Disable player, trigger particle explosion and wait 3 seconds before restart
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);                                                     //Reload the game scene                                                                                                               
        }
        if (totalTime > 0)   //There is time Delay when dashing
        {
            totalTime -= Time.deltaTime;                                                                                        //Decrease by 1 real time second
            if (totalTime <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);                                                     //Reload the game scene                                                                                                                                                                                                                                                                                                                                                           
            }
        }
    }
        private void FixedUpdate()
    {
        if (isDash)
        {
            return;
        }
        rb.velocity = new Vector2(horizontal * movementSpeed, vertical * movementSpeed);        //Update velocity in x,y dir by a speed

    }


    private void Flip()             //Movement dir is right & up
    {   //Use local scale to define the direction object is facing
        CreateAirDust();
        if (facingRight && horizontal < 0f || !facingRight && horizontal > 0f)
        {
            Vector3 localScaleX = transform.localScale;
            facingRight = !facingRight;                                         //Flip the bool value to false
            localScaleX.x *= -1f;
            transform.localScale = localScaleX;                                 //Change the transforms localScale to oppisite value, face left
        }
        if (facingUp && vertical < 0f || !facingUp && vertical > 0f)
        {
            Vector3 localScaleY = transform.localScale;
            facingUp = !facingUp;
            localScaleY.y *= -1f;
            transform.localScale = localScaleY;
        }

    }

    private IEnumerator Dash()                  //Dash coroutine, Add audio to dash, cant dash through screen boundary add boxcolliders
    {
        canDash = false;
        isDash = true;
        //if (facingRight && !facingUp || !facingRight && facingUp)
        //{
        //    rb.velocity = new Vector2(transform.localScale.x * dashSpeed, 0);

        //}
        //else if (facingUp && facingRight || !facingUp && !facingRight)
        //{
        //    rb.velocity = new Vector2(transform.localScale.y * dashSpeed, 0);

        //}
        rb.velocity = new Vector2(transform.localScale.x * dashSpeed, 0);
        Physics2D.IgnoreLayerCollision(0, 7, true);
        airDust.Stop();                                                          //Dont Show particles when dashing
        trailRenderer.emitting = true;                                          //Emit the trail behind the dash
        yield return new WaitForSeconds(dashTime);                              //Wait for the dash to complete
        trailRenderer.emitting = false;                                         //Set emission to false
        isDash = false;                                                         //Cancel dashing until the cooldown is complete
        Physics2D.IgnoreLayerCollision(0, 7, false);
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
        //Make invulnerable to any collisions, ignore collision layers

    }
     
    void CreateAirDust()
    {
        airDust.Play();                         //Function to play the particle effect
    }
    private IEnumerator IncreaseSpeed()
    {
        //canInvis = false;
        canSpeed = false;
        isSpeeding = true;
        isInvis = false;
        canInvis = false; 
        dashSpeed += 8.0f;
        movementSpeed += speedIncrease; 
        renderer.color = new Color(0.0f, 1.0f, 0.0f, 1.0f);                                   //Change sprite and its particle trail to green while speeding
        trailRenderer.startColor = new Color(0.0f, 1.0f, 0.0f, 1.0f);
        trailRenderer.endColor = new Color(0.0f, 1.0f, 0.0f, 1.0f);
        airDust.startColor = new Color(0.0f, 1.0f, 0.0f, 1.0f);
        Debug.Log("Speed: " + isSpeeding);
        yield return new WaitForSeconds(speedTime);
        isSpeeding = false;
        canSpeed = false;
        canInvis = true;
        dashSpeed = 20.0f; 
        movementSpeed = Mathf.Lerp(movementSpeed, 6.0f, 2.0f);                                     //Lerp back to original speed
        Debug.Log("Speed: " + isSpeeding);
        renderer.color = decConvert.GetColorFromString("00FFF5");
        trailRenderer.startColor = decConvert.GetColorFromString("00FFF5");
        trailRenderer.endColor = decConvert.GetColorFromString("00FFF5"); 
        airDust.startColor = decConvert.GetColorFromString("1325C0");
        yield return new WaitForSeconds(speedCooldown);
    }
    public IEnumerator Invisibility()
    {
       //canSpeed = false;
        canInvis = false;
        isInvis = true;
        isSpeeding = false;
        Physics2D.IgnoreLayerCollision(0, 7, true);
        Physics2D.IgnoreLayerCollision(0, 8, true);
        Physics2D.IgnoreLayerCollision(0, 9, true);
        Physics2D.IgnoreLayerCollision(0, 10, true);
        Physics2D.IgnoreLayerCollision(0, 11, true);
        Physics2D.IgnoreLayerCollision(0, 12, true);
        renderer.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        float emissionRate = 5.0f;                                      //Decrease emission rate
        var emission = airDust.emission;
        emission.rateOverTime = emissionRate;                           //Make the particles emit less
        canDash = false;
        canSpeed = false;
        Debug.Log("Invis: " + isInvis);
        yield return new WaitForSeconds(invisTime);
        canInvis = false;
        isInvis = false;
        float baseEmissionRate = 100.0f;
        emission.rateOverTime = baseEmissionRate;
        Physics2D.IgnoreLayerCollision(0, 7, false);
        Physics2D.IgnoreLayerCollision(0, 8, false);
        Physics2D.IgnoreLayerCollision(0, 9, false);
        Physics2D.IgnoreLayerCollision(0, 10, false);
        Physics2D.IgnoreLayerCollision(0, 11, false);
        Physics2D.IgnoreLayerCollision(0, 12, false);
        renderer.color = decConvert.GetColorFromString("00FFF5");
        airDust.startColor = decConvert.GetColorFromString("1325C0");
        Debug.Log("Invis: " + isInvis);
        canDash = true;
        canSpeed = true;
        yield return new WaitForSeconds(invisCooldown);
    }

    private IEnumerator SlowSpeed()         //Slow speed coroutine for capsule effect
    {
        movementSpeed = Mathf.Lerp(movementSpeed, movementSpeed / 2, 1.5f);                                     //Lerp back to original speed/2
        yield return new WaitForSeconds(6.0f);
        movementSpeed = Mathf.Lerp(movementSpeed, 6.0f, 2.0f);                                     //Lerp back to original speed


    }

    private IEnumerator Immobilize()
    {
        canDash = false;
        canInvis = false;
        canSpeed = false;
        movementSpeed = 0;
        yield return new WaitForSeconds(0.25f);
        canDash = true;
        canInvis = true;
        canSpeed = true;
        movementSpeed = 6.0f;

    }

    private IEnumerator DisableDash() //FIX DISABLE DASH
    {
        canDash = false;                
        yield return new WaitForSeconds(3.0f);
        canDash = true;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boundary"))
        {
            canDash = false;
            hitCounter++;
            if (hitCounter >= 50)                                           //Limit collisions with outer walls to 20
            {
                gameObject.SetActive(false);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);                                                     //Reload the game scene                                                                                                               

            }
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            animations.CollCamShake();                                          //Trigger cam shake
            //Diamonds chase player
            hitCounter++;                                                       //Increment hit counter
            playerHealth = playerHealth - 0.5f;                                                     //Decrease player health by 0.5
        }

        if (collision.gameObject.CompareTag("Circle"))
        {
            //Miniature explosion, spawn smaller circles at quicker speeds
            hitCounter++;
            playerHealth = playerHealth - 0.25f;
            StartCoroutine(Immobilize());
        }

        if (collision.gameObject.CompareTag("Capsule"))
        {
            //Start coroutine to slow down player speed using lerp to 3.0f
            hitCounter++;                                                       //Increment hit counter
            playerHealth = playerHealth - 1.0f;                                                     //Decrease player health by 1
            StartCoroutine(SlowSpeed());
        }

        if (collision.gameObject.CompareTag("HexPoint"))
        {
            //Inflict damage then despawn hex while spinning
            hitCounter++;
            playerHealth = playerHealth - 0.75f;
            StartCoroutine(DisableDash());
        }

        if (collision.gameObject.CompareTag("HexJoined"))
        {
            //Fog screen/flash background/disable movement temporarily which acts as BOSS
        }
        //ELSE CHECK WHEN EACH TYPE OF COLLISION HAS HAPPENNED EG COLLIDED WITH HEXPOINT && CAPSULE THEN CALL BOTH COROUTINES
        //ELSE IF ALL HAVE HAPPENED AT ONCE IMMOBILIZE PLAYER COMPLETELY
        else if (collision.gameObject.CompareTag("HexPoint") && collision.gameObject.CompareTag("Capsule"))
        {
            hitCounter = hitCounter + 2;
            playerHealth = playerHealth - 1.75f;
            StartCoroutine(SlowSpeed());
            StartCoroutine(DisableDash());
        }


    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LazerBeam"))
        {
            mainCamera.backgroundColor = Color.black;
            playerHealth = Mathf.Lerp(playerHealth, 0.0f, 0.20f * Time.deltaTime);      //Lerp health from 10 to 0 over just over 10 seconds
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LazerBeam"))
        {
            mainCamera.backgroundColor = decConvert.GetColorFromString("060620");
        }
    }
}


