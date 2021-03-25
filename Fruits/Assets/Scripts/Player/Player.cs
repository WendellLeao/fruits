using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    [Header("Main Variables")]
    SpriteRenderer sprite;
    public static Rigidbody2D body;
    public static Animator anim;

    [Header("Player's Movement")]
    public float speedMove;
    private float move;

    public bool isFacingRight;

    [Header("Player's Jump")]
    public static bool isGrounded;
    private bool isDoubleJump;
    public static bool knockJump = false;
    public float forceJump;

    public Transform checkGround;
    public float radius = 0.2f;

    public LayerMask whatIsGround;

    [Header("Player's Wall Jump")]
    public Transform wallCheck;
    public LayerMask whatIsWall;

    private bool isTouchingWall;
    private bool isWallSliding;
    private bool canWallJumping = false;

    public float wallCheckDistance;
    public float wallSlideSpeed;

    [Header("Life System")]
    public static bool isInvulnerable = false;
    public static bool isAlive;
    public static bool isDead = false;

    //private int lifes = 3;

    [Header("Knocks")]
    public float knockBack;
    public static float knockBackLenght = 0.35f;
    public static float knockBackCount;
    public static bool knockFromRight;

    [Header("Others")]
    public ParticleSystem DustParticle;
    private bool canSpawnDust;
    private bool canPlayHitSound = true, canPlayExplosionSound = true;

    private void Awake()
    {
        instance = this;

        isDead = false;

        sprite = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        StartCoroutine(AppearTime());

        knockBackCount = 0;
        isAlive = true;
        //GameManager.isStart = true;

        //Flip to left
         if (sprite.flipX)
         {
             Flip();
             sprite.flipX = false;
         }

        if (GameManager.canFlipOnStart)
        {
           Flip();
        }

        //Ao iniciar troca o nome
        this.gameObject.name = "Player";
    }

    void Update()
    {
        //Inputs and Call Methods
        if(GameManager.isStart && isAlive)
        {
            //Call methods
            WallJump();

            //Jump Input
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded || Input.GetKeyDown(KeyCode.Space) && canWallJumping && !isGrounded
                || knockJump && !isGrounded) //First Jump
            {
                Jump();
                canWallJumping = false;
                knockJump = false;
            }

            else if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && isDoubleJump) //Double Jump
            {
                DoubleJump();
            }
        }

        //If the player dies
        if (!isAlive)
        {
            StartCoroutine(Death());
            anim.SetBool("isGrabbed", false);
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.isStart)
        {
            //If the plyar is alive
            if (isAlive)
            {
                //Call Walk Method
                Walk();

                //Check if the player is on the ground and check jump wall
                isGrounded = Physics2D.OverlapCircle(checkGround.position, radius, whatIsGround);
                isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsWall);

                if (!isGrounded)
                {
                    anim.SetBool("isFalling", true);
                }
                else
                {
                    anim.SetBool("isFalling", false);
                }
            } 
        }
        else
        {
            //If the player dies
            if (!isAlive)
            {
                if (knockBackCount > 0 && !isWallSliding)
                {
                    if (knockFromRight)
                    {
                        body.velocity = new Vector2(-knockBack, 1);

                        if (!isGrounded)
                        {
                            body.velocity = new Vector2(-knockBack, 3);
                        }
                    }
                    else
                    {
                        body.velocity = new Vector2(knockBack, 1);

                        if (!isGrounded)
                        {
                            body.velocity = new Vector2(knockBack, 3);
                        }
                    }

                    knockBackCount -= Time.deltaTime;
                }
                else
                {
                    body.velocity = new Vector2(0, 0);
                }
            }
        }
    }

    //Methods
    void Walk()
    {
        //Get the input
        move = Input.GetAxis("Horizontal");

        //Play the run animation
        anim.SetFloat("speed", Mathf.Abs(move));

        //Move the player
        if(knockBackCount <= 0 && !isWallSliding)
        {
            body.velocity = new Vector2(move * speedMove, body.velocity.y);
        }

        //Flip the sprite
        if(move > 0 && isFacingRight || move < 0 && !isFacingRight)
        {
            Flip();
        }
    }
    void Jump()
    {
        isDoubleJump = true;

        AudioManager.instance.Play("Jump");
        anim.SetTrigger("isJumping");
        body.velocity = new Vector2(body.velocity.x, 0f);
        body.AddForce(new Vector2(body.velocity.x, forceJump));
    }
    void DoubleJump()
    {
        if (isDoubleJump)
        {
            AudioManager.instance.Play("Jump");
            body.velocity = new Vector2(body.velocity.x, 0f);
            body.AddForce(new Vector2(body.velocity.x, forceJump));
            anim.SetTrigger("isDoubleJumping");

            isDoubleJump = false;
        }
    }
    void WallJump()
    {
        if (isTouchingWall && !isGrounded && body.velocity.y < 0)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }

        if (isWallSliding)
        {
            //isDoubleJump = true;
            canWallJumping = true;
            anim.SetBool("isGrabbed", true);

            if (body.velocity.y < -wallSlideSpeed)
            {
                body.velocity = new Vector2(body.velocity.x, -wallSlideSpeed);
            }
        }
        else
        {
            anim.SetBool("isGrabbed", false);
        }
    }
    void Flip()
    {
        if (!isWallSliding && isAlive)
        {
            //sprite.flipX = !sprite.flipX;
            isFacingRight = !isFacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;

            theScale.x *= -1;
            // rotate wallCheck checkWallDistence to nigative

            wallCheckDistance *= -1;
            transform.localScale = theScale;
        }
    }

    //Triggers
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.isStart)
        {
            //Traps Collision
            /*if (collision.gameObject.CompareTag("Trap_Spikes") || collision.gameObject.CompareTag("Trap_Fire") && !isInvulnerable)
            {
                anim.SetTrigger("wasHited");
                StartCoroutine(Death());
            }*/
        }
    }

    //IEnumerators
    IEnumerator Death()
    {
        ShakeCam.canShakeCam = true;
        move = 0f;

        if(canPlayHitSound)
        {
            AudioManager.instance.Play("Damage");
            canPlayHitSound = false;
        }
        anim.SetTrigger("wasHited");
        GameManager.isStart = false;
        //isAlive = false;

        yield return new WaitForSeconds(0.3f);    
        if(canPlayExplosionSound)
        {
            AudioManager.instance.Play("Explosion");
            canPlayExplosionSound = false;
        }
        anim.SetTrigger("Desappear");
        Destroy(gameObject,0.5f);
    }
    IEnumerator AppearTime()
    {
        AudioManager.instance.Play("Appear");
        yield return new WaitForSeconds(0.4f);
        
        if(TutorialMenu.instance != null)
        {
            if(TutorialMenu.instance.ShowedTutorial)
                GameManager.isStart = true;
        }
        else
        {
            GameManager.isStart = true;
        }
    }

    //Others
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(checkGround.position, radius); //Check Ground
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z)); //Check Wall
    }
}
