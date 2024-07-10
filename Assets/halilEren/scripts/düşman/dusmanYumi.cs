using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dusmanYumi : MonoBehaviour
{
    Animator animator;
    GameObject oyuncu;

    bool okFirlat, geriKac,yaklas;
    public GameObject solaOk, sagaOk;

    public float hareketHizi;
    float okTimer;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        oyuncu = GameObject.FindGameObjectWithTag("oyuncu");
        yaklas = true;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float oyuncuyaYakinlik = Vector2.Distance(oyuncu.transform.position, transform.position);

        if (oyuncuyaYakinlik > 8)
        {
            yaklas = true;
            okFirlat = false;
            geriKac = false;
        }
        if ((oyuncuyaYakinlik<=10&&8>=oyuncuyaYakinlik)&&!geriKac)
        {
            yaklas=false;
            okFirlat = true;
        }
        if(oyuncuyaYakinlik<5)
        {
            okFirlat = false;
            geriKac = true;
        }

        Yaklas();
        OkFirlat();
        GeriKac();
    }
    void Yaklas()
    {
        if(yaklas)
        {
            okTimer = 0;
            animator.SetBool("ok", false);
            animator.SetBool("yurume", true);
            if (oyuncu.transform.position.x > transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.Translate(transform.right * hareketHizi * Time.deltaTime);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                transform.Translate(-transform.right * hareketHizi * Time.deltaTime);
            }
        }
    }
    void OkFirlat()
    {
        if(okFirlat)
        {
            okTimer += Time.deltaTime;
            if(okTimer>1f)
            {
                if(transform.rotation.y<=-1)
                {
                    Debug.Log("dfsfd");
                    Instantiate(solaOk,transform.position,solaOk.transform.rotation);
                }
                if(transform.rotation.y>=0)
                {
                    Debug.Log("gdsgsd");

                    Instantiate(sagaOk, transform.position, sagaOk.transform.rotation);
                }
                okTimer = 0;
            }
            animator.SetBool("yurume", false);
            animator.SetBool("ok", true);
        }
    }
    void GeriKac()
    {
        if(geriKac)
        {
            okTimer = 0;

            animator.SetBool("ok", false);

            animator.SetBool("yurume", true);

            if (oyuncu.transform.position.x > transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                transform.Translate(-transform.right * hareketHizi * Time.deltaTime);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.Translate(transform.right * hareketHizi * Time.deltaTime);
            }
        }
    }
}
