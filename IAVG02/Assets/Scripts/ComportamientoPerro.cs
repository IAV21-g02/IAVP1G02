using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCM.IAV.Movimiento
{
    /// <summary>
    /// Clase que gestiona el comportamiento del perro
    /// respecto a la flauta
    /// </summary>
    public class ComportamientoPerro : ComportamientoAgente
    {
        /// <summary>
        /// Referencia al flautista
        /// </summary>
        private GameObject flautista;
        /// <summary>
        /// Referencia al material del GO
        /// </summary>
        private Material mat;

        private AudioSource audioSource;
        public AudioClip ladridoSonido;
        public AudioClip lloroSonido;

        private void Start()
        {
            //Se registra el flautista como primer objetivo
            objetivo = GameObject.FindGameObjectWithTag("Player");
            flautista = objetivo;

            audioSource = GetComponent<AudioSource>();
            audioSource.volume = 0.25f;

            mat = gameObject.GetComponentInChildren<Renderer>().material;

            //avisamos de nuestra existencia a la flauta
            Flauta aux = flautista.GetComponent<Flauta>();
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

            //Gestión de colores en función de si toca 
            //o no toca la flauta
            switch (estado)
            {
                case Estado.FLAUTA_OFF:
                    audioSource.clip = ladridoSonido;
                    mat.SetColor("_Color", Color.red);
                    break;
                case Estado.FLAUTA_ON:
                    audioSource.clip = lloroSonido;
                    mat.SetColor("_Color", Color.Lerp(Color.red, Color.yellow, 0.5f));
                    break;
                default:
                    break;
            }
            audioSource.Play();
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
