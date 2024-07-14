using UnityEngine;

public class yagmurDamla : MonoBehaviour
{
    public float xPos1, xPos2;
    public GameObject damla;
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
        float f = Random.Range(xPos1,xPos2);
        Instantiate(damla,new Vector2(f, transform.position.y),Quaternion.identity);   
    }
}
