using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCam : MonoBehaviour
{
    public static bool canShakeCam = false;
    void Update()
    {
        if (canShakeCam)
        {
            GetComponent<Animator>().SetTrigger("ShakeCam");
            ShakeCam.canShakeCam = false;
        }
    }
}
