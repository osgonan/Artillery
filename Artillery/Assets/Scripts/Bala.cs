using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public GameObject particulaExplosion;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Suelo") {
            Invoke("Explotar", 3);
        }
        if (collision.gameObject.tag == "Obstaculo" || collision.gameObject.tag == "Objetivo") Explotar();
    }

    public void Explotar() { 
    
        GameObject particula = Instantiate(particulaExplosion,transform.position,Quaternion.identity) as GameObject;
        Canon.bloqueado = false;
        Destroy(particula, 2f);
        Destroy(this.gameObject);
    }
}
