using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dusmanOk : MonoBehaviour
{
    public float hasar;
    canKontrol canKontrol;
    // Start is called before the first frame update
    void Start()
    {
        canKontrol=FindObjectOfType<canKontrol>();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("oyuncu"))
        {
            canKontrol.canAzalmasi(hasar);
            Destroy(gameObject);
        }
    }
}
