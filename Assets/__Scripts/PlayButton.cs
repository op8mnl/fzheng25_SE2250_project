using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    public void playGame()
    {
        GameObject.FindGameObjectWithTag("Script").GetComponent<LevelManager>().getNextLevel("right");
        GameObject.FindGameObjectWithTag("Script").GetComponent<LoadCharacter>().loadPlayer();
    }
}
