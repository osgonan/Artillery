using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AdminHUD : MonoBehaviour
{
    public GameObject MenuSlider;


    public void modificarSlider(float modificador) { 
        Slider slider  = MenuSlider.GetComponent<Slider>();
        slider.value += modificador;
    }
}
