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
    public void setNextLevel(string direction)
    {
        if (direction == "right")
        {
            currentLevel++;
        }
        else
        {
            currentLevel--;
        }
    }
    public void getNextLevel()
    {
        
        SceneManager.LoadScene(currentLevel);
        GetComponent<SpawnScott>().spawnScott();
    }
    public void setNextLevel(int currentLevel)
    { 
        this.currentLevel = currentLevel;
    }

}
