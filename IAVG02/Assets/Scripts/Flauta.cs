using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UCM.IAV.Movimiento;

public class Flauta : MonoBehaviour
{
    /// <summary>
    /// Lista de los agentes externos al jugador
    /// </summary>
    private List<ComportamientoAgente> agentes;
    /// <summary>
    /// Estado actual de la flauta
    /// </summary>
    private Estado estado;

    /// <summary>
    /// Referencia al componente material 
    /// del GO
    /// </summary>
    Material mat;

    /// <summary>
    /// Devuelve el estado actual de la flauta
    /// </summary>
    /// <returns></returns>
    public Estado GetEstado() {
        return estado;
    }

    private void Start()
    {
        estado = Estado.FLAUTA_OFF;
        //Se buscan todos los objetos con el componente Agente o heredados de Agente
        mat = gameObject.GetComponentInChildren<Renderer>().material;
        agentes = new List<ComportamientoAgente>();
    }

    /// <summary>
    /// Cambia el estado de la flauta y avisa 
    /// a los agentes del estado de ésta
    /// </summary>
    public void TocarFlauta() {
        //Cambia el estado de la flauta y
        //el tamaño del collider (sonido) de la flauta
        switch (estado) {
            //Cuando la flauta no suena, es pequeño
            case Estado.FLAUTA_OFF:
                estado = Estado.FLAUTA_ON;
                break;
            //Cuando la flauta suena, es grande
            case Estado.FLAUTA_ON:
                estado = Estado.FLAUTA_OFF;
                break;
            default:
                break;
        }

        foreach (ComportamientoAgente ag in agentes)
        {
            ag.TocaFlauta(estado);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Cuando la flauta suene y cuando la otra entidad sea una rata
        if (estado == Estado.FLAUTA_ON && other.GetComponent<ComportamientoRata>()) {
            other.GetComponent<ComportamientoRata>().TocaFlauta(estado);
        }
    }

    public void Update()
    {
        //Se encarga de cambiar el estado de la flauta
        //Se toca la flauta mientras la tecla espacio este pulsada
        if (Input.GetKeyDown(KeyCode.Space)) {
           mat.SetColor("_Color", Color.black);
            TocarFlauta();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            mat.SetColor("_Color", Color.yellow);
            TocarFlauta();
        }
    }

    /// <summary>
    /// Inserta al agente a la lista de agentes
    /// para que puedan ser avisados de cualquier evento
    /// </summary>
    /// <param name="agente"></param>
    public void InsertaAgente(ComportamientoAgente agente)
    {
        if (agente != null)
        {
            agentes.Add(agente);
        }
    }
}
