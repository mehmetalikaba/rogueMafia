using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBg : MonoBehaviour
{
    public Camera cam;
    private float length, startPos;
    public float parallaxEfect;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        cam = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        cam = FindObjectOfType<Camera>();

        float temp = (cam.transform.position.x * (1 - parallaxEfect));
        float dist = (cam.transform.position.x * parallaxEfect);

        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

        if (temp > startPos + length) startPos += length-4;
        else if (temp < startPos - length) startPos -= length+4;

    }
}
