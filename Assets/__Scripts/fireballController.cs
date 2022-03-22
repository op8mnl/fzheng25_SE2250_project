using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireballController : MonoBehaviour
{
    public Vector2 StartingVelocity;
    public DragonController script;
    // Start is called before the first frame update
    void Start()
    {
        script = GameObject.FindGameObjectWithTag("Player").GetComponent<DragonController>();
        if (script.getDirection() == 1)
        {
            GetComponent<Rigidbody2D>().velocity = StartingVelocity;
            Invoke("Destroy", 1.5f);
        }
        if (script.getDirection() == -1)
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
