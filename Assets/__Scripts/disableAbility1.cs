using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableAbility1 : MonoBehaviour
{

    public void selectorCall()
    {
        GetComponent<AbilitySelector>().disable1();
    }
}
