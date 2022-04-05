using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public Vector2 StartingVelocity;
    private NinjaController script;
    // Start is called before the first frame update
    void Start()
    {
        script = GameObject.FindGameObjectWithTag("Player").GetComponent<NinjaController>();
        if (script.getDirection() == true)
        {
            GetComponent<Rigidbody2D>().velocity = StartingVelocity;
            Invoke("Destroy", 1.5f);

        }
        if (script.getDirection() == false)
        {
            GetComponent<Rigidbody2D>().velocity = -StartingVelocity;
            Invoke("Destroy", 1.5f);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Destroy()
    {
        Destroy(gameObject);
    }
    
}
