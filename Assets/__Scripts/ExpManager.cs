using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ExpManager : MonoBehaviour
{
    public Slider xSlider;
    public float _sliderValue;

    // Start is called before the first frame update
    void Start()
    {
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
      
    }

    public void setExp(float exp) { _sliderValue = exp; }


}