using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCM.IAV.Movimiento
{

    public class AgenteRata : ComportamientoAgente
    {
        /// <summary>
        /// Referencia al flautista (para no tener que coger su transform cada vez que cambiamos de estado de flauta)
        /// </summary>
        private GameObject flautista;
        public GameObject nuevoObjetivo;
        public MeshCollider suelo;

        private float limX;
        private float limZ;


        // Start is called before the first frame update
        void Start()
        {
            //Nos guardamos una referencia al flautista para los posibles cambios de objetivo que pueda haber
            flautista = objetivo;
            //limX = suelo.bounds.max.x;
            //limZ = suelo.bounds.max.z;
            limX = 50.0f;
            limZ = 50.0f;
            InvokeRepeating("CambiaObjetivo", 0, 5.0f);

        }

        void CambiaObjetivo()
        {
            if (objetivo != flautista)
            {
                Debug.Log("Cambio de objetivo");
                Destroy(objetivo);
            }

            Vector3 aux = new Vector3(Random.Range(-limX, limX ), 0, Random.Range(-limZ, limZ));
            objetivo = Instantiate(nuevoObjetivo, aux, Quaternion.identity);
        }

        //public override void Update()
        //{
        //    //switch (estado)
        //    //{
        //    //    case Estado.FLAUTA_ON:
        //    //        //Si suena la flauta, las ratas siguen al flautista
        //    //        Debug.Log("Sonando");

        //    //        break;
        //    //    case Estado.FLAUTA_OFF:
        //    //        //Si NO suena la flauta, las ratas huyen del flautista
        //    //       // Debug.Log("No implementado");
        //    //        Debug.Log("No sonando");


        //    //        //if (objetivo.transform.position == this.gameObject.transform.position)
        //    //        //{


        //    //        //objetivo.transform.position= new Vector3(Random.Range(transform.position.magnitude - 500.0f, transform.position.magnitude + 500.0f), 0,
        //    //        //Random.Range(transform.position.magnitude - 500.0f, transform.position.magnitude + 500.0f));

        //    //        //Debug.Log("Nuevo objetivo RATA");
        //    //        //Otra opcion
        //    //        //navAgente.destination = new Vector3(Random.Range(-1000.0f, 1000.0f), 0, Random.Range(-1000.0f, 1000.0f));
        //    //        //}
        //    //        break;
        //    //    default:
        //    //        break;
        //    //}

        //    base.Update();
        //}

        /// <summary>
        /// Gestiona la lógica de la rata en función de la flauta
        /// </summary>
        public override void TocaFlauta(Estado nuevo)
        {
            estado = nuevo;

            if (estado == Estado.FLAUTA_OFF)
            {
                Debug.Log("Dejamos de seguir al flautista");
                InvokeRepeating("CambiaObjetivo", 0, 5.0f);
            }
            else
            {
                CancelInvoke();
                objetivo = flautista;
                Debug.Log("Empieza a seguir al flautista");

            }
        }

        public override Direccion GetDireccion()
        {
            Direccion direccion = new Direccion();
            direccion.lineal = objetivo.transform.position - transform.position;
            direccion.lineal.Normalize();
            direccion.lineal = direccion.lineal * agente.aceleracionMax;
            return direccion;
        }
    }
}
