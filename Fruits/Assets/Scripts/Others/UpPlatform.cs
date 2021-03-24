using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpPlatform : MonoBehaviour
{
    public LayerMask upPlataform;
    void Update()
    {
        if(Player.isGrounded == false) 
        {
            GetComponent<CompositeCollider2D>().isTrigger = true;
        }
        else
        {
            GetComponent<CompositeCollider2D>().isTrigger = false;
        }
    }
}
