using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthManager : MonoBehaviour
{
    private Slider slider;
    private float _sliderValue;
    // Start is called before the first frame update
    void Start()
    {
        slider = GameObject.FindGameObjectWithTag("slider").GetComponent<Slider>();
        slider.value = _sliderValue;
    }

    public void healthUpdate(float healthVal)
    {
        _sliderValue = healthVal;
        Debug.Log(_sliderValue);
        slider.value = _sliderValue;
    }

    private void Update()
    {
        
        
    }

    public void setHealth(float health) { _sliderValue = health; }
    

}