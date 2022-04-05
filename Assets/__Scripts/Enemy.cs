using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using System.String.Equals;

public class Enemy : MonoBehaviour
{
    public float healthPoints = 100f;
    public float speed;
    protected bool hitScan = true;
    public float expPointsGiven;
    public float damageGiven;
    protected GameObject scottPlayer;
    protected bool isScott = false;
    protected bool isDragon = false;
    protected bool isNinja = false;

    void Awake () {
        scottPlayer = GameObject.FindGameObjectWithTag("Player");

        if (scottPlayer.GetComponent<ScottController>() != null) {
            Debug.Log("We are officially Scott");
            isScott = true;
        }
        if (scottPlayer.GetComponent<DragonController>() != null) {
            Debug.Log("We are officially Dragon");
            isDragon = true;
        }
        if (scottPlayer.GetComponent<NinjaController>() != null) {
            Debug.Log("We are officially Ninja");
            isNinja = true;
        }
    }

    // Update is called once per frame
    void Update () {
        die();
    }

    protected void onTrigEnter(Collider2D other) {
        if (other.gameObject.CompareTag("Player") && hitScan == true)
        {
            if (scottPlayer == null) {
                scottPlayer = GameObject.FindGameObjectWithTag("Player");
                if (scottPlayer.GetComponent<ScottController>() != null) {
                    isScott = true;
                }
                if (scottPlayer.GetComponent<DragonController>() != null) {
                    isDragon = true;
                }
                if (scottPlayer.GetComponent<NinjaController>() != null) {
                    isNinja = true;
                }
            }

            if (isScott) {
                scottPlayer.GetComponent<ScottController>().takeDamage(damageGiven);
            } else if (isDragon) {
                scottPlayer.GetComponent<DragonController>().takeDamage(damageGiven);
            } else if (isNinja) {
                scottPlayer.GetComponent<NinjaController>().takeDamage(damageGiven);
            } else {
                Debug.Log("WE've DONE MUCKED up");
            }

            hitScan = false;
            Invoke("hitScanTrue", 1.0f);
        }
    }

    protected void onTrigExit(Collider2D other)
    {   
        if (other.gameObject.CompareTag("basicAttack") || other.gameObject.CompareTag("strike") || other.gameObject.CompareTag("Strike") || other.gameObject.CompareTag("Slash") || other.gameObject.CompareTag("projectile"))
        {
            hit();
        }
    }
    
    protected void hit() {
        if (scottPlayer == null) {
            scottPlayer = GameObject.FindGameObjectWithTag("Player");
            if (scottPlayer.GetComponent<ScottController>() != null) {
                isScott = true;
            }
            if (scottPlayer.GetComponent<DragonController>() != null) {
                isDragon = true;
            }
            if (scottPlayer.GetComponent<NinjaController>() != null) {
                isNinja = true;
            }
        }

        if (scottPlayer.transform.position.x < transform.position.x) {  // Scott is looking right
            gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * 150);
            gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * 200);
        } else {
            gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * -150);
            gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * 200);
        }

        if (isScott) {
            healthPoints -= scottPlayer.GetComponent<ScottController>().getScottDamage(); 
        } else if (isDragon) {
            healthPoints -= scottPlayer.GetComponent<DragonController>().getScottDamage(); 
        } else if (isNinja) {
            healthPoints -= scottPlayer.GetComponent<NinjaController>().getScottDamage(); 
        } else {
            Debug.Log("WE've DONE MUCKED up");
        }

        Debug.Log("HealthPoints: " + healthPoints);
    }

    protected virtual void die() {
        if(healthPoints <= 0)
        {
            Destroy(gameObject);

            if (scottPlayer == null) {
                scottPlayer = GameObject.FindGameObjectWithTag("Player");
                if (scottPlayer.GetComponent<ScottController>() != null) {
                    isScott = true;
                }
                if (scottPlayer.GetComponent<DragonController>() != null) {
                    isDragon = true;
                }
                if (scottPlayer.GetComponent<NinjaController>() != null) {
                    isNinja = true;
                }
            }

            if (isScott) {
                scottPlayer.GetComponent<ScottController>().gainExp(expPointsGiven);
            } else if (isDragon) {
                scottPlayer.GetComponent<DragonController>().gainExp(expPointsGiven);
            } else if (isNinja) {
                scottPlayer.GetComponent<NinjaController>().gainExp(expPointsGiven); 
            } else {
                Debug.Log("WE've DONE MUCKED up");
            }
        }
    }

    protected void hitScanTrue() {
        hitScan = true;
    }

    protected void hitScanFalse() {
        hitScan = false;
    }
}
