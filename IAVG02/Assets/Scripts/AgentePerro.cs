using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCM.IAV.Movimiento
{
    public class AgentePerro : ComportamientoAgente
    {
        private GameObject flautista;

        public float distanciaSeguridad = 10.0f;
        private void Start()
        {
            flautista = objetivo;
        }

        /// <summary>
        /// En cada tick, mover el avatar del jugador según las órdenes de este último
        /// </summary>
        /// //TODO
        public override void Update()
        {
            if (estado == Estado.FLAUTA_ON && (transform.position - flautista.transform.position).magnitude < distanciaSeguridad)
            {
                CalculaLejano();
            }

            //base.Update();
        }

        /// <summary>
        /// Gestiona la lógica del perro cuando en función de la flauta
        /// </summary>
        public override void TocaFlauta(Estado nuevo)
        {
            estado = nuevo;
            print("Cambio de objetivo " + nuevo.ToString());

            //Lógica del perro
            switch (estado)
            {
                case Estado.FLAUTA_OFF:
                    //Sigue hacia el flautista
                    navAgente.destination = flautista.transform.position;
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
            foreach (GameObject muro in muros)
            {
                if ((aux - muro.transform.position).magnitude > maxDir)
                {
                    maxDir = (aux - muro.transform.position).magnitude;
                    objPos = muro.transform.position;
                }
            }
            navAgente.destination = objPos;

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
