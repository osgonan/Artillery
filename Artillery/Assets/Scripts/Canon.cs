using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    [SerializeField] private GameObject balaPrefab;
    private GameObject puntoCanon;
    private float _rotacion;
    private int _balasDisparadas = 0;
    private GameObject adminJuego;


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

        if (Input.GetKeyDown(KeyCode.Space)) {
            if (balasDisparadas < AdministradorJuego.DISPAROPORJUEGO)
            {
                GameObject tmp = Instantiate(balaPrefab, puntoCanon.transform.position, transform.rotation);
                Rigidbody tmpRB = tmp.GetComponent<Rigidbody>();
                Vector3 direccionDisparo = transform.rotation.eulerAngles;
                direccionDisparo.y = 90 - direccionDisparo.x;
                tmpRB.velocity = direccionDisparo.normalized * AdministradorJuego.VELOCIDADBALA;
                balasDisparadas += 1;
            }
        }

    }
}
