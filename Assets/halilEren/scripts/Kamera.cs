using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamera : MonoBehaviour
{
    oyuncuHareket oyuncuHareket;
    public float minX, maxX;
    public Vector3 offset;
    public float damping;
    Vector3 velocity = Vector3.zero;
    float startYPos;
    public Transform target;

    bool yukari, asagi;
    private void Start()
    {
        oyuncuHareket =FindAnyObjectByType<oyuncuHareket>();
        startYPos = transform.position.y;
    }
    private void FixedUpdate()
    {
        Vector3 movePos = target.transform.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, movePos, ref velocity, damping);
        transform.position = new Vector3(transform.position.x, transform.position.y, movePos.z);
        if (transform.position.x < minX)
        {
            transform.position = new Vector3(minX, transform.position.y,-10);
        }
        if (transform.position.x > maxX)
        {
            transform.position = new Vector3(maxX, transform.position.y,-10);
        }
        if(oyuncuHareket.transform.localScale.x==1)
        {
            offset=new Vector3(offset.x+1*Time.deltaTime*6,offset.y, offset.z);
            if(offset.x>3.5f)
            {
                offset.x = 3.5f;
            }
        }
        else
        {
            offset = new Vector3(offset.x - 1*Time.deltaTime*6, offset.y, offset.z);
            if (offset.x < -3.5f)
            {
                offset.x = -3.5f;
            }
        }
        if(!asagi)
        {
            if (Input.GetKey(KeyCode.W))
            {
                yukari = true;
                offset = new Vector3(offset.x, offset.y + 1 * Time.deltaTime * 4, offset.z);
                if (offset.y > 5.25f)
                {
                    offset.y = 5.25f;
                }
            }
            else
            {
                offset = new Vector3(offset.x, offset.y - 1 * Time.deltaTime * 4, offset.z);
                if (offset.y < 3.5f)
                {
                    offset.y = 3.5f;
                }
            }
        }
        if(!yukari)
        {
            if (Input.GetKey(KeyCode.S))
            {
                asagi = true;
                offset = new Vector3(offset.x, offset.y - 1 * Time.deltaTime * 4, offset.z);
                if (offset.y < 3)
                {
                    offset.y = 3;
                }
            }
            else
            {
                offset = new Vector3(offset.x, offset.y + 1 * Time.deltaTime * 4, offset.z);
                if (offset.y > 3.5f)
                {
                    offset.y = 3.5f;
                }
            }
        }

        if(offset.y==3.5f)
        {
            yukari = false;
            asagi = false;
        }

    }
}