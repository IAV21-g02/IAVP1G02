using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCM.IAV.Movimiento
{
    public class AgentePerro : ComportamientoAgente
    {
        private GameObject flautista;

        private void Start()
        {
            flautista = objetivo;
        }

        /// <summary>
        /// En cada tick, mover el avatar del jugador según las órdenes de este último
        /// </summary>
        public override void Update()
        {
            //Huye del flautista
            switch (estado)
            {
                case Estado.FLAUTA_ON:
                    //Huye del flautista
                    CalculaLejano();
                    break;
                default:
                    break;
            }

            base.Update();
        }

        /// <summary>
        /// Gestiona la lógica del perro cuando en función de la flauta
        /// </summary>
        public override void TocaFlauta(Estado nuevo)
        {
            estado = nuevo;
            print("Cambio de objetivo");

            //Lógica del perro
            switch (estado)
            {
                case Estado.FLAUTA_OFF:
                    //Sigue hacia el flautista
                    objetivo = GameObject.Find("Flautista");
                    break;
                default:
                    break;
            }
        }

        private void CalculaLejano()
        {
            //Lista de los muros del escenario
            GameObject[] muros = GameObject.FindGameObjectsWithTag("Pared");
            //Valor del muro más lejano
            float maxDir = 0;
            //Auxiliar de la posición del objetivo acutal
            Vector3 aux = flautista.transform.position;
            //Busca el muro más lejano para que el perro huya
            foreach (GameObject muro in muros)
            {
                if ((aux - muro.transform.position).magnitude > maxDir)
                {
                    maxDir = (aux - muro.transform.position).magnitude;
                    objetivo = muro;
                }
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
