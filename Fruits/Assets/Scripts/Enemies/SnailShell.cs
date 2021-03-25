using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailShell : EnemyController
{
    private bool isInvunerable = true;
    private bool canToDeath = false;
    private bool isDashing = false;
    private bool canFlip = true;

    private void Start()
    {
        StartCoroutine(InvunerableOff());
    }
    protected override void Update()
    {
        base.Update();

        if (isDashing)
        {
            body.velocity = new Vector2(speedMove, body.velocity.y);
        }
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") && canFlip)
        {
            Flip();
            StartCoroutine(CanFlip());
            canFlip = false;
        }

        if (collision.gameObject.CompareTag("Player") && Player.isAlive && !isInvunerable && !canToDeath)
        {
            isDashing = true;
            AudioManager.instance.Play("Damage");
            StartCoroutine(CanDeathing());

            if (transform.position.x < playerPos.position.x)
            {
                isFacingRight = false;
                speedMove *= -1;
            }
            else
            {
                isFacingRight = true;
                speedMove *= +1;
            }
        }

        if (collision.gameObject.CompareTag("Player") && Player.isAlive && !isInvunerable && canToDeath)
        {
            KnockBackPlayer();
        }

        if (collision.gameObject.CompareTag("HeadStomper") && Player.isAlive && !Player.isGrounded && !isInvunerable && canToDeath)
        {
            EnemyStomped();
        }
    }

    IEnumerator InvunerableOff()
    {
        yield return new WaitForSeconds(0.7f);
        isInvunerable = false;
    }

    IEnumerator CanDeathing()
    {
        yield return new WaitForSeconds(0.4f);
        canToDeath = true;
    }

    IEnumerator CanFlip()
    {
        anim.SetTrigger("wasHitedWall");
        AudioManager.instance.Play("Damage");
        yield return new WaitForSeconds(1f);
        canFlip = true;
    }
}
