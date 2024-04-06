using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    public static bool bloqueado;

    [SerializeField] private GameObject balaPrefab;
    private GameObject puntoCanon;
    private float _rotacion;
    private int _balasDisparadas = 0;
    private GameObject adminJuego;
    public GameObject particulaDisparo;
    public AudioClip clipDisparo;
    private GameObject sonidoDisparo;
    private AudioSource sourceDisparo;
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
        rotacion += Input.GetAxis("Horizontal")*AdministradorJuego.VELOCIDADROTACION;
        if (rotacion <= 90 && rotacion>=0) {
            transform.eulerAngles = new Vector3(rotacion,90,0.0f);
        }
        if (rotacion > 90) rotacion = 90;
        if(rotacion <0) rotacion = 0;

        if (Input.GetKeyDown(KeyCode.Space) && !bloqueado) {
            if (balasDisparadas < AdministradorJuego.DISPAROPORJUEGO)
            {
                GameObject tmp = Instantiate(balaPrefab, puntoCanon.transform.position, transform.rotation);
                Rigidbody tmpRB = tmp.GetComponent<Rigidbody>();
                SeguirCamara.objetivo = tmp;
                Vector3 direccionDisparo = transform.rotation.eulerAngles;
                Vector3 direccionParticulas = new Vector3(-90 + direccionDisparo.x, 90, 0);
                GameObject particula = Instantiate(particulaDisparo, puntoCanon.transform.position,Quaternion.Euler(direccionParticulas),transform);
                direccionDisparo.y = 90 - direccionDisparo.x;
                tmpRB.velocity = direccionDisparo.normalized * AdministradorJuego.VELOCIDADBALA;
                sourceDisparo.PlayOneShot(clipDisparo);
                balasDisparadas += 1;
                bloqueado = true;
            }
        }

    }
}
