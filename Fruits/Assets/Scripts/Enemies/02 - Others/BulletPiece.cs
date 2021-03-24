using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPiece : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(Flashing());
    }
    IEnumerator Flashing()
    {
        yield return new WaitForSeconds(0.4f);
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.25f);
        GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.25f);
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.25f);
        GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.25f);
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.25f);
        GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.25f);
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.25f);
        GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.25f);
        GetComponent<SpriteRenderer>().enabled = false;
        Destroy(gameObject);
    }
}
