using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class disableAbility2 : MonoBehaviour
{
    public Button ability2Btn;
    private bool _isAb2Disabled = false;

    //public GameObject script;

    private void Start()
    {
        ability2Btn.onClick.AddListener(disable2);
    }

    public void disable2()
    {
        _isAb2Disabled = !_isAb2Disabled;
        //Debug.Log("calling 1st selectorCall");
        //script.GetComponent<AbilitySelector>().disable1();
    }

    public bool getDisabled2()
    {
        return _isAb2Disabled;
    }
}
