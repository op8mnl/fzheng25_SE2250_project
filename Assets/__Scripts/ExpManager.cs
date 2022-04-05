using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ExpManager : MonoBehaviour
{
    public Slider xSlider;
    public float _sliderValue;

    // Start is called before the first frame update
    public void Start()
    {
        // sets initial value of slider
        xSlider = GameObject.FindGameObjectWithTag("slider2").GetComponent<Slider>();
        xSlider.value = 0.01f;
        _sliderValue = 0.01f;
    }

    // adjusts slider value
    public void expUpdate(float expVal)
    {
        Debug.Log(expVal);
        _sliderValue = expVal/100f;
        Debug.Log(_sliderValue);
        xSlider = GameObject.FindGameObjectWithTag("slider2").GetComponent<Slider>();
        xSlider.value = (float)_sliderValue;
    }

    private void Update()
    {
      
    }

    // sets _sliderValue value
    public void setExp(float exp) { _sliderValue = exp; }


}