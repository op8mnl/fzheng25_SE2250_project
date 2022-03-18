using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScott : MonoBehaviour
{
    public GameObject scott;
   
    public void spawnScott()
    {
        Instantiate(scott, new Vector2(-10, -2.5f), Quaternion.identity);
    }
}
