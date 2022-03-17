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
        scott.GetComponent<ScottController>()._healthPoints = 100f;
        scott.GetComponent<healthManager>().slider_value = 100f;
        Instantiate(scott, new Vector2(-10, -2.5f), Quaternion.identity);
    }
}
