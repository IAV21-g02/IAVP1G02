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

        // Start is called before the first frame update
        void Start()
        {
            //Nos guardamos una referencia al flautista para los posibles cambios de objetivo que pueda haber
            flautista = objetivo;
        }

        public override void Update()
        {
            switch (estado)
            {
                case Estado.FLAUTA_ON:
                    //Si suena la flauta, las ratas siguen al flautista
                    Debug.Log("Sonando");
                    navAgente.destination = flautista.transform.position;
                    break;
                case Estado.FLAUTA_OFF:
                    //Si NO suena la flauta, las ratas huyen del flautista
                   // Debug.Log("No implementado");
                    Debug.Log("No sonando");

                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Gestiona la lógica de la rata en función de la flauta
        /// </summary>
        public override void TocaFlauta(Estado nuevo)
        {
            estado = nuevo;
            print("Cambio de objetivo (Rata):  " + nuevo.ToString());
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
