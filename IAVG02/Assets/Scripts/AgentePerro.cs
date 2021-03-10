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
                    break;
                case Estado.FLAUTA_ON:
                    mat.SetColor("_Color", Color.Lerp(Color.red, Color.yellow, 0.5f));
                    break;
                default:
                    break;
            }
        }

        public override Direccion GetDireccion()
        {
            Direccion direccion = new Direccion();
            direccion.lineal = objetivo.transform.position - transform.position;
            direccion.lineal.y = 0;
            direccion.lineal.Normalize();
            direccion.lineal = direccion.lineal * agente.aceleracionMax;
            if (estado == Estado.FLAUTA_ON) direccion.lineal *= -1;

            return direccion;
        }
    }
}
