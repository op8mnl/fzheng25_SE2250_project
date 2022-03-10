using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour
{
    public double healthPoints = 100;
    private int movementDir;
    private bool shouldJump;
    private bool isJump;
    private bool isLanded;
    private bool hitScan;
    // Start is called before the first frame update
    void Start()
    {
        initMovement();
    }

    // Update is called once per frame
    void Update()
    {
        getShouldJump();
        movement(movementDir);
        die();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("basicAttack"))
        {
            hitScan = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("basicAttack") && hitScan == true)
        {
            Debug.Log("hit");
            hit();
            hitScan = false;
        }
    }
    private void hit() {
        //hits 3 times for basic attack
        healthPoints -= 10;
        gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * 50);
        gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * 25);
    }

    private void die()
    {
        if(healthPoints <= 0)
        {
            Destroy(gameObject);
        }
    }
    void initMovement()
    {
        movementDir = Random.Range(1, 3);
    }
    void movement(int dir)
    {
        if (movementDir == 1 && shouldJump == true)
        {
            //left movement
            
        }
        else if (movementDir == 2 && shouldJump == true)
        {
            //right movement

        }
    }
    void getShouldJump()
    {
        int rand = Random.Range(1, 11);
        if (rand == 1 && isLanded == true)
        {
            shouldJump = true;
        }else
        {
            shouldJump = false;
        }

    }
}
