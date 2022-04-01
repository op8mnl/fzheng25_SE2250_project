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

    // Animator birdAnim; //animator

    Rigidbody2D rb;

    void Start()
    {
        // scott = GameObject.FindGameObjectWithTag("Player").transform;
        // birdAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
 
    void FixedUpdate() {
        Vector2 moveDir;    // force that will move the enemy in the desired direction
        float ySpeed = 0f;

        if (transform.position.x > scottPlayer.transform.position.x) {
            // face left
            transform.localScale = new Vector3(isRightFacing ? -1 : 1, 1, 1);

            moveDir = new Vector2(-speed, ySpeed);   // move to the left if needed, and if on a hill, move accordingly
            // Debug.Log("moveDir: " + moveDir);
        } else {
            // face right
            transform.localScale = new Vector3(isRightFacing ? 1 : -1, 1, 1);

            moveDir = new Vector2(speed, ySpeed);    // move to the right if needed, and if on a hill, move accordingly
            // Debug.Log("moveDir: " + moveDir);
        }

        if (Vector3.Distance(transform.position, scottPlayer.transform.position) >= minDistance) {
            rb.velocity = moveDir;
            // Debug.Log("rb.velocity: " + rb.velocity);

            // transform.position += (Vector3)moveDir * Time.deltaTime;

            // birdAnim.SetFloat("speed", rb.velocity.x);

            // if (Vector3.Distance(transform.position, scottPlayer.transform.position) <= maxDistance)
            // {
            //     //call functions like a shoot or swing function at here or something
            //     if (birdAnim == null) {
            //         birdAnim = GetComponent<Animator>();
            //     }
            //     //change animation when player is attack distance
            //     birdAnim.SetBool("isAttack", true);
            // } else {
            //     birdAnim.SetBool("isAttack", false);
            // }
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        onTrigEnter(other);
    }

    void OnTriggerExit2D(Collider2D other) {
        onTrigExit(other);
    }
}