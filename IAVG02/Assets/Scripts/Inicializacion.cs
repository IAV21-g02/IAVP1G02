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

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        Instantiate(perroPrefab, new Vector3(5, 0, 0), Quaternion.identity);
        InvokeRepeating("CreaRata", 0.5f, timeToCreateRat);

        Vector3 suelo = GameObject.Find("Suelo").GetComponent<MeshCollider>().bounds.max;
        limX = suelo.x - suelo.x / 10;
        limZ = suelo.z - suelo.z / 10;
        ThisIsNotHaltonButWorks();
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

    private void ThisIsNotHaltonButWorks (){

        for (int i = 0; i < numObst; i++) {
            float posX = Random.Range(-limX, limX);
            float posZ = Random.Range(-limZ, limZ);

            GameObject objeto = Instantiate(obstaculo, new Vector3(posX, 0, posZ), Quaternion.identity, transform);
            float sizeX = Random.Range(0.5f, 2.5f);
            float sizeZ = Random.Range(0.5f, 2.5f);
            objeto.transform.localScale = new Vector3(sizeX, 3, sizeZ);
        }
    }
}
