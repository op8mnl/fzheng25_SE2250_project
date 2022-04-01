using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MullerController : MonoBehaviour
{
    private bool triggerFinalExam=false;
    private bool ultCooldown = false;
    private bool swingCooldown = false;
    public float cd = 15f;
    public float cds = 5f;
    public Animator anim;
    public GameObject f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        getShouldUltimate();
        Attack();
    }
    void getShouldUltimate()
    {
        float rand = Random.Range(1, 500);
        
        if (rand == 1 && !ultCooldown )
        {
            triggerFinalExam = true;
        }
    }
    void Attack()
    {
        if (triggerFinalExam == true && !ultCooldown && !anim.GetBool("isSwing"))
        {
            
            ultCooldown=true;
            Invoke("shortUlt", 3f);
            Invoke("cooldownU", cd);
            Invoke("spawnF", 1.25f);
            Debug.Log("Ult");
            anim.SetBool("isUlt", true);
            GetComponent<Rigidbody2D>().AddForce(transform.up * 400);
            GetComponent<Rigidbody2D>().AddForce(transform.right * -400);

        }
        else
        {
            anim.SetBool("isUlt", false);
        }
        if (Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position , transform.position) <= 4 && !swingCooldown && !triggerFinalExam)
        {
            
            anim.SetBool("isSwing",true);
            swingCooldown = true;
            Invoke("cooldownS",cds);
            
        }
        else
        {
            anim.SetBool("isSwing", false);
        }

    }

    void cooldownS()
    {

         swingCooldown = false;
        
    }
    void cooldownU()

    {
        ultCooldown = false;
  
    }
    void shortUlt()
    {
        triggerFinalExam = false;
    }
    void spawnF()
    {
        Instantiate(f, new Vector2(transform.position.x + 2f, transform.position.y + 10f), f.transform.rotation );
        Instantiate(f, new Vector2(transform.position.x + -2f, transform.position.y + 10f), f.transform.rotation );
        Instantiate(f, new Vector2(transform.position.x + 6f, transform.position.y + 14f), f.transform.rotation );
        Instantiate(f, new Vector2(transform.position.x + -6f, transform.position.y + 14f), f.transform.rotation);
        Instantiate(f, new Vector2(transform.position.x + 10f, transform.position.y + 18f), f.transform.rotation);
        Instantiate(f, new Vector2(transform.position.x + -10f, transform.position.y + 18f), f.transform.rotation);
        Instantiate(f, new Vector2(transform.position.x + 14f, transform.position.y + 24f), f.transform.rotation);
        Instantiate(f, new Vector2(transform.position.x + -14f, transform.position.y + 24f), f.transform.rotation);
    }
    
}
