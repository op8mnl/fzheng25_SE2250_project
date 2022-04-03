using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggController : MonoBehaviour
{
    public Vector2 StartingVelocity;
    public BirdController script;
    // Start is called before the first frame update
    void Start()
    {
        script = GameObject.FindGameObjectWithTag("Bird").GetComponent<BirdController>();
        GetComponent<Rigidbody2D>().velocity = StartingVelocity;
        Invoke("Destroy", 5f);

    }
    private void Destroy()
    {
        Destroy(gameObject);
    }
}
