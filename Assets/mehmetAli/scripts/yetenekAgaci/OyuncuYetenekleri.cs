using System.Collections.Generic;
using UnityEngine;

public class OyuncuYetenekleri : MonoBehaviour
{
    public int ejderhaPuani;
    public Dictionary<Yetenek, int> aktifYetenekler = new Dictionary<Yetenek, int>();

    public bool YetenekAc(Yetenek yetenek)
    {
        if (ejderhaPuani >= yetenek.gerekliEjderhaPuani)
        {
            bool gerekliYeteneklerTamam = true;
            foreach (var gerekliYetenek in yetenek.gerekliYetenekler)
            {
                if (!aktifYetenekler.ContainsKey(gerekliYetenek) || aktifYetenekler[gerekliYetenek] < gerekliYetenek.maxSeviye)
                {
                    gerekliYeteneklerTamam = false;
                    break;
                }
            }

            if (gerekliYeteneklerTamam)
            {
                if (aktifYetenekler.ContainsKey(yetenek))
                {
                    aktifYetenekler[yetenek]++;
                }
                else
                {
                    aktifYetenekler[yetenek] = 1;
                }
                ejderhaPuani -= yetenek.gerekliEjderhaPuani;
                return true;
            }
        }
        return false;
    }

    public bool YetenekAcikmi(Yetenek yetenek)
    {
        return aktifYetenekler.ContainsKey(yetenek) && aktifYetenekler[yetenek] > 0;
    }
}
