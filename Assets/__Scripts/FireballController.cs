using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
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

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
