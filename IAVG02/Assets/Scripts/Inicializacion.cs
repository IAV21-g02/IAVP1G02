using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inicializacion : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject perroPrefab;
    public GameObject rataPrefab;
    public GameObject rataParent;
    public GameObject obstaculo;
    public int maxRatas;
    public int numObst;
    
    public float timeToCreateRat;
    private int numrats = 0;
    private float limX;
    private float limZ;
    // bases de numeros coprimos para el metodo de halton
    // coprimos => maximo comun divisor entre baseX y baseY sea =1
    private const float baseX = 2;
    private const float baseY = 3;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        Instantiate(perroPrefab, new Vector3(5, 0, 0), Quaternion.identity);
        InvokeRepeating("CreaRata", 0.5f, timeToCreateRat);

        Vector3 suelo = GameObject.Find("Suelo").GetComponent<MeshCollider>().bounds.max;
        limX = suelo.x - suelo.x / 10;
        limZ = suelo.z - suelo.z / 10;
        for(int i = 0; i < numObst; i++)
        {
            Halton2d(baseX, baseY, i + 1);
        }
    }

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
    /// metodo que dada una base y el indice del objeto en la secuencia de halton devuelve su valor correspondiente entr 0 y 1
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
