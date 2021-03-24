using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trunk : EnemyFire
{
    [Header("WaitFlip")]
    protected bool isWaitingFlip = false;
    private int canFlipToPatrol;

    bool canFlip = true;
    protected override void Update()
    {
        base.Update();

        //Animations
        anim.SetBool("isRunning", canMove);
    }
    void FixedUpdate()
    {
        if (!isWaitingFlip && !isFiring)
        {
            body.velocity = new Vector2(speedMove, body.velocity.y);
            canMove = true;
        }
        else
        {
            canMove = false;
        }

        //Detect Player
        DistanceCount();

        if (distanceX <= distanceAttack && Mathf.Abs(distanceY) <= distanceAttack && Player.isAlive)
        {
            isFiring = true;
        }
        else
        {
            isFiring = false;
        }

        if (isFiring)
        {
            Fire();
            FlipToPlayer();
        }
        else
        {
            isFirstShot = true;
        }

        if (isWaitingFlip && isFiring)
        {
            anim.SetBool("isIdle", false);
            FlipToPlayer();
            canFlipToPatrol = 1;
        }

        else if(isWaitingFlip && !isFiring && canFlipToPatrol == 1)
        {
            FlipToPatrol();
            canFlipToPatrol = 0;
        }
    }

    void FlipToPlayer()
    {
        if (transform.position.x < playerPos.position.x && !isFacingRight || transform.position.x > playerPos.position.x && isFacingRight)
        {
            Flip();
        }
    }

    void FlipToPatrol()
    {
        if (transform.position.x < playerPos.position.x && isFacingRight || transform.position.x > playerPos.position.x && !isFacingRight)
        {
            Flip();
        }
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.gameObject.CompareTag("Wall"))
        {
            anim.SetBool("isIdle", true);

            if (canFlip)
            {
                StartCoroutine(WaitFlip());
                canFlip = false;
            }
        }
    }

    IEnumerator WaitFlip()
    {
        isWaitingFlip = true;
        yield return new WaitForSeconds(2f);
        isWaitingFlip = false;
        anim.SetBool("isIdle", false);

        Flip();

        yield return new WaitForSeconds(2.5f);
        canFlip = true;
    }
}
