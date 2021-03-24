using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : EnemyController
{
    public GameObject shellObject;
    public Transform spawnShell;
    int canSpawnShell = 1;

    bool canFlip = true;
    protected override void Update()
    {
        base.Update();

        body.velocity = new Vector2(speedMove, body.velocity.y);
        anim.SetBool("isRunning", true);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") && canFlip)
        {
            Flip();
            StartCoroutine(CanFlip());

            canFlip = false;
        }

        if (collision.gameObject.CompareTag("Player") && Player.isAlive)
        {
            KnockBackPlayer();
        }

        if (collision.gameObject.CompareTag("HeadStomper") && Player.isAlive && !Player.isGrounded)
        {
            if(canSpawnShell == 1)
            {
                Instantiate(shellObject, spawnShell.position, spawnShell.rotation);
                canSpawnShell = 0;
            }

            EnemyStomped();
        }
    }
    IEnumerator CanFlip()
    {
        yield return new WaitForSeconds(1f);
        canFlip = true;
    }
}
