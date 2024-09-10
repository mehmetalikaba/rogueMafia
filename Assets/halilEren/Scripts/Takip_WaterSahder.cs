using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Takip_WaterSahder : MonoBehaviour
{
    public Transform target;
    void FixedUpdate()
    {
        transform.position = new Vector2(target.transform.position.x, transform.position.y);
        
    }
}
