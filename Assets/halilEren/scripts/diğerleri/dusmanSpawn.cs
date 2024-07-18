using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dusmanSpawn : MonoBehaviour
{
    public GameObject[] dusmans;
    public Transform pos1, pos2;
    public float spawnTime;
    float spawnTimer;
    // Start is called before the first frame update
    void Start()
    {
        int i = Random.RandomRange(0, dusmans.Length);
        int a = Random.Range(0, 2);
        if (i == 1)
        {
            if(a == 1)
            {
                Instantiate(dusmans[i], pos1.transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(dusmans[i], pos1.transform.position, Quaternion.identity);

            }
        }
        else
        {
            if (a == 1)
            {
                Instantiate(dusmans[i], pos2.transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(dusmans[i], pos2.transform.position, Quaternion.identity);

            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        spawnTimer += Time.deltaTime;
        if(spawnTimer>=spawnTime)
        {
            int i = Random.RandomRange(0, 2);
            int a = Random.Range(0, 2);
            if (i == 1)
            {
                if (a == 1)
                {
                    Instantiate(dusmans[0], pos1.transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(dusmans[1], pos1.transform.position, Quaternion.identity);

                }
            }
            else
            {
                if (a == 1)
                {
                    Instantiate(dusmans[0], pos2.transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(dusmans[1], pos2.transform.position, Quaternion.identity);

                }
            }
            spawnTimer = 0;
        }
    }
}
