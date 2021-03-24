using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;
    private float waitTime = 0.5f;
    private bool isCounting = false;
    private void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            waitTime = 0.2f;
            isCounting = true;
        }
    }
    private void FixedUpdate()
    {
        if (isCounting)
        {
            TurnWayDown();
        }
        else
        {
            waitTime = 0f;
        }
    }

    void TurnWayDown()
    {
        waitTime -= Time.deltaTime;

        if (waitTime >= 0)
        {
            effector.rotationalOffset = 180f;
        }
        else
        {
            effector.rotationalOffset = 0f;
            isCounting = false;
        }
    }
}
