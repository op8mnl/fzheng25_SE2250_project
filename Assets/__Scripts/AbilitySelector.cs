using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySelector : MonoBehaviour
{
    public GameObject scott;
    //public GameObject ninja;
    //public GameObject dragon;

    public void disable1()
    {
        scott.GetComponent<ScottController>().setAbility1(true);
        //GetComponent<NinjaController>().setAbility1(true);
        //GetComponent<DragonController>().setAbility1(true);
    }

    public void disable2()
    {
        scott.GetComponent<ScottController>().setAbility2(true);
        //GetComponent<NinjaController>().setAbility2(true);
        //GetComponent<DragonController>().setAbility2(true);
    }

    public void disable3()
    {
        scott.GetComponent<ScottController>().setAbility3(true);
        //GetComponent<NinjaController>().setAbility3(true);
        //GetComponent<DragonController>().setAbility3(true);
    }
}


// when button clicked for ability not using, need a boolean variable to use in if
// statement to disable button pressing, like how activeShield in ScottController prevents.
// need so when one is clicked, the other are set true as to not disable all

// scripts for each button, when pressed, sent to this script, then depending on which pressed (if statement),
// method call to each player controller to set specific ability bool to false and the others true,
// and in the controllers if certain bool is true, only then can ability be used

// how to get OnClick to call method in disable scripts?