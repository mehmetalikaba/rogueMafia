using Unity.VisualScripting;
using UnityEngine;

public class oyuncuSaldiriTest : MonoBehaviour
{
    public GameObject silah1, silah2;
    silahTest silahTest1,silahTest2;
    public Transform saldiriPos;
    public LayerMask dusmanLayer;
    public float saldiriMenzili1,saldiriMenzili2;
    public float hasar1,hasar2;
    public float sonHasar;
    private void Start()
    {
        silahTest1 = silah1.GetComponent<silahTest>();
        silahTest2 = silah2.GetComponent<silahTest>();
        Debug.Log(silahTest1.silahAdi);
        Debug.Log(silahTest2.silahAdi);
        
        saldiriMenzili1 = silahTest1.silahSaldiriMenzili;
        saldiriMenzili2 = silahTest2.silahSaldiriMenzili;
        hasar1 = silahTest1.silahSaldiriHasari;
        hasar2 = silahTest2.silahSaldiriHasari;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(saldiriPos.position, saldiriMenzili1, dusmanLayer);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<dusmanHasar>().hasarAl(hasar1);
                sonHasar = hasar1;
            }
        }
        if(Input.GetMouseButtonDown(1))
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(saldiriPos.position, saldiriMenzili2, dusmanLayer);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<dusmanHasar>().hasarAl(hasar2);
                sonHasar = hasar2;
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(saldiriPos.position, saldiriMenzili1);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(saldiriPos.position, saldiriMenzili2);
    }
}
