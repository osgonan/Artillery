using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuSlider : MonoBehaviour
{
    [SerializeField] private Slider sliderModificadorFuerza;  
    [SerializeField] private Canon canon;  
    [SerializeField] private float displayTime = 2f;  

    private float timer = 0f;

    private void Start()
    {
        // Configura el slider
        if (sliderModificadorFuerza != null && canon != null)
        {
            sliderModificadorFuerza.minValue = canon.velocidadMinimaFuerza;
            sliderModificadorFuerza.maxValue = canon.velocidadMaximaFuerza;
            sliderModificadorFuerza.gameObject.SetActive(false); 
        }
    }

    public void ActualizarSlider()
    {
        
        if (sliderModificadorFuerza != null && canon != null)
        {
            // Actualiza el valor del slider
            sliderModificadorFuerza.value = canon.getModificadorFuerza();

            // Muestra el slider y reinicia el temporizador
            sliderModificadorFuerza.gameObject.SetActive(true);
            timer = displayTime;
        }
    }

    private void Update()
    {
        // Oculta el slider después de cierto tiempo sin interacción
        if (sliderModificadorFuerza != null && timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                sliderModificadorFuerza.gameObject.SetActive(false);
            }
        }
    }
}
