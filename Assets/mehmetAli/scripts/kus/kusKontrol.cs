using UnityEngine;

public class kusKontrol : MonoBehaviour
{
    public GameObject fx;
    float fxTimer;

    public GameObject oyuncu, kusFirlatilan;
    public float yalpalamaMiktari, firlatmaTimer, yalpalamaHizi, firlatmaSuresi, yumuşatmaHizi = 0.125f, takipHizi = 5.0f;
    public bool yakindaDusmanVar, firlatildiMi;

    public Vector3 soldaDurus = new Vector3(-1f, 1f, 0f);
    public Vector3 sagdaDurus = new Vector3(1f, 1f, 0f);
    private Vector3 hedefPozisyon;
    private oyuncuHareket oyuncuHareket;

    void Start()
    {
        oyuncuHareket = oyuncu.GetComponent<oyuncuHareket>();
        firlatmaTimer = 0;
    }

    void Update()
    {
        if (oyuncuHareket.sagaBakiyor)
            hedefPozisyon = oyuncu.transform.position + soldaDurus;
        else
            hedefPozisyon = oyuncu.transform.position + sagdaDurus;

        hedefPozisyon += new Vector3(
            Mathf.Sin(Time.time * yalpalamaHizi) * yalpalamaMiktari,
            Mathf.Cos(Time.time * yalpalamaHizi) * yalpalamaMiktari,
            0f
        );

        Vector3 yeniPozisyon = Vector3.Lerp(transform.position, hedefPozisyon, yumuşatmaHizi);
        transform.position = Vector3.MoveTowards(transform.position, yeniPozisyon, takipHizi * Time.deltaTime);

        if (hedefPozisyon.x > transform.position.x)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else
            transform.rotation = Quaternion.Euler(0, 180, 0);

        dusmanVarMi();

        if (firlatildiMi)
        {
            firlatmaTimer -= Time.deltaTime;
            if (firlatmaTimer <= 0)
            {
                firlatildiMi = false;
                firlatmaTimer = firlatmaSuresi;
            }
        }

        fxTimer += Time.deltaTime;
        if(fxTimer>0.25f)
        {
            Instantiate(fx, transform.position, transform.rotation);
            fxTimer = 0;
        }
    }

    void dusmanVarMi()
    {
        if (!firlatildiMi)
        {
            dusmanHasar[] dusmanlar = FindObjectsOfType<dusmanHasar>();
            foreach (var dusman in dusmanlar)
            {
                float distance = Vector3.Distance(dusman.transform.position, oyuncu.transform.position);
                if (distance <= 5)
                {
                    yakindaDusmanVar = true;
                    if (!firlatildiMi)
                    {
                        firlatildiMi = true;
                        Instantiate(kusFirlatilan, transform.position, transform.rotation);
                    }
                    break;
                }
                else
                    yakindaDusmanVar = false;
            }
        }
    }
}