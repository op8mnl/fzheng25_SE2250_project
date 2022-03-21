using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setDragon : MonoBehaviour
{
    public GameObject dragon;

    public GameObject script;

    public void setScript()
    {
        script.GetComponent<LoadCharacter>().setNinja(dragon);
    }
}
