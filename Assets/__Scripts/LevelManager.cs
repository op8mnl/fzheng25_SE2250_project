using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int currentLevel;

    public void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void getNextLevel(int current, String dir)
    {
        if(dir == "right")
        {
            currentLevel++;
        }else if(dir == "left")
        {
            currentLevel--;
        }
        SceneManager.LoadScene(currentLevel);
        GetComponent<LoadCharacter>().loadPlayer();
    }
    public void setNextLevel(int currentLevel)
    { 
        this.currentLevel = currentLevel;
    }

}
