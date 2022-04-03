using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class disableAbility2 : MonoBehaviour
{
    public Button ability2Btn;
    public GameObject script;

    private void Start()
    {
        ability2Btn.onClick.AddListener(selectorCall);
    }

    public void selectorCall()
    {
        Debug.Log("calling 2nd selectorCall");
        script.GetComponent<AbilitySelector>().disable2();
    }
}
