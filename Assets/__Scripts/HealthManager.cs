using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthManager : MonoBehaviour
{
    public Slider mSlider;
    public float _sliderValue;

    // Start is called before the first frame update
    void Start()
    {
        mSlider = GameObject.FindGameObjectWithTag("slider").GetComponent<Slider>();
        _sliderValue = 100f;
        mSlider.value = 100f;
    }

    // adjusts slider value
    public void healthUpdate(float healthVal)
    {
        _sliderValue = healthVal;
        mSlider = GameObject.FindGameObjectWithTag("slider").GetComponent<Slider>();
        mSlider.value = (float)_sliderValue;
    }

    // sets _sliderValue value
    public void setHealth(float health) { _sliderValue = health; }
}
