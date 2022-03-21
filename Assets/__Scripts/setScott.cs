using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setScott : MonoBehaviour
{
    public GameObject scott;

    public GameObject script;

    public void setScript()
    {
        script.GetComponent<LoadCharacter>().setScott(scott);
    }
}
