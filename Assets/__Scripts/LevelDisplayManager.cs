using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDisplayManager : MonoBehaviour
{

    public Text levelText;

    // Start is called before the first frame update
    void Start()
    {
        levelText.text = "Level 1";
        levelText.color = Color.black;
    }

    // sets text value
    internal void setLevelText(int level)
    {
        levelText.text = "Level " + level;
    }

    // changes text font color to white
    internal void setTextWhite()
    {
        levelText.color = Color.white;
    }

    // changes text font color to black
    internal void setTextBlack()
    {
        levelText.color = Color.black;
    }
}

