using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UCM.IAV.Movimiento;

public class Impulso : MonoBehaviour
{
    public float impulseForce;
    private Vector3 impulseDir;

    private void OnTriggerEnter(Collider other)
    {
        //Le otorga un impulso al perro para que no se quede
        //atascado en las esquinas
        AgentePerro perro = other.GetComponent<AgentePerro>();
        if (perro && perro.getEstado() == Estado.FLAUTA_ON) {
            impulseDir = (-transform.position).normalized;
            other.GetComponent<Rigidbody>().AddForce(impulseDir * impulseForce, ForceMode.Impulse);
        }
    }
}
