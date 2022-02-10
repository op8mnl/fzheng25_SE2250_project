using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    public Text plot;

    void Start()
    {
        plot.text = "";
    }

    private void OnTriggerEnter2D(Collider2D rectangle)
    {

        if (rectangle.CompareTag("Player"))
        {
            plot.text = "Young Scott is on a life changing journey to find love on Timber. " +
               "Scott’s dedication to his journey led him to have his phone " +
               "confiscated during Ari Steinfeld’s MATH 1512 lecture.";

        }
 
    }
    private void OnTriggerExit2D(Collider2D rectangle)
    {
        plot.text = "";
    }

}
