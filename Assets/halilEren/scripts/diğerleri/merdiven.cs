using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class merdiven : MonoBehaviour
{
    public Transform maxPos, minPos;
    bool merdivende;
    tirmanma tirmanma;
    // Start is called before the first frame update
    void Start()
    {
        tirmanma=FindObjectOfType<tirmanma>();
    }

    // Update is called once per frame
    void Update()
    {
        if(merdivende)
        {
            if(!tirmanma.tirmaniyor)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    tirmanma.tirmaniyor = true;
                    tirmanma.transform.position = new Vector2(transform.position.x - 0.05f, tirmanma.transform.position.y + 0.35f);
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    tirmanma.tirmaniyor = true;
                    tirmanma.transform.position = new Vector2(transform.position.x - 0.05f, tirmanma.transform.position.y - 1f);
                }
            }
            if((tirmanma.transform.position.y>=maxPos.transform.position.y)||(tirmanma.transform.position.y<minPos.transform.position.y))
            {
                tirmanma.tirmaniyor = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("oyuncu"))
        {
            merdivende = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("oyuncu"))
        {
            merdivende = false;
        }
    }
}
