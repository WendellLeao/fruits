using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDeath : MonoBehaviour
{
    public float speedY;
    public float speedZ;
    void Update()
    {
        transform.Translate(new Vector2(0, Time.deltaTime * -speedY));
    }
}
