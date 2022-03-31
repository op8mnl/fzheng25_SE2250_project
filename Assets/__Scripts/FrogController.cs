using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : Enemy
{
    public float jumpHeight = 500f;
    private int count = 0;
    private int movementDir;
    private bool shouldJump;
    private bool isLanded;
    Animator frog;

    // Start is called before the first frame update
    void Start()
    {
        initMovement();
        frog = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        getShouldJump();
        movement(movementDir);
        // die();

    }
    void OnTriggerEnter2D(Collider2D other) {
        onTrigEnter(other);
    }

    void OnTriggerExit2D(Collider2D other) {
        onTrigExit(other);
    }

    void initMovement()
    {
        movementDir = Random.Range(1, 3);
    }
    int force()
    {
        if(gameObject.GetComponent<Rigidbody2D>().velocity.x > 0)
        {
            return 1;
        }
        else if (gameObject.GetComponent<Rigidbody2D>().velocity.x < 0)
        {
            return -1;
        }
        return 0;
    }
    void movement(int dir)
    {
        if (movementDir == 1 && shouldJump == true)
        {
            //left movement
            gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * -1 * speed);
            gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * jumpHeight*2);  
        }
        else if (movementDir == 2 && shouldJump == true)
        {
            //right movement
            gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * speed*2);
            gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * jumpHeight*2 );
        }

        //Anim
        if (gameObject.GetComponent<Rigidbody2D>().velocity.y > 0)
        {
            frog.SetBool("isFalling", false);
            frog.SetBool("isJump",true);
        }else if (gameObject.GetComponent<Rigidbody2D>().velocity.y < 0 )
        {
            frog.SetBool("isJump", false);
            frog.SetBool("isFalling", true);
        }
        else
        {
            frog.SetBool("isFalling", false);
            frog.SetBool("isJump", false);
        }
        initMovement();
    }
    void getShouldJump()
    {
        
        float rand = Random.Range(1, 1000);
        rand = rand / (1+(count/10000));
        
        if (rand < 1)
        {
            rand = 1;
        }

        if (rand == 1 && isLanded == true)
        {
            count = 0;
            shouldJump = true;
        }
        else
        {
            count++;
            shouldJump = false;
        }

        
    }
    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.CompareTag("Ground")|| col.gameObject.CompareTag("Hill"))
        {
            isLanded = true;
            frog.SetBool("isLanded", true);
            
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        isLanded = false;
        frog.SetBool("isLanded", false);
    }
}
