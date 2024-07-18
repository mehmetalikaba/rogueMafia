using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objeOlusturucu : MonoBehaviour
{
    public GameObject[] gameObjects;
    public Transform[] transforms;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transforms.Length; i++)
        {
            int a = Random.Range(0, gameObjects.Length);
            Instantiate(gameObjects[a], transforms[i].transform.position, transforms[i].transform.rotation);
        }
    }

}
