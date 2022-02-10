using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Camera cam;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        cam.transform.position = new Vector3(-7.5f, -2, -1);
    }

    // Update is called once per frame
    void Update()
    {
       
        var scottPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        cam.transform.position = new Vector3(scottPos.x,scottPos.y,-1);
    }
}
