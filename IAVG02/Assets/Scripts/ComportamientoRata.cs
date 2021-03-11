using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCM.IAV.Movimiento
{
    /// <summary>
    /// Clase que gestiona el comportamiento de la rata
    /// respecto a la flauta
    /// </summary>
    public class ComportamientoRata : ComportamientoAgente
    {
        //PUBLIC
        /// <summary>
        /// Radio a partir del cual se frenará
        /// con respecto al flautista
        /// </summary>
        [Tooltip("Radio a partir del cual se frenará con respecto al flautista")]
        public float radiusStop;
        /// <summary>
        /// Nuevo objetivo de la rata cuando está
        /// merodeando
        /// </summary>
        [Tooltip("Nuevo objetivo de la rata cuando está merodeando")]
        public GameObject nuevoObjetivo;


        //PRIVATE
        /// <summary>
        /// Pool de los objetivos de las ratas
        /// </summary>
        private Transform objetivoPool;
        /// <summary>
        /// Referencia al Collider del suelo
        /// </summary>
        private MeshCollider suelo;
        /// <summary>
        /// Referencia al script Agente de la rata
        /// </summary>
        private Agente ag;
        /// <summary>
        /// Limite horizontal del escenario
        /// </summary>
        private float limX;
        /// <summary>
        /// Límite vertical del escenario
        /// </summary>
        private float limZ;
        /// <summary>
        /// Radio de escucha de la rata
        /// </summary>
        private float radiusEar;
        /// <summary>
        /// Referencia al material de la rata
        /// </summary>
        private Material mat;
        /// <summary>
        /// Referencia al flautista
        /// </summary>
        private GameObject flautista;
        /// <summary>
        /// Referencia al radio del flautista
        /// </summary>
        private float radiusPlayer;

        void Start()
        {
            //Nos guardamos una referencia al flautista para los
            //posibles cambios de objetivo que pueda haber
            objetivo = GameObject.FindGameObjectWithTag("Player");
            flautista = objetivo;

            //Buscamos el suelo y guardamos los límites de Hamelin
            Vector3 suelo = GameObject.Find("Suelo").GetComponent<MeshCollider>().bounds.max;
            limX = suelo.x;
            limZ = suelo.z;

            //Se genera el radio de escucha aleatorio de la rata
            radiusEar = Random.Range(5, 11);
            gameObject.GetComponentInChildren<SphereCollider>().radius = radiusEar;

            
            objetivoPool = GameObject.Find("ObjetivoRatas").transform;
            ag = gameObject.GetComponent<Agente>();
            radiusPlayer = flautista.GetComponent<SphereCollider>().radius;
            mat = gameObject.GetComponentInChildren<Renderer>().material;

            //avisamos de nuestra existencia a la flauta
            Flauta aux = flautista.GetComponent<Flauta>();
            if (aux != null)
            {
                aux.InsertaAgente(this);
                estado = aux.GetEstado();
            }

            InvokeRepeating(nameof(CambiaObjetivo), 0, 5.0f);
        }

        private void OnTriggerExit(Collider other)
        {
            //Si la flauta esta activa y sale del contacto con el jugador
            if (estado == Estado.FLAUTA_ON && other.gameObject.GetComponent<JugadorAgente>())
            {
                TocaFlauta(Estado.FLAUTA_OFF);
            }
        }

        /// <summary>
        /// Cambia el objeto de la rata en función
        ///  de si la flauta suena o no
        /// </summary>
        void CambiaObjetivo()
        {
            //Si el objetivo anterior no era el flautista se destruye
            if (objetivo != flautista)
            {
                Destroy(objetivo);
            }

            //Se genera un nuevo objetivo para que la rata merodee
            Vector3 nuevaPos = new Vector3(Random.Range(-limX, limX), 0, Random.Range(-limZ, limZ));
            objetivo = Instantiate(nuevoObjetivo, nuevaPos, Quaternion.identity, objetivoPool);
        }

        public override void Update()
        {
            //Controla que la rata no se pegue demasiado al jugador
            if (estado == Estado.FLAUTA_ON && objetivo == flautista &&
                (transform.position - flautista.transform.position).magnitude < radiusStop)
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

            //Cuando la flauta no suena, comienza a merodear
            //y se cambia a color azul
            if (estado == Estado.FLAUTA_OFF)
            {
                mat.SetColor("_Color", Color.blue);
                InvokeRepeating(nameof(CambiaObjetivo), 0, 5.0f);
            }
            //Si la flauta suena, entonces se comprueba si la rata está
            //ya dentro del radio del player (porque si lo está entonces no se aplica
            //el método OnTriggerEnter(...)) y se cambia a color verde y sigue al flautista
            else if ((transform.position - flautista.transform.position).magnitude < radiusEar + radiusPlayer)
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
