using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dusmanSpawn : MonoBehaviour
{
    public GameObject dusman;
    public Transform pos1, pos2;
    public float spawnTime;
    float spawnTimer;
    // Start is called before the first frame update
    void Start()
    {
        int i = Random.RandomRange(0, 2);
        if (i == 1)
        {
            Instantiate(dusman, pos1.transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(dusman, pos2.transform.position, Quaternion.identity);

        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        spawnTimer += Time.deltaTime;
        if(spawnTimer>=spawnTime)
        {
            int i = Random.RandomRange(0, 2);
            if(i==1)
            {
                Instantiate(dusman,pos1.transform.position, Quaternion.identity);   
            }
            else
            {
                Instantiate(dusman, pos2.transform.position, Quaternion.identity);

            }
            spawnTimer = 0;
        }
    }
}
