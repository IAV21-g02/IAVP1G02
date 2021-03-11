using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UCM.IAV.Movimiento;

public class Repulsion : MonoBehaviour
{
    /// <summary>
    /// Fuerza de la repulsión
    /// </summary>
    [Tooltip("Fuerza de la repulsión")]
    public float force;

    /// <summary>
    /// Referencia al componente 
    /// Rigidbody del GO
    /// </summary>
    private Rigidbody rb;

    void Start()
    {
        rb = this.gameObject.GetComponentInParent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Repulsion rata = other.gameObject.GetComponent<Repulsion>();
        if(rata != null)
        {
            //Calcula el vector director de la repulsión
            Vector3 dir = other.gameObject.transform.position - this.gameObject.transform.position;
            dir = dir.normalized;
            rb.AddForce(dir * force, ForceMode.Impulse);
        }
    }
}

