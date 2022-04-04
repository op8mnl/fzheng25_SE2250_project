using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class disableAbility3 : MonoBehaviour
{
    public Button ability3Btn;
    private bool _isAb3Disabled = false;

    public GameObject script;

    private void Start()
    {
        ability3Btn.onClick.AddListener(disable3);
    }

    public void disable3()
    {
        _isAb3Disabled = !_isAb3Disabled;
        //Debug.Log("calling 1st selectorCall");
        //script.GetComponent<AbilitySelector>().disable1();
    }

    public bool getDisabled3()
    {
        return _isAb3Disabled;
    }
}
