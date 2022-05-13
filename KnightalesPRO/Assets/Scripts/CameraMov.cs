using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMov : MonoBehaviour
{
    public GameObject personaje;

    public GameObject personajeMago;

    private Vector3 posicion;

    // Start is called before the first frame update
    void Start()
    {
        if (personaje.active)
        {
            posicion = transform.position - personaje.transform.position;
        }else
        {
            posicion = transform.position - personajeMago.transform.position;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (personaje.active)
        {
            posicion = transform.position - personaje.transform.position;
        }else
        {
            posicion = transform.position - personajeMago.transform.position;

        }
    }
}