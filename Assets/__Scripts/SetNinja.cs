using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNinja : MonoBehaviour
{
    public GameObject ninja;

    public GameObject script;

    public void setScript()
    {
        script.GetComponent<LoadCharacter>().setNinja(ninja);
    }
}
