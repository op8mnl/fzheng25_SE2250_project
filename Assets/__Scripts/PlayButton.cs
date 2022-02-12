using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    public GameObject script;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void playGame()
    {
        script.GetComponent<LevelManager>().setNextLevel(1);
        script.GetComponent<LevelManager>().getNextLevel();
    }
    
}
