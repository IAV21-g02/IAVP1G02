using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCM.IAV.Movimiento
{
    public class AgentePerro : ComportamientoAgente
    {
        private GameObject flautista;
        private Material mat;

        private void Start()
        {
            objetivo = GameObject.FindGameObjectWithTag("Player");
            flautista = objetivo;
            //avisamos de nuestra existencia a la flauta
            Flauta aux = flautista.GetComponent<Flauta>();
            mat = gameObject.GetComponentInChildren<Renderer>().material;
            if (aux != null)
            {
                aux.InsertaAgente(this);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (estado == Estado.FLAUTA_ON && (other.gameObject.GetComponent<JugadorAgente>() ||
                other.gameObject.GetComponent<AgenteRata>())) CalculaLejano();
        }

        /// <summary>
        /// Gestiona la lógica del perro cuando en función de la flauta
        /// </summary>
        public override void TocaFlauta(Estado nuevo)
        {
            estado = nuevo;

            //Lógica del perro
            switch (estado)
            {
                case Estado.FLAUTA_OFF:
                    //Sigue hacia el flautista
                    mat.SetColor("_Color", Color.red);
                    objetivo = flautista;
                    break;
                case Estado.FLAUTA_ON:
                    CalculaLejano();
                    mat.SetColor("_Color", Color.Lerp(Color.red, Color.blue, 0.2f));
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Calcula el punto más lejano del jugador
        /// </summary>
        private void CalculaLejano()
        {
            //Lista de los muros del escenario
            GameObject[] muros = GameObject.FindGameObjectsWithTag("Pared");
            //Valor del muro más lejano
            float maxDir = 0;
            //Auxiliar de la posición del objetivo acutal
            Vector3 aux = flautista.transform.position;
            //Busca el muro más lejano para que el perro huya
            Vector3 objPos = Vector3.zero;
            GameObject nuevoObjetivo = null;
            foreach (GameObject muro in muros)
            {
                if ((aux - muro.transform.position).magnitude > maxDir)
                {
                    maxDir = (aux - muro.transform.position).magnitude;
                    objPos = muro.transform.position;
                    nuevoObjetivo = muro;
                }
            }
            objetivo = nuevoObjetivo;

        }

        public override Direccion GetDireccion()
        {
            Direccion direccion = new Direccion();
            direccion.lineal = objetivo.transform.position - transform.position;
            direccion.lineal.y = 0;
            direccion.lineal.Normalize();
            direccion.lineal = direccion.lineal * agente.aceleracionMax;
            return direccion;
        }
    }
}
