using UnityEngine;

public class dusmanHareket : MonoBehaviour
{
    GameObject oyuncu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        oyuncu = GameObject.FindGameObjectWithTag("oyuncu");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(oyuncu.transform.position.x>transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (oyuncu.transform.position.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
