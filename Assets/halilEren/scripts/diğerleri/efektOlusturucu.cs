using UnityEngine;

public class efektOlusturucu : MonoBehaviour
{
    public float xPos1, xPos2;
    public float yPos1, yPos2;
    public GameObject obje;
    public float time;
    float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer>time)
        {
            Zaman();
            timer = 0;
        }
    }
    void Zaman()
    {
        float a = Random.Range(xPos1,xPos2);
        float b = Random.Range(yPos1,yPos2);
        Instantiate(obje,new Vector2(a, b),obje.transform.rotation,transform.transform);   
    }
}
