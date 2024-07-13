using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rastgeleSilahDusurmeScripti : MonoBehaviour
{
    public GameObject rastgeleSilah;
    public float randomSayi;

    public void silahiDusur(float silahDusurmeIhtimali, float minSilahDusurmeIhtimali, float maxSilahDusurmeIhtimali)
    {
        randomSayi = Random.Range(minSilahDusurmeIhtimali, maxSilahDusurmeIhtimali);

        if (randomSayi > silahDusurmeIhtimali)
            Instantiate(rastgeleSilah, transform.position, transform.rotation);
    }
}