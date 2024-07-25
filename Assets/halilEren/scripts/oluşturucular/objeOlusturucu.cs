using UnityEngine;

public class objeOlusturucu : MonoBehaviour
{
    public int ihtimal;
    public GameObject[] gameObjects;
    public Transform[] transforms;

    void Start()
    {
        if (ihtimal == 0)
        {
            ihtimal = 100;
        }
        for (int i = 0; i < transforms.Length; i++)
        {
            int a = Random.Range(0, gameObjects.Length);
            int b = Random.Range(0, 100);
            if (b <= ihtimal)
            {
                Instantiate(gameObjects[a], transforms[i].transform.position, transforms[i].transform.rotation, transforms[i].transform);

            }
        }
    }
}