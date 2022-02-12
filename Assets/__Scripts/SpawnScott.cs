using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScott : MonoBehaviour
{
    public GameObject scott;
    
    // Start is called before the first frame update
    void Start()
    {
 
    }
   
    public void spawnScott()
    {
        Instantiate(scott, new Vector2(-10, -2.5f), Quaternion.identity);
    }
}
