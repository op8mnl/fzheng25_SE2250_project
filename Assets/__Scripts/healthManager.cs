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
        // slider = GameObject.Find("HealthContainer").GetComponent<Slider>();
        mSlider = GameObject.FindGameObjectWithTag("slider").GetComponent<Slider>();
        mSlider.value = _sliderValue;
    }

    public void healthUpdate(float healthVal)
    {
        Debug.Log(healthVal);
        _sliderValue = healthVal;
        Debug.Log(_sliderValue);
        mSlider = GameObject.FindGameObjectWithTag("slider").GetComponent<Slider>();
        mSlider.value = (float)_sliderValue;
    }

    private void Update()
    {
        // mSlider = GameObject.FindGameObjectWithTag("slider").GetComponent<Slider>();
        // mSlider.value = (float)_sliderValue;
        // Debug.Log(_sliderValue);
    }

    public void setHealth(float health) { _sliderValue = health; }
    

}