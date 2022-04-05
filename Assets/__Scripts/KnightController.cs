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
    private float ySpeed = 0f;
    private bool attackCooldown = false;

    Animator knightAnim; //animator

    Rigidbody2D rb;

    void Start()
    {
        knightAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
 
    void FixedUpdate() {
        Vector2 moveDir;    // force that will move the enemy in the desired direction

        if (transform.position.x > scottPlayer.transform.position.x) {
            // face left
            transform.localScale = new Vector3(isRightFacing ? -1 : 1, 1, 1);

            if (isOnHill) {
                ySpeed = speed/3;
            } else if (!isOnGround) {
                ySpeed = -speed;
            } else {
                ySpeed = 0f;
            }

            moveDir = new Vector2(-speed, ySpeed);   // move to the left if needed, and if on a hill, move accordingly
        } else {
            // face right
            transform.localScale = new Vector3(isRightFacing ? 1 : -1, 1, 1);

            if (isOnHill) {
                ySpeed = speed/3;
            } else if (!isOnGround) {
                ySpeed = -speed;
            } else {
                ySpeed = 0f;
            }

            moveDir = new Vector2(speed, ySpeed);    // move to the right if needed, and if on a hill, move accordingly
        }

        if (Vector3.Distance(transform.position, scottPlayer.transform.position) >= minDistance) {
            // only move if greater than minimum distance
            rb.velocity = moveDir;
            if (rb == null) {
                rb = GetComponent<Rigidbody2D>();
            }

            knightAnim.SetFloat("speed", rb.velocity.x);

            if (Vector3.Distance(transform.position, scottPlayer.transform.position) <= maxDistance)
            {
                //call functions like a shoot or swing function at here or something
                if (knightAnim == null) {
                    knightAnim = GetComponent<Animator>();
                }

                if (!attackCooldown && !knightAnim.GetBool("isAttack")) {
                    attackCooldown = true;
                    Invoke("cooldown", 1f);

                    //change animation when player is attack distance
                    knightAnim.SetBool("isAttack", true);
                } else {
                    knightAnim.SetBool("isAttack", false);
                }
            } else {
                knightAnim.SetBool("isAttack", false);
            }
        } else {
            //call functions like a shoot or swing function at here or something
                if (knightAnim == null) {
                    knightAnim = GetComponent<Animator>();
                }

                if (!attackCooldown && !knightAnim.GetBool("isAttack")) {
                    attackCooldown = true;
                    Invoke("cooldown", 1f);

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

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Hill")) {
            isOnHill = true;
        }
    }

    void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Hill")) {
            isOnHill = false;
        }
    }

    void cooldown()
    {
        attackCooldown = false;
    }
}