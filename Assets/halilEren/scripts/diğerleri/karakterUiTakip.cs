using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class karakterUiTakip : MonoBehaviour
{
    public Transform hedef;
    public RectTransform objeTransform;
    void Update()
    {
        if (hedef.transform.rotation.y == 0)
        {
            objeTransform.localScale=new Vector2(-0.003f, objeTransform.localScale.y);
        }
        else
        {
            objeTransform.localScale=new Vector2(0.003f, objeTransform.localScale.y);
        }
    }
}
