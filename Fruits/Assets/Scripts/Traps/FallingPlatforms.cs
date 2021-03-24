using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatforms : MonoBehaviour
{
    private bool isFalling = false;

    private float playerPosY;
    private float distancePlayerY;
    private void Update()
    {
        //The distance to destroy the Platform
        playerPosY = GameObject.Find("Player").GetComponent<Transform>().localPosition.y;
        distancePlayerY = this.transform.localPosition.y - playerPosY;

        if (isFalling)
        {
            transform.Translate(new Vector3(0, -Time.deltaTime * 2.5f, 0));

            if (distancePlayerY <= -35)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<Animator>().SetBool("isOn", true);
            StartCoroutine(Falling());
        }
    }

    IEnumerator Falling()
    {
        yield return new WaitForSeconds(0.5f);
        isFalling = true;
    }
}
