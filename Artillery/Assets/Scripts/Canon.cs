using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Canon : MonoBehaviour
{
    public static bool bloqueado;
    [SerializeField] private GameObject balaPrefab;
    private GameObject puntoCanon;
    private float _rotacion;
    private int _balasDisparadas = 0;
    private float modificadorFuerza = 1;
    private GameObject adminJuego;
    public GameObject particulaDisparo;
    public AudioClip clipDisparo;
    private GameObject sonidoDisparo;
    private AudioSource sourceDisparo;
    public CanonControls canonControls;

    private InputAction apuntar;
    private InputAction modificarFuerza;
    private InputAction disparar;

    public UnityEvent administradorHUD;

    public float velocidadMinimaFuerza = 10.0f;
    public float velocidadMaximaFuerza = 60.0f;


    public void Awake() {

        canonControls = new CanonControls();

    }

    public void OnEnable()
    {
        apuntar = canonControls.Canon.Apuntar;
        modificarFuerza = canonControls.Canon.ModificarFuerza;
        disparar = canonControls.Canon.Disparar;
        modificarFuerza = canonControls.Canon.ModificarFuerza;

        apuntar.Enable();
        modificarFuerza.Enable();
        disparar.Enable();
        modificarFuerza.Enable();

        disparar.performed += Disparar;

    }

    public float rotacion {
        get => _rotacion;
        set=> _rotacion = value;
    }

    public int balasDisparadas { 
        get=> _balasDisparadas;
        set=> _balasDisparadas = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        puntoCanon = transform.Find("puntoCanon").gameObject;
        sonidoDisparo = GameObject.Find("SonidoDisparo");
        sourceDisparo = sonidoDisparo.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        rotacion += apuntar.ReadValue<float>()*AdministradorJuego.VELOCIDADROTACION;
        if (rotacion <= 90 && rotacion>=0) {
            transform.eulerAngles = new Vector3(rotacion,90,0.0f);
        }
        if (rotacion > 90) rotacion = 90;
        if(rotacion <0) rotacion = 0;
        float modificar = modificarFuerza.ReadValue<float>();
        if (Mathf.Abs(modificar) > 0.1f) // Si hay suficiente movimiento en el stick
        {
            modificadorFuerza+=modificar* AdministradorJuego.VELOCIDADBALA * Time.deltaTime;
            modificadorFuerza = Mathf.Clamp(modificadorFuerza, velocidadMinimaFuerza, velocidadMaximaFuerza); // Limita la fuerza

            administradorHUD.Invoke();
        }

    }

    private void Disparar(InputAction.CallbackContext context)
    {
        if (balasDisparadas < AdministradorJuego.DISPAROPORJUEGO && !bloqueado)
        {
            GameObject tmp = Instantiate(balaPrefab, puntoCanon.transform.position, transform.rotation);
            Rigidbody tmpRB = tmp.GetComponent<Rigidbody>();
            SeguirCamara.objetivo = tmp;
            Vector3 direccionDisparo = transform.rotation.eulerAngles;
            Vector3 direccionParticulas = new Vector3(-90 + direccionDisparo.x, 90, 0);
            GameObject particula = Instantiate(particulaDisparo, puntoCanon.transform.position, Quaternion.Euler(direccionParticulas), transform);
            direccionDisparo.y = 90 - direccionDisparo.x;
            tmpRB.velocity = direccionDisparo.normalized * modificadorFuerza;
            sourceDisparo.PlayOneShot(clipDisparo);
            balasDisparadas += 1;
            AdministradorJuego.DISPAROPORJUEGO--;
            bloqueado = true;
        }
    }

    public float getModificadorFuerza() {
        return modificadorFuerza;
    }

}
