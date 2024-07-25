using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yolAcici : MonoBehaviour
{
    Kamera kamera;
    killSayaci killSayaci;

    public int yeterliKillSayisi,acilmaSayisi;
    public bool sandikActi;

    public GameObject engel;

    // Start is called before the first frame update
    void Start()
    {
        kamera=FindObjectOfType<Kamera>();
        killSayaci=FindObjectOfType<killSayaci>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Home))
        {
            killSayaci.oldurmeSayisi= yeterliKillSayisi;
            sandikActi=true;

            if (acilmaSayisi == 0)
            {

                engel.transform.position = new Vector2(engel.transform.position.x + 90, transform.position.y);
                kamera.maxX = kamera.maxX + 90;
                sandikActi = false;
            }

            if (acilmaSayisi==1)
            {
                engel.transform.position = new Vector2(engel.transform.position.x + 120, transform.position.y);
                kamera.maxX = kamera.maxX + 120;
                sandikActi = false;
            }

            acilmaSayisi++;

        }
        if (sandikActi&&killSayaci.oldurmeSayisi>=yeterliKillSayisi)
        {
            Debug.Log("Lanet Kalktı");
        }
    }
}
