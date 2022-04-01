using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MullerController : MonoBehaviour
{
    private bool triggerFinalExam=false;
    private bool ultCooldown = false;
    private bool swingCooldown = false;
    public float cd = 4f;
    public float cds = 3f;
    public Animator anim;
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
            ultCooldown = true;
            Invoke("cooldownU",4f);
        }
    }
    void Attack()
    {
        if (triggerFinalExam == true)
        {
            triggerFinalExam= false;
            Debug.Log("Ult");
            GetComponent<Rigidbody2D>().AddForce(transform.up * 400);
            GetComponent<Rigidbody2D>().AddForce(transform.right * -400);
        }
        if (Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position , transform.position) <= 4 && !swingCooldown && !triggerFinalExam)
        {
            
            anim.SetBool("isSwing",true);
            swingCooldown = true;
            Invoke("cooldownS",2);
            
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

}
