using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordWaveController : MonoBehaviour
{
    public Vector2 StartingVelocity;
    private ScottController script;
    // Start is called before the first frame update
    void Start()
    {
        script = GameObject.FindGameObjectWithTag("Player").GetComponent<ScottController>();
        if (script.getDirection() == true)
        {
            GetComponent<Rigidbody2D>().velocity = StartingVelocity;
            Invoke("Destroy", 3f);
            
        }
        if (script.getDirection() == false)
        {
            GetComponent<Rigidbody2D>().velocity = -StartingVelocity;
            Invoke("Destroy", 3f);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        grow();
    }
    private void Destroy()
    {
        Destroy(gameObject);
    }
    private void grow()
    {
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x + Time.deltaTime*0.2f, gameObject.transform.localScale.y + Time.deltaTime * 0.2f, gameObject.transform.localScale.z);
    }
}
