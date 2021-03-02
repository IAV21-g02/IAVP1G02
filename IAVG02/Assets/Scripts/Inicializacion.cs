using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inicializacion : MonoBehaviour
{
   public GameObject playerPrefab;
    public GameObject perroPrefab;
    public GameObject rataPrefab;
    public GameObject rataParent;
    public int maxRatas;
    public float timeToCreateRat;
    private int numrats = 0;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        Instantiate(perroPrefab, new Vector3(5, 0, 0), Quaternion.identity);
        InvokeRepeating("CreaRata", 0.5f, timeToCreateRat);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void CreaRata()
    {
        Instantiate(rataPrefab, rataParent.transform.position, Quaternion.identity,rataParent.transform);
        numrats++;
        if(numrats >= maxRatas)
        {
            CancelInvoke("CreaRata");
        }
    }

   

}
