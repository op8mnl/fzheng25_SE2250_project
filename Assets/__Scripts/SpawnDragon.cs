using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDragon : MonoBehaviour
{
    public GameObject dragon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dragon.GetComponent<ScottController>()._healthPoints = 100f; //set the healthpoints to 100
        dragon.GetComponent<healthManager>().slider_value = 100f; //slider value (NEEDS TO BE FIXED)
        Instantiate(dragon, new Vector2(-10, -2.5f), Quaternion.identity);
    }
}
