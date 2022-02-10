using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager
{
    public void getNextLevel(int currentLevel, string direction)
    {
        if (currentLevel == 0 && direction == "right")
        {
            currentLevel++;
        }
        else
        {
            currentLevel--;
        }
        SceneManager.LoadScene(currentLevel);
    }
    public void getNextLevel(int level)
    {
        SceneManager.LoadScene(level);
    }
}
