using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float healthPoints = 100f;
    public float speed;
    protected bool hitScan = true;
    public float expPointsGiven;
    public float damageGiven;
    protected GameObject scottPlayer;

    void Awake () {
        scottPlayer = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update () {
        die();
    }

    protected void onTrigEnter(Collider2D other) {
        // if (other.gameObject.CompareTag("basicAttack") || other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("strike"))
        if (other.gameObject.CompareTag("Player") && hitScan == true)
        {
            // hitScan = true;

            if (scottPlayer == null) {
                scottPlayer = GameObject.FindGameObjectWithTag("Player");
            }

            scottPlayer.GetComponent<ScottController>().takeDamage(damageGiven);
            hitScan = false;
            Invoke("hitScanTrue", 1.0f);
        }
    }

    protected void onTrigExit(Collider2D other)
    {   
        if (other.gameObject.CompareTag("basicAttack"))
        {
            hit();
            // Invoke("hitScanFalse", 1.0f);
        }
        if (other.gameObject.CompareTag("strike"))
        {
            hit();
            // Invoke("hitScanFalse", 1.0f);
        }
        // if(other.gameObject.CompareTag("Player") && hitScan == true)
        // {
        //     scottPlayer.GetComponent<ScottController>().takeDamage(damageGiven);
        //     // Invoke("hitScanFalse", 1.0f);
        //     hitScan = false;
        // }
    }
    
    protected void hit() {
        gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * 150);
        gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * 200);

        healthPoints -= scottPlayer.GetComponent<ScottController>().getScottDamage(); 
        Debug.Log("HealthPoints: " + healthPoints);
    }

    protected virtual void die() {
        if(healthPoints <= 0)
        {
            Destroy(gameObject);
            scottPlayer.GetComponent<ScottController>().gainExp(expPointsGiven);
        }
    }

    protected void hitScanTrue() {
        hitScan = true;
    }

    protected void hitScanFalse() {
        hitScan = false;
    }
}
