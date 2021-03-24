using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : EnemyController
{
    protected override void Update()
    {
        base.Update();

        Move();

        if ((playerPos.position.x < transform.position.x && isFacingRight) || (playerPos.position.x > transform.position.x && !isFacingRight) && distanceY >= 0f && distanceY <= 4f)
        {
            Flip();
        }
    }
}
