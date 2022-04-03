using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableAbility2 : MonoBehaviour
{
    private void Start()
    {
        selectorCall();
    }
    public void selectorCall()
    {
        GetComponent<AbilitySelector>().disable2();
    }
}
