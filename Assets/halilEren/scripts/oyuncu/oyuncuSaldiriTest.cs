using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class oyuncuSaldiriTest : MonoBehaviour
{
    bool okFirladi;
    public GameObject okSag, okSol, silah1, silah2;
    silahTest silahTest1, silahTest2;
    public Transform saldiriPos;
    public LayerMask dusmanLayer;
    public float saldiriMenzili1, saldiriMenzili2;
    public float hasar1, hasar2;
    public float sonHasar;
    public RuntimeAnimatorController oyuncuAnimator, silah1Animator, silah2Animator;
    public Animator animator;

    private void Start()
    {

        //animator.runtimeAnimatorController = oyuncuAnimator;

        silahTest1 = silah1.GetComponent<silahTest>();
        silahTest2 = silah2.GetComponent<silahTest>();
        Debug.Log(silahTest1.silahAdi);
        Debug.Log(silahTest2.silahAdi);

        saldiriMenzili1 = silahTest1.silahSaldiriMenzili;
        saldiriMenzili2 = silahTest2.silahSaldiriMenzili;
        hasar1 = silahTest1.silahSaldiriHasari;
        hasar2 = silahTest2.silahSaldiriHasari;
        silah1Animator = silahTest1.karakterAnimator;
        silah2Animator = silahTest2.karakterAnimator;

    }
    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.runtimeAnimatorController = silah1Animator;

            animator.SetTrigger("saldiri");

            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(saldiriPos.position, saldiriMenzili1, dusmanLayer);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<dusmanHasar>().hasarAl(hasar1);
                sonHasar = hasar1;
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (okFirladi == false)
            {
                okFirladi = true;

                animator.runtimeAnimatorController = silah2Animator;

                animator.SetTrigger("saldiri");
                StartCoroutine(okZaman());
            }
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            animator.runtimeAnimatorController = oyuncuAnimator;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(saldiriPos.position, saldiriMenzili1);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(saldiriPos.position, saldiriMenzili2);
    }
    IEnumerator okZaman()
    {
        yield return new WaitForSeconds(0.55f);
        if (transform.localScale.x == 1)
        {
            Instantiate(okSag, transform.position, okSag.transform.rotation);
        }
        if (transform.localScale.x == -1)
        {
            Instantiate(okSol, transform.position, okSol.transform.rotation);
        }
        okFirladi = false;
    }
}
