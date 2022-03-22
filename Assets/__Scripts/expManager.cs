using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class expManager : MonoBehaviour
{
    public Slider xSlider;
    public float _sliderValue;

    // Start is called before the first frame update
    void Start()
    {
        // slider = GameObject.Find("HealthContainer").GetComponent<Slider>();
        xSlider = GameObject.FindGameObjectWithTag("slider").GetComponent<Slider>();
        xSlider.value = _sliderValue;
    }

    public void expUpdate(float expVal)
    {
        Debug.Log(expVal);
        _sliderValue = expVal;
        Debug.Log(_sliderValue);
        xSlider = GameObject.FindGameObjectWithTag("slider").GetComponent<Slider>();
        xSlider.value = (float)_sliderValue;
    }

    private void Update()
    {
        // xSlider = GameObject.FindGameObjectWithTag("slider").GetComponent<Slider>();
        // xSlider.value = (float)_sliderValue;
        // Debug.Log(_sliderValue);
    }

    public void setExperience(float experience) { _sliderValue = experience; }


}