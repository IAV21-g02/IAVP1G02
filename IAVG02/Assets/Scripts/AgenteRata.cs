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

        private Agente ag;
        private float limX;
        private float limZ;
        private float radius;
        private Material mat;
        private float radiusPlayer;

        // Start is called before the first frame update
        void Start()
        {
            //Nos guardamos una referencia al flautista para los posibles cambios de objetivo que pueda haber
            objetivo = GameObject.FindGameObjectWithTag("Player");
            flautista = objetivo;


            Vector3 suelo = GameObject.Find("Suelo").GetComponent<MeshCollider>().bounds.max;
            limX = suelo.x;
            limZ = suelo.z;

            ag = gameObject.GetComponentInChildren<Agente>();

            radius = Random.Range(5, 11);
            gameObject.GetComponentInChildren<SphereCollider>().radius = radius;
            mat = gameObject.GetComponentInChildren<Renderer>().material;

            //avisamos de nuestra existencia a la flauta
            Flauta aux = flautista.GetComponent<Flauta>();
            radiusPlayer = flautista.GetComponent<SphereCollider>().radius;

            if (aux != null)
            {
                aux.InsertaAgente(this);
                estado = aux.GetEstado();
            }

            InvokeRepeating("CambiaObjetivo", 0, 5.0f);
        }

        private void OnTriggerExit(Collider other)
        {
            //Si la flauta esta activa y entra en contacto con el jugador
            if (estado == Estado.FLAUTA_ON && other.gameObject.GetComponent<JugadorAgente>())
            {
                TocaFlauta(Estado.FLAUTA_OFF);
            }
        }

        void CambiaObjetivo()
        {
            if (objetivo != flautista)
            {
                Destroy(objetivo);
            }

            Vector3 aux = new Vector3(Random.Range(-limX, limX), 0, Random.Range(-limZ, limZ));
            objetivo = Instantiate(nuevoObjetivo, aux, Quaternion.identity);
        }

        public override void Update()
        {
            if (estado == Estado.FLAUTA_ON && objetivo == flautista &&
                (transform.position - flautista.transform.position).magnitude < radius / 5)
            {
                ag.velocidad = Vector3.zero;
            }

            base.Update();
        }
        /// <summary>
        /// Gestiona la lógica de la rata en función de la flauta
        /// </summary>
        public override void TocaFlauta(Estado nuevo)
        {
            estado = nuevo;

            if (estado == Estado.FLAUTA_OFF)
            {
                mat.SetColor("_Color", Color.blue);
                InvokeRepeating(nameof(CambiaObjetivo), 0, 5.0f);
            }
            else if ((transform.position - flautista.transform.position).magnitude < radius + radiusPlayer)
            {
                CancelInvoke();
                mat.SetColor("_Color", Color.green);
                if (objetivo != flautista) Destroy(objetivo);
                objetivo = flautista;
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
