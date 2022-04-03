using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdController : Enemy
{
    // private Transform scott;
    public float maxDistance;
    public float minDistance;
    public bool canFly;
    public bool isRightFacing;

    public GameObject smallBirdPrefab;

    // Animator birdAnim; //animator

    Rigidbody2D rb;
    private float nextDrop;

    void Start()
    {
        // scott = GameObject.FindGameObjectWithTag("Player").transform;
        // birdAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        nextDrop = Random.Range (0.2f, 2f);
    }
 
    void FixedUpdate() {
        // if (transform.position.x > scottPlayer.transform.position.x) {
        //     // face left
        //     transform.localScale = new Vector3(isRightFacing ? -1 : 1, 1, 1);

        //     moveDir = new Vector2(-speed, 0f);   // move to the left if needed, and if on a hill, move accordingly
        //     // Debug.Log("moveDir: " + moveDir);
        // } else {
        //     // face right
        //     transform.localScale = new Vector3(isRightFacing ? 1 : -1, 1, 1);

        //     moveDir = new Vector2(speed, 0f);    // move to the right if needed, and if on a hill, move accordingly
        //     // Debug.Log("moveDir: " + moveDir);
        // }



        if (speed < 0) {
            // face left
            transform.localScale = new Vector3(isRightFacing ? -1 : 1, 1, 1);
        } else {
            // face right
            transform.localScale = new Vector3(isRightFacing ? 1 : -1, 1, 1);
        }

        rb.velocity = new Vector2(speed, 0f);;    // force that will move the enemy in the desired direction

        // if (Vector3.Distance(transform.position, scottPlayer.transform.position) >= minDistance) {
        //     rb.velocity = moveDir;

            // StartCoroutine(createSmallBird());

            if (Vector3.Distance(transform.position, scottPlayer.transform.position) <= maxDistance)
            {
                // shoot out small birds at random intervals
                // StartCoroutine(createSmallBird());
                if (Time.time > nextDrop) {
                    nextDrop = Time.time + Random.Range (0.2f, 2f);
                    Instantiate(smallBirdPrefab, new Vector2(transform.position.x,transform.position.y - 0.5f), smallBirdPrefab.transform.rotation * Quaternion.Euler(0f, 0f, 0f));
                }
            }
        // }
    }

    void OnTriggerEnter2D(Collider2D other) {
        speed *= -1;

        onTrigEnter(other);
    }

    void OnTriggerExit2D(Collider2D other) {
        onTrigExit(other);
    }

    // private IEnumerator createSmallBird()
    // {
    //     //Wait for 2 seconds
    //     yield return new WaitForSeconds(2);

    //     //create small bird from prefab
    //     Instantiate(smallBirdPrefab, new Vector2(transform.position.x,transform.position.y - 0.5f), smallBirdPrefab.transform.rotation * Quaternion.Euler(0f, 0f, 0f));
    // }
}