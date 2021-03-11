using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase que gestiona la inicialización del mundo
/// </summary>
public class Inicializacion : MonoBehaviour
{
    //PUBLIC
    // bases de numeros coprimos para el metodo de halton
    // coprimos => maximo comun divisor entre baseX y baseY sea =1
    /// <summary>
    /// Coprimo Halton X
    /// </summary>
    [Tooltip("Coprimo Halton X")]
    public float baseX = 2;
    /// <summary>
    /// Coprimo Halton Y
    /// </summary>
    [Tooltip("Coprimo Halton Y")]
    public float baseY = 3;
    /// <summary>
    /// Referencia al flautista
    /// </summary>
    [Tooltip("Referencia al flautista")]
    public GameObject playerPrefab;
    /// <summary>
    /// Referencia al perro
    /// </summary>
    [Tooltip("Referencia al perro")]
    public GameObject perroPrefab;
    /// <summary>
    /// Referencia a la rata
    /// </summary>
    [Tooltip("Referencia a la rata")]
    public GameObject rataPrefab;
    /// <summary>
    /// Referencia al pool de ratas
    /// </summary>
    [Tooltip("Referencia al pool de ratas")]
    public GameObject rataParent;
    /// <summary>
    /// Referencia a los obstaculos
    /// </summary>
    [Tooltip("Referencia a los obstaculos a crear")]
    public GameObject obstaculo;
    /// <summary>
    /// Numero maximo de ratas que van a generar
    /// </summary>
    [Tooltip("Numero maximo de ratas que van a generar")]
    public int maxRatas;
    /// <summary>
    /// Numero maximo de obstaculos que van a generar
    /// </summary>
    [Tooltip("Numero maximo de obstaculos que van a generar")]
    public int numObst;
    /// <summary>
    /// Tiempo de creacion entre cada rata
    /// </summary>
    [Tooltip("Tiempo de creación entre cada rata")]
    public float timeToCreateRat;

    //PRIVATE
    /// <summary>
    /// Número de ratas actual
    /// </summary>
    private int numrats = 0;
    /// <summary>
    /// Límite horizontal del mundo
    /// </summary>
    private float limX;
    /// <summary>
    /// Límite vertical del mundo
    /// </summary>
    private float limZ;

    void Start()
    {
        //Se genera el player
        Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        //Se genera el perro
        Instantiate(perroPrefab, new Vector3(5, 0, 0), Quaternion.identity);
        //Se generan las ratas
        InvokeRepeating(nameof(CreaRata), 0.5f, timeToCreateRat);

        Vector3 suelo = GameObject.Find("Suelo").GetComponent<MeshCollider>().bounds.max;
        limX = suelo.x - suelo.x / 10;
        limZ = suelo.z - suelo.z / 10;
        for(int i = 0; i < numObst; i++)
        {
            Halton2d(baseX, baseY, i + 1);
        }
    }

    /// <summary>
    /// Método que crea una rata
    /// </summary>
    private void CreaRata()
    {
        if (numrats >= maxRatas)
        {
            CancelInvoke("CreaRata");
        }
        else
        {
            Instantiate(rataPrefab, rataParent.transform.position, Quaternion.identity, rataParent.transform);
            numrats++;
        }
    }

    /// <summary>
    /// Metodo que dada una base y el indice del objeto
    /// en la secuencia de halton devuelve su valor correspondiente entr 0 y 1
    /// </summary>
    private float Halton(float b, float index)
    {
        float result = 0;
        float denominator = 1;

        while(index > 0)
        {
            denominator *= b;
            result += (index % b) / denominator;
            index = Mathf.Floor(index / b);
            
        }
        return result;
    }

    /// <summary>
    /// Generador del mapa de Halton
    /// </summary>
    private void Halton2d(float baseX, float baseY, float index)
    {
        float x = Halton(baseX, index);
        float z = Halton(baseY, index);
        //Ajuste para pasar del rango [0,1] a las coordenadas reales del suelo
        float posX = -limX + (x * limX*2);
        float posZ = -limZ + (z * limZ*2);
        GameObject objeto = Instantiate(obstaculo, new Vector3(posX, 0, posZ), Quaternion.identity, transform);
        //tamaño random del objeto
        float sizeX = Random.Range(0.5f, 2.5f);
        float sizeZ = Random.Range(0.5f, 2.5f);
        objeto.transform.localScale = new Vector3(sizeX, 3, sizeZ);
    }

}
