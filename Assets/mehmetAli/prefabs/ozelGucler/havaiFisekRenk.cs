using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class havaiFisekRenk : MonoBehaviour
{
    public TrailRenderer trailRenderer;
    public float renkDegisimHizi = 1f; 

    private Gradient gradient;
    private GradientColorKey[] colorKey;
    private GradientAlphaKey[] alphaKey;
    private float zaman;

    void Start()
    {
        gradient = new Gradient();
        colorKey = new GradientColorKey[2];
        alphaKey = new GradientAlphaKey[2];

        colorKey[0].color = RastgeleRenkOlustur();
        colorKey[0].time = 0.0f;
        colorKey[1].color = RastgeleRenkOlustur();
        colorKey[1].time = 1.0f;

        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 1.0f;
        alphaKey[1].time = 1.0f;

        gradient.SetKeys(colorKey, alphaKey);
        trailRenderer.colorGradient = gradient;
    }
    void Update()
    {
        zaman += Time.deltaTime * renkDegisimHizi;

        colorKey[0].color = Color.Lerp(colorKey[0].color, RastgeleRenkOlustur(), zaman);
        colorKey[1].color = Color.Lerp(colorKey[1].color, RastgeleRenkOlustur(), zaman);

        gradient.SetKeys(colorKey, alphaKey);
        trailRenderer.colorGradient = gradient;

        if (zaman >= 1f)
            zaman = 0f;
    }
    Color RastgeleRenkOlustur()
    {
        return new Color(Random.value, Random.value, Random.value);
    }
}