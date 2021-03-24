using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFloor : MonoBehaviour
{
    public float speedMoveFloor;

    private float posX;
    void Update()
    {
        posX = this.transform.localPosition.x;

        if(posX <= -9){
            transform.localPosition = new Vector3(13, 0, 0);
        }

        transform.Translate(new Vector2(-Time.deltaTime * speedMoveFloor,0));
    }
}
