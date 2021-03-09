using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UCM.IAV.Movimiento;

public class Repulsion : MonoBehaviour
{
    public float force;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        AgenteRata rata = other.gameObject.GetComponent<AgenteRata>();
        if(rata != null && rata.getEstado() == Estado.FLAUTA_OFF)
        {
            Vector3 dir = this.gameObject.transform.position - other.gameObject.transform.position;
            dir = dir.normalized;
            rb.AddForce(dir * force, ForceMode.Impulse);
        }
    }
}

