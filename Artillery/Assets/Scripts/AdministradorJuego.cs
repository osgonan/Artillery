using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdministradorJuego : MonoBehaviour
{

    private static AdministradorJuego _SINGLETONADMINISTRADORJUEGO;
    private static int _VELOCIDADBALA = 30;
    private static int _DISPAROPORJUEGO = 10;
    private static float _VELOCIDADROTACION = 1;

    public static AdministradorJuego SINGLETONADMINISTRADORJUEGO {
        get => _SINGLETONADMINISTRADORJUEGO;
        set => _SINGLETONADMINISTRADORJUEGO = value;
    }

    public static int VELOCIDADBALA { 
        get=> _VELOCIDADBALA;
        set=> _VELOCIDADBALA = value;
    }

    public static int DISPAROPORJUEGO { 
        get=> _DISPAROPORJUEGO; set => _DISPAROPORJUEGO = value;
    }

    public static float VELOCIDADROTACION {
        get => _VELOCIDADROTACION; set => _VELOCIDADROTACION = value;   
    }
    private void Awake()
    {
        if (SINGLETONADMINISTRADORJUEGO == null)
        {
            SINGLETONADMINISTRADORJUEGO = this;
        }
        else {
            Debug.LogError("Ya existe una instancia de la clase");

        }
    }
}
