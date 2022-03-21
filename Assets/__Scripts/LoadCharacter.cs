using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCharacter : MonoBehaviour
{
    private GameObject player;
    public GameObject scott;

    private void Start()
    {
        player = scott;
    }

    public void setScott(GameObject hoppy)
    {
        player = hoppy;
    }

    public void setNinja(GameObject ninja)
    {
        player = ninja;
    }

    public void setDragon(GameObject dragon)
    {
        player = dragon;
    }
    public void loadPlayer()
    {
        Instantiate(player, new Vector2(-10, -2.5f), Quaternion.identity);
    }

}
