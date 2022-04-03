using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdController : Enemy
{
    public float maxDistance;
    public float minDistance;
    public bool canFly;
    public bool isRightFacing;

    public GameObject smallBirdPrefab;

    Rigidbody2D rb;
    private float nextDrop;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        nextDrop = Random.Range (0.2f, 2f);
    }
 
    void FixedUpdate() {
        if (speed < 0) {
            // face left
            transform.localScale = new Vector3(isRightFacing ? -1 : 1, 1, 1);
        } else {
            // face right
            transform.localScale = new Vector3(isRightFacing ? 1 : -1, 1, 1);
        }

        rb.velocity = new Vector2(speed, 0f);;    // force that will move the enemy in the desired direction

        if (Vector3.Distance(transform.position, scottPlayer.transform.position) <= maxDistance)
        {
            // shoot out small birds at random intervals
            if (Time.time > nextDrop) {
                nextDrop = Time.time + Random.Range (0.2f, 2f);
                Instantiate(smallBirdPrefab, new Vector2(transform.position.x,transform.position.y - 0.5f), smallBirdPrefab.transform.rotation * Quaternion.Euler(0f, 0f, 0f));
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        speed *= -1;

        onTrigEnter(other);
    }

    void OnTriggerExit2D(Collider2D other) {
        onTrigExit(other);
    }
}