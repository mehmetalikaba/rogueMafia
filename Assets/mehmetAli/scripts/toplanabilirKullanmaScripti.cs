using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toplanabilirKullanmaScripti : MonoBehaviour
{

    public float toplanabilirEtkiSuresi;

    public bool buObjeCan, buObjeDayaniklilik, buObjeHasar, buObjeHareketHizi;

    toplanabilirOzellikleri toplanabilirOzellikleri;




    void Start()
    {
        
    }

    void Update()
    {
        if (buObjeCan)
        {

        }
        if (buObjeDayaniklilik)
        {

        }
        if (buObjeHasar)
        {

        }
        if (buObjeHareketHizi)
        {

        }
    }


    IEnumerator toplanabilirEtkiSuresiBasladi()
    {
        yield return new WaitForSeconds(toplanabilirEtkiSuresi);
    }
}
