using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBirdController : Enemy
{

    void Update() {
        if (transform.position.x > scottPlayer.transform.position.x) {
            // face left
            transform.localScale = new Vector3(-0.2f, 0.2f, 0f);
        } else {
            // face right
            transform.localScale = new Vector3(0.2f, 0.2f, 0f);
        }
    }

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
