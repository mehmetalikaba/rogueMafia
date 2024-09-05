using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zincirSes : MonoBehaviour
{
    public GameObject ses;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("oyuncu"))
        {
            Instantiate(ses,transform.position,Quaternion.identity );
        }
    }
}
