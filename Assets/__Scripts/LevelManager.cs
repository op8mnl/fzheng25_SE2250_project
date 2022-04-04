using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int currentLevel;
    //public GameObject scott;

    public void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void getNextLevel(String dir)
    {
        if (dir == "right")
        {
            currentLevel++;
        }
        else if (dir == "left")
        {
            currentLevel--;
        }
        SceneManager.LoadScene(currentLevel);
        
    }
    public int getLevel()
    {
        return currentLevel;
    }

    
}
