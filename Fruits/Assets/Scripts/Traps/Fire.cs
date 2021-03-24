using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private float fireRate;

    private float time = 0f;
    private float timerOn = 3f;
    private float timerOff = 6f;

    private bool isOn = false;

    public GameObject FireTrigger;

    void Update()
    {
        time = time + Time.deltaTime;

        if(time >= timerOn && time < timerOff)
        {
            isOn = true;
        }
        else if(time >= timerOff)
        {
            isOn = false;
            time = 0;
        }

        if (isOn)
        {
            //AudioManager.instance.Play("Fire");
            GetComponent<Animator>().SetBool("isOn", true);
            FireTrigger.SetActive(true);
        }
        else
        {
            GetComponent<Animator>().SetBool("isOn", false);
            FireTrigger.SetActive(false);
        }


    }
}
