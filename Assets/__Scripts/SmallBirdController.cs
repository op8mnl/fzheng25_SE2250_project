using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBirdController : Enemy
{
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Hill"))
        {
            StartCoroutine(destroy());
        }
        if (col.gameObject.CompareTag("Ground")) {
            StartCoroutine(destroy());
        }
    }

    private IEnumerator destroy()
    {
        //Wait for 2 seconds
        yield return new WaitForSeconds(2);

        //Destroy object
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other) {
        onTrigEnter(other);
    }

    void OnTriggerExit2D(Collider2D other) {
        onTrigExit(other);
    }
}
