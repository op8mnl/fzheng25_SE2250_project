using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySelector : MonoBehaviour
{
    public Button ability1Btn;
    public Button ability2Btn;
    public Button ability3Btn;

    private bool _isAb1Disabled = false;
    private bool _isAb2Disabled = false;
    private bool _isAb3Disabled = true;

    //public GameObject script;

    private void Start()
    {
        ability1Btn.onClick.AddListener(disable1);
        ability2Btn.onClick.AddListener(disable2);
        ability3Btn.onClick.AddListener(disable3);
    }

    public void disable1()
    {
        Debug.Log("Button 1 is pressed");
        _isAb1Disabled = !_isAb1Disabled;
        _isAb2Disabled = false;
        _isAb3Disabled = false;
       
    }
    public void disable2()
    {
        Debug.Log("Button 2 is pressed");
        _isAb1Disabled = false;
        _isAb2Disabled = !_isAb2Disabled;
        _isAb3Disabled = false;
      
    }
    public void disable3()
    {
        Debug.Log("Button 3 is pressed");
        _isAb1Disabled = false;
        _isAb2Disabled = false;
        _isAb3Disabled = !_isAb3Disabled;
    }

    public bool getDisabled1()
    {
        return _isAb1Disabled;
    }
    public bool getDisabled2()
    {
        return _isAb2Disabled;
    }
    public bool getDisabled3()
    {
        return _isAb3Disabled;
    }

}