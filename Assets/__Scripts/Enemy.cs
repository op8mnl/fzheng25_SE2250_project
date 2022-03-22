using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float healthPoints = 100f;
    public float speed = 200f;
    protected bool hitScan;
    // Change scott to Start --> FindGameObjectWithTag("Player")
    // That should help with getting this to work with all players
    public GameObject scott;

    // Update is called once per frame
    void Update () {
        die();
    }

    protected void onTrigEnter(Collider2D other) {
        if (other.gameObject.CompareTag("basicAttack")|| other.gameObject.CompareTag("Player")|| other.gameObject.CompareTag("strike"))
        {
            hitScan = true;
        }
    }

    protected void onTrigExit(Collider2D other)
    {
        if (other.gameObject.CompareTag("basicAttack") && hitScan == true)
        {
            hit();
            hitScan = false;
        }
        if (other.gameObject.CompareTag("strike") && hitScan == true)
        {

        }
        if(other.gameObject.CompareTag("Player") && hitScan == true)
        {
            scott.GetComponent<ScottController>().takeDamage(10f);
        }
    }
    
    protected void hit() {
        healthPoints -= 10; 
        gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * 150);
        gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * 200);
    }

    protected void die()
    {
        if(healthPoints <= 0)
        {
            Destroy(gameObject);
            scott.GetComponent<ScottController>().gainExp(30f);
        }
    }

}
