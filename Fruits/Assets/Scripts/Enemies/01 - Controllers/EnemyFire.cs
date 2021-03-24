using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : EnemyController
{
    [Header("Fire Variables")]
    protected bool isFirstShot;
    protected bool isFiring = false;
    protected float timer = 0f;
    public float fireRate;

    public float timeWaitShot;

    [Header("Bullet Variables")]
    public GameObject enemyBullet;
    public Transform spawnBullet;

    protected void Fire()
    {
        if (isFirstShot)
        {
            timer = fireRate;
            isFirstShot = false;
        }
        else
        {
            timer += Time.deltaTime;

            if (timer >= fireRate)
            {
                StartCoroutine(WaitShot());
                timer = 0;
            }
        }
    } //Para inimigos que atiram projéteis e precisam de fireRate

    protected IEnumerator WaitShot()
    {
        anim.SetTrigger("fireEnemy");
        yield return new WaitForSeconds(timeWaitShot);
        GameObject cloneBullet = Instantiate(enemyBullet, spawnBullet.position, spawnBullet.rotation);
    }
}
