using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class healthManager : MonoBehaviour
{
    public Slider slider;
    public float slider_value;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = slider_value;
    }

    public void healthUpdate(float healthVal)
    {
        slider_value = healthVal;
        Debug.Log(slider_value);
    }

    private void Update()
    {
        slider.value = slider_value;
    }

}
