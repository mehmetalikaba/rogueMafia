using UnityEngine;

public class kusKontrol : MonoBehaviour
{
    public GameObject oyuncu, firlatilan;
    public float temelTakipHizi = 5.0f;
    public float yumuşatmaHizi = 0.125f;
    public Vector3 soldaDurus = new Vector3(-1f, 1f, 0f);
    public Vector3 sagdaDurus = new Vector3(1f, 1f, 0f);
    public LayerMask dusmanLayer;

    private Vector3 hedefPozisyon;
    private oyuncuHareket oyuncuHareket;
    private Transform enYakinDusman;

    public bool dusmanaYaklasti, dusmanaGidiyor;
    public float yalpalamaMiktari, yalpalamaHizi;

    private void Start()
    {
        oyuncuHareket = oyuncu.GetComponent<oyuncuHareket>();
    }

    void Update()
    {
        if (enYakinDusman == null)
        {
            dusmanVarMi();
        }

        /*if (enYakinDusman != null)
        {
            dusmanaGidiyor = true;
            dusmanaYaklasti = false;

            hedefPozisyon = new Vector3(enYakinDusman.position.x, enYakinDusman.position.y + 1f, enYakinDusman.position.z);

            if (Vector3.Distance(transform.position, enYakinDusman.position) < 2f)
            {
                dusmanaYaklasti = true;
                dusmanaGidiyor = false;
            }
        }*/

        if (enYakinDusman != null)
        {
            Instantiate(firlatilan, transform.position, transform.rotation);
        }

        dusmanaGidiyor = false;

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
        transform.position = Vector3.MoveTowards(transform.position, yeniPozisyon, temelTakipHizi * Time.deltaTime);

        if (hedefPozisyon.x > transform.position.x)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else
            transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    void dusmanVarMi()
    {
        //enYakinDusman = null;
        float enYakinMesafe = Mathf.Infinity;

        Collider2D[] yakindakiDusmanlar = Physics2D.OverlapCircleAll(oyuncu.transform.position, 2.5f, dusmanLayer);

        for (int i = 0; i < yakindakiDusmanlar.Length; i++)
        {
            float dusmanMesafesi = Vector3.Distance(oyuncu.transform.position, yakindakiDusmanlar[i].transform.position);
            if (dusmanMesafesi < enYakinMesafe)
            {
                enYakinMesafe = dusmanMesafesi;
                //enYakinDusman = yakindakiDusmanlar[i].transform;
            }
        }
    }
}
