using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UCM.IAV.Movimiento;

public class Flauta : MonoBehaviour
{
    // private ComportamientoAgente[] agentes;
    private List<ComportamientoAgente> agentes;
    private Estado estado;

    private void Start()
    {
        Debug.Log("Start flauta");
        estado = Estado.FLAUTA_OFF;
        //Se buscan todos los objetos con el componente Agente o heredados de Agente
        //ComportamientoAgente[] aux = GameObject.FindObjectsOfType<ComportamientoAgente>();
        //foreach(ComportamientoAgente ag in aux)
        //{
        //    agentes.Add(ag);
        //}
        agentes = new List<ComportamientoAgente>();
        
    }

    /// <summary>
    /// Cambia el estado de la flauta y avisa 
    /// a los agentes del estado de ésta
    /// </summary>
    public void TocarFlauta() {
        //Cambia el estado de la flauta
        switch (estado) {
            case Estado.FLAUTA_OFF:
                estado = Estado.FLAUTA_ON;
                break;
            case Estado.FLAUTA_ON:
                estado = Estado.FLAUTA_OFF;
                break;
            default:
                break;
        }

        //Busca a todos los agentes y les avisa del estado de la flauta
        foreach (ComportamientoAgente ag in agentes) {
            ag.TocaFlauta(estado);
        }
    }

    public void Update()
    {
        //Se encarga de cambiar el estado de la flauta
        //Se toca la flauta mientras la tecla espacio este pulsada
        if (Input.GetKeyDown(KeyCode.Space)) {
            TocarFlauta();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            TocarFlauta();
        }
        
    }
    public void AnadeAgente(ComportamientoAgente agente)
    {
        if (agente != null)
        {
             Debug.Log("flauta entro en if");
            agentes.Add(agente);

        }
        else Debug.Log("fallo");

    }
  
}
