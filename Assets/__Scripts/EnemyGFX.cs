using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
    public AIPath aiPath;
    public GameObject go;

    // Update is called once per frame
    void Update()
    {
        if (aiPath.desiredVelocity.x >= 0.1f) { // moving right
            go.transform.rotation = Quaternion.Euler(0,0,0);
        } else if (aiPath.desiredVelocity.x <= -0.1f) { // moving left
            go.transform.rotation = Quaternion.Euler(0,180,0);
        }
    }
}
