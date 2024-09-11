using UnityEngine;

public class kusKontrol : MonoBehaviour
{
    public bool tonyaMaskesi;
    public GameObject fx;
    float fxTimer;

    public GameObject oyuncu, kusFirlatilan;
    public float yumuşatmaHizi, takipHizi, yalpalamaMiktari, yalpalamaTimer, yalpalamaSuresi, yalpalamaHizi, yalpalamaYonuX, yalpalamaYonuY, firlatmaTimer, firlatmaSuresi;
    public bool yakindaDusmanVar, firlatildiMi, kusDur;

    public Vector3 soldaDurus = new Vector3(-1f, 1f, 0f);
    public Vector3 sagdaDurus = new Vector3(1f, 1f, 0f);
    private Vector3 hedefPozisyon;
    private oyuncuHareket oyuncuHareket;
    Vector3 yalpalamaOffset, hedefYalpalamaOffset;

    void Start()
    {
        oyuncuHareket = oyuncu.GetComponent<oyuncuHareket>();
        firlatmaTimer = 0;

        yalpalamaOffset = Vector3.zero;
        hedefYalpalamaOffset = Vector3.zero;
        yalpalamaSuresi = Random.Range(0.5f, 1f);
        yalpalamaTimer = yalpalamaSuresi;
    }

    void FixedUpdate()
    {
        oyuncuyuTakip();
        if (!tonyaMaskesi)
        {
            dusmanVarMi();
            fxTimer += Time.deltaTime;
            if (fxTimer > 0.25f)
            {
                if (fx != null)
                    Instantiate(fx, transform.position, transform.rotation, transform.transform);
                fxTimer = 0;
            }
        }


    }
    public void oyuncuyuTakip()
    {
        if (!kusDur)
        {
            Vector3 yeniPozisyon = Vector3.Lerp(transform.position, hedefPozisyon, yumuşatmaHizi);

            yeniPozisyon += Yalpalama();

            transform.position = Vector3.MoveTowards(transform.position, yeniPozisyon, takipHizi * Time.deltaTime);

            if (oyuncuHareket.sagaBakiyor)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                hedefPozisyon = oyuncu.transform.position + soldaDurus;
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                hedefPozisyon = oyuncu.transform.position + sagdaDurus;
            }
        }
    }
    Vector3 Yalpalama()
    {
        yalpalamaTimer -= Time.deltaTime;

        if (yalpalamaTimer <= 0)
        {
            float yeniYonuX = Random.Range(-0.1f, 0.1f);
            float yeniYonuY = Random.Range(-0.1f, 0.1f);

            hedefYalpalamaOffset = new Vector3(
                yeniYonuX * yalpalamaMiktari,
                yeniYonuY * yalpalamaMiktari,
                0f
            );

            yalpalamaSuresi = Random.Range(0.5f, 1f);
            yalpalamaTimer = yalpalamaSuresi;
        }
        yalpalamaOffset = Vector3.Lerp(yalpalamaOffset, hedefYalpalamaOffset, Time.deltaTime * yalpalamaHizi);
        return yalpalamaOffset;
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
                        kusDur = true;
                        if (dusman.transform.position.x < transform.position.x)
                            transform.rotation = Quaternion.Euler(0, 180, 0);
                        else
                            transform.rotation = Quaternion.Euler(0, 0, 0);
                        Instantiate(kusFirlatilan, transform.position, transform.rotation);
                    }
                    break;
                }
                else
                    yakindaDusmanVar = false;
            }
        }
        else if (firlatildiMi)
        {
            firlatmaTimer -= Time.deltaTime;
            if (firlatmaTimer <= firlatmaSuresi / 2.5f)
                kusDur = false;
            if (firlatmaTimer <= 0)
            {
                firlatildiMi = false;
                firlatmaTimer = firlatmaSuresi;
            }
        }
    }
}