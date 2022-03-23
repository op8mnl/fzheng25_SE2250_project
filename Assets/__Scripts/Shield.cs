using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    public GameObject shield;

    private bool activeShield;

    // Start is called before the first frame update
    void Start()
    {
        activeShield = false;
        shield.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Shield"))
        {
            if (!activeShield)
            {
                shield.SetActive(true);
                activeShield = true;
            }
            else
            {
                shield.SetActive(false);
                activeShield = false;
            }
        }
    }

    public bool ActiveShield
    {
        get
        {
            return activeShield;
        }
        set
        {
            activeShield = value;
        }
    }
}
