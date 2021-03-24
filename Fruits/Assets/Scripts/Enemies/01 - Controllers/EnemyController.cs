using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Main Variables")]
    protected SpriteRenderer sprite;
    protected Transform playerPos;
    protected Rigidbody2D body;
    protected Animator anim;

    [Header("Movements")]
    protected bool isFliped;
    protected bool isMoving = false;
    public bool isFacingRight;

    protected bool canMove;

    [Header("Distance")]
    protected float totalDistance;
    protected float distanceX;
    protected float distanceY;

    [Header("Attack System")]
    public float distanceAttack;
    public float speedMove;

    [Header("Life System")]
    protected bool wasHited;
    private bool isStomped;
    public int health;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    } //Variables declaration

    protected virtual void Update()
    {
        playerPos = GameObject.Find("Player").GetComponent<Transform>();
    }
    //Movement
    protected void Flip()
    {
        isFacingRight = !isFacingRight;
        speedMove *= -1;

        if (isFacingRight)
        {
            transform.rotation = new Quaternion(transform.rotation.x, 180f, transform.rotation.z, transform.rotation.w);
        }
        else
        {
            transform.rotation = new Quaternion(transform.rotation.x, 0f, transform.rotation.z, transform.rotation.w);
        }

    } //Flip the sprite
    protected void Move()
    {
        DistanceCount();

        isMoving = (distanceX <= distanceAttack);

        if (isMoving && totalDistance >= 0.6f && Player.isAlive)
        {
            //Move
            body.velocity = new Vector2(-speedMove, body.velocity.y);
            anim.SetBool("isIdle", false);
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isIdle", true);
            anim.SetBool("isRunning", false);
        }
    } //Move the enemy

    //Distance Counts
    protected float PlayerDistance()
    {
        return Vector2.Distance(playerPos.position, transform.position);
    } //Attack player if him is close enough
    protected void DistanceCount()
    {
        distanceX = PlayerDistance();
        distanceY = playerPos.position.y - transform.position.y;
        totalDistance = distanceX - distanceY;

        totalDistance = Mathf.Abs(totalDistance);
    } //Calcula a distancia entre o inimigo e o player

    //Death system or something
    protected void KnockBackPlayer()
    {
        Player.isAlive = false;
        Player.knockBackCount = Player.knockBackLenght;

        if (playerPos.position.x < transform.position.x)
        {
            Player.knockFromRight = true;
        }
        else
        {
            Player.knockFromRight = false;
        }
    } //Knock the player
    protected void EnemyStomped()
    {
        //Knock up player
        AudioManager.instance.Play("Jump");
        Player.anim.SetTrigger("isDoubleJumping");
        Player.knockJump = true;

        //Enemy damage 
        wasHited = true;

        if (wasHited)
        {
            health--;
            EnemyLifeSystem();
            wasHited = false;
        }
    } //Enemys who was stomped
    protected void EnemyLifeSystem()
    {
        ShakeCam.canShakeCam = true;

        if (health <= 0)
        {
            anim.SetTrigger("wasHited");
            Destroy(gameObject, 0.1f);
        }
        else
        {
            anim.SetTrigger("wasHited");
        }
    } //Enemy's Life System

    //Triggers & Colliders
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Player.isAlive)
        {
            KnockBackPlayer();
        }

        if (collision.gameObject.CompareTag("HeadStomper") && Player.isAlive && !Player.isGrounded)
        {
            isStomped = true;

            if (isStomped)
            {
                EnemyStomped();
                isStomped = false;
            }
        }
    }
}
