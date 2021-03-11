using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UCM.IAV.Movimiento;

/// <summary>
/// Clase que gestiona el impulso que se le da a un GO
/// cuando entra en su trigger. Su objetivo principal
/// es que el perro no se quede atorado en las esquinas
/// </summary>
public class Impulso : MonoBehaviour
{
    /// <summary>
    /// Fuerza de impulso aplicado
    /// </summary>
    [Tooltip("Fuerza de impulso aplicado")]
    public float impulseForce;
    /// <summary>
    /// Referencia al GO con el que hace diagonal
    /// </summary>
    [Tooltip("Referencia al GO con el que hace diagonal")]
    public Transform diagonal;

    /// <summary>
    /// Dirección de impulso
    /// </summary>
    private Vector3 impulseDir;

    private void Start()
    {
        impulseDir = (diagonal.position - transform.position).normalized;
    }

    private void OnTriggerEnter(Collider other)
    {
        ComportamientoPerro perro = other.GetComponent<ComportamientoPerro>();
        if (perro && perro.getEstado() == Estado.FLAUTA_ON)
            other.GetComponent<Rigidbody>().AddForce(impulseDir * impulseForce, ForceMode.Impulse);
    }
}
