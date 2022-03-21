using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    public GameObject[] players;
    public int selected = 0;

    public void NextCharacter()
    {
        players[selected].SetActive(false);
        selected = (selected + 1) & players.Length;
        players[selected].SetActive(true);
    }

    public void PreviousCharacter()
    {
        players[selected].SetActive(false);
        selected--;
        if (selected < 0)
        {
            selected += players.Length;
        }
        players[selected].SetActive(true);
    }
        public void StartGame()
    {
        PlayerPrefs.SetInt("selected", selected);
    }
}
