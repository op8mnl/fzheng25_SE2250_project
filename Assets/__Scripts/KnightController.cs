using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnightController : Enemy
{
    // private Transform scott;
    public float maxDistance;
    public float minDistance;
    private bool isOnHill = false;
    private bool isOnGround = false;
    public bool isRightFacing;

    Animator knightAnim; //animator

    Rigidbody2D rb;

    void Start()
    {
        // scott = GameObject.FindGameObjectWithTag("Player").transform;
        knightAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
 
    void FixedUpdate() {
        Vector2 moveDir;    // force that will move the enemy in the desired direction
        float ySpeed = 0f;

        Debug.Log("Speed: " + speed);

        if (transform.position.x > scottPlayer.transform.position.x) {
            // face left
            transform.localScale = new Vector3(isRightFacing ? -1 : 1, 1, 1);

            if (isOnHill) {
                ySpeed = speed;
            } else if (!isOnGround) {
                ySpeed = -speed/2;
            }

            moveDir = new Vector2(-speed, ySpeed);   // move to the left if needed, and if on a hill, move accordingly
            // Debug.Log("moveDir: " + moveDir);
        } else {
            // face right
            transform.localScale = new Vector3(isRightFacing ? 1 : -1, 1, 1);

            if (isOnHill) {
                ySpeed = speed;
            } else if (!isOnGround) {
                ySpeed = -speed/2;
            }

            moveDir = new Vector2(speed, ySpeed);    // move to the right if needed, and if on a hill, move accordingly
            // Debug.Log("moveDir: " + moveDir);
        }

        if (Vector3.Distance(transform.position, scottPlayer.transform.position) >= minDistance) {
            if (rb == null) {
                rb = GetComponent<Rigidbody2D>();
            }
            rb.velocity = moveDir;
            // rb.AddForce((transform.right * speed));
            // Debug.Log("rb.velocity: " + rb.velocity);

            // transform.position += (Vector3)moveDir * Time.deltaTime;

            knightAnim.SetFloat("speed", rb.velocity.x);

            if (Vector3.Distance(transform.position, scottPlayer.transform.position) <= maxDistance)
            {
                //call functions like a shoot or swing function at here or something
                if (knightAnim == null) {
                    knightAnim = GetComponent<Animator>();
                }
                //change animation when player is attack distance
                knightAnim.SetBool("isAttack", true);
            } else {
                knightAnim.SetBool("isAttack", false);
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