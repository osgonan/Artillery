using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Objetivo : MonoBehaviour
{

    public UnityEvent gameWon;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Explosion") {
            gameWon.Invoke();
            Destroy(this.gameObject);
        }
    }
}
