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
        GameObject.FindGameObjectWithTag("Script").GetComponent<LevelManager>().getNextLevel("right");
        GameObject.FindGameObjectWithTag("Script").GetComponent<LoadCharacter>().loadPlayer();
    }
    
}
