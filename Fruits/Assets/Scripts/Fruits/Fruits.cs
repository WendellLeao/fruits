using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruits : MonoBehaviour
{
    //To collect only 1 fruit
    private int canGet = 0;
    private int canCount = 1;

    private void Awake()
    {
        GameManager.howManyNeeds = 0;
    }
    private void Start()
    {
        if(canCount == 1)
        {
            GameManager.howManyNeeds++;
            canCount = 0;
        }
    }
    private void Update()
    {
        if (canGet == 1)
        {
            GetComponent<Animator>().SetBool("isCollected", true);
            StartCoroutine(DestroyFruit());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && canGet == 0 && Player.isAlive)
        {
            GameManager.fruitQtd ++;
            canGet = 1;
        }
    }

    IEnumerator DestroyFruit()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
