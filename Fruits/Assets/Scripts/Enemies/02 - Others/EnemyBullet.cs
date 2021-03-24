using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public Transform spawnPiece2;
    private Transform playerPos;

    public GameObject piece1;
    public GameObject piece2;

    public float speed;
    private void Update()
    {
        playerPos = GameObject.Find("Player").GetComponent<Transform>();
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            KnockBackPlayer();
            BulletPieces();
            Destroy(gameObject);
        }

        BulletPieces();
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BulletPieces();
        Destroy(gameObject);
    }

    void BulletPieces()
    {
        int canSpawn = 1;

        if(canSpawn == 1)
        {
            GameObject cloneBulletPieces1 = Instantiate(piece1, transform.position, transform.rotation);
            GameObject cloneBulletPieces2 = Instantiate(piece2, spawnPiece2.position, transform.rotation);
            canSpawn = 0;
        }
    }
    void KnockBackPlayer()
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

    }
}
