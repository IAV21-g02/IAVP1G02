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
        rb = this.gameObject.GetComponentInParent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Repulsion rata = other.gameObject.GetComponent<Repulsion>();
        if(rata != null)
        {
            Vector3 dir = this.gameObject.transform.position - other.gameObject.transform.position;
            dir = dir.normalized;
            rb.AddForce(dir * force, ForceMode.Impulse);
        }
    }
}

