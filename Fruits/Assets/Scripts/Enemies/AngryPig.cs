using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryPig : EnemyController
{
    [Header("Angry Pig variables")]
    //points at which he will turn
    private float rightLimit;
    private float leftLimit;

    private float enemyDistanceX;
    private float enemyDistanceY;

    //Patrol Variables
    public float howManySteps;
    private int canTurn = 1;

    //Speed movement
    public bool isRunning = false;
    private float increaseSpeed;
    private float boostSpeed;

    void Start()
    {
        increaseSpeed = speedMove;

        health = 2;

        rightLimit = transform.localPosition.x + howManySteps;
        leftLimit = transform.localPosition.x - howManySteps;

        enemyDistanceX = playerPos.localPosition.x - transform.localPosition.x;
        enemyDistanceY = playerPos.localPosition.y - transform.localPosition.y;
    }

    void FixedUpdate()
    {
        float distance = PlayerDistance();
        isRunning = distance <= distanceAttack;

        if (isRunning)
        {
            if(playerPos.localPosition.x > transform.position.x && !sprite.flipX || playerPos.position.x < transform.position.x && sprite.flipX && enemyDistanceX != 0)// && enemyDistanceX != 0 && enemyDistanceY >= 0
            {
                Flip();
            }

            speedMove = increaseSpeed + boostSpeed;
            anim.SetBool("isRunning", true);
        }
        else
        {
            speedMove = increaseSpeed;
            anim.SetBool("isRunning", false);

            if (transform.localPosition.x >= rightLimit && canTurn == 1)
            {
                FlipPig();
                canTurn = 0;
            }

            else if (transform.localPosition.x <= leftLimit && canTurn == 0)
            {
                FlipPig();
                canTurn = 1;
            }
        }

        body.velocity = new Vector2(speedMove, body.velocity.y);
        anim.SetFloat("speed", Mathf.Abs(speedMove));
    }

    void FlipPig()
    {
        sprite.flipX = !sprite.flipX;
        speedMove *= -1;
        increaseSpeed *= -1;
        boostSpeed *= -1;
    }
}
