using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : Enemy
{
    // private Transform scott;
    public float moveSpeed;
    public float maxDistance;
    public float minDistance;
    private bool isOnHill = false;
    private bool isOnGround = false;

    public bool isRightFacing;

    Rigidbody2D rb;

    void Start()
    {
        // scott = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }
 
    void FixedUpdate() {
        Vector2 moveDir;    // force that will move the enemy in the desired direction
        float ySpeed = 0f;

        if (transform.position.x > scottPlayer.transform.position.x) {
            // face left
            transform.localScale = new Vector3(isRightFacing ? -1 : 1, 1, 1);

            if (isOnHill) {
                ySpeed = moveSpeed/2;
            } else if (!isOnGround) {
                ySpeed = -moveSpeed/2;
            }

            moveDir = new Vector2(-moveSpeed, ySpeed);   // move to the left if needed, and if on a hill, move accordingly

        } else {
            // face right
            transform.localScale = new Vector3(isRightFacing ? 1 : -1, 1, 1);

            if (isOnHill) {
                ySpeed = moveSpeed;
            } else if (!isOnGround) {
                ySpeed = -moveSpeed;
            }

            moveDir = new Vector2(moveSpeed, ySpeed);    // move to the right if needed, and if on a hill, move accordingly
        }

        if (Vector3.Distance(transform.position, scottPlayer.transform.position) >= minDistance) {
            rb.velocity = moveDir;

            if (Vector3.Distance(transform.position, scottPlayer.transform.position) <= maxDistance)
            {
                //call any function like a shoot or swing function at here or something
            }
 
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Hill")) {
            isOnHill = true;
        }
        if (other.CompareTag("Ground")) {
            isOnGround = true;
        }

        onTrigEnter(other);
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Hill")) {
            isOnHill = false;
        }
        if (other.CompareTag("Ground")) {
            isOnGround = false;
        }

        onTrigExit(other);
    }
}