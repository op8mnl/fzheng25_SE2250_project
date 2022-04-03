using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class disableAbility3 : MonoBehaviour
{
    public Button ability3Btn;
    public GameObject script;

    private void Start()
    {
        ability3Btn.onClick.AddListener(selectorCall);
    }

    public void selectorCall()
    {
        Debug.Log("calling 3rd selectorCall");
        script.GetComponent<AbilitySelector>().disable3();
    }
}

//add AddListener for each of these scripts, making the buttons injected as GameObject being referenced