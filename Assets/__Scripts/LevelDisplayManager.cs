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

    internal void setLevelText(int level)
    {
        levelText.text = "Level " + level;
    }

    //internal void setTextColor(String color)
    //{
    //    scottLevelText.text = "Level " + level;
    //}

    internal void setTextWhite()
    {
        levelText.color = Color.white;
    }

    internal void setTextBlack()
    {
        levelText.color = Color.black;
    }
}


// attach text to this GameObject
// in player controllers, change text