using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FController : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke("destroyF", 3f);
    }

    void destroyF()
    {
        Destroy(gameObject);
    }
}
