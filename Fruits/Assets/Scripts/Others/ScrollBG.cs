using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBG : MonoBehaviour
{
    public float speedScroll;

    private float posY;
    void Update()
    {
        posY = this.transform.localPosition.y;

        if(posY <= 0)
        {
            transform.localPosition = new Vector3(0, 33, 0);
        }

        transform.Translate(new Vector3(0, -Time.deltaTime * speedScroll, 0));
    }
}
