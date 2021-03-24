using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : EnemyFire
{
    private void FixedUpdate()
    {
        DistanceCount();

        if (distanceX <= distanceAttack && Mathf.Abs(distanceY) <= distanceAttack / 2.5f && Player.isAlive)
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
        }
        else
        {
            isFirstShot = true;
        }
    }
}
