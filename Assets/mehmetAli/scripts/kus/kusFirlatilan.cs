using UnityEngine;

public class KusFirlatilan : MonoBehaviour
{
    public float hareketHizi = 5.0f;
    public LayerMask dusmanLayer;
    private Transform hedefDusman;
    private bool hedefBelirlendi = false;

    void Start()
    {
        BulEnYakinDusman();
    }

    void Update()
    {
        if (hedefBelirlendi)
            transform.position = Vector3.MoveTowards(transform.position, hedefDusman.position, hareketHizi * Time.deltaTime);
    }

    void BulEnYakinDusman()
    {
        dusmanHasar[] dusmanHasarlar = FindObjectsOfType<dusmanHasar>();

        Transform enYakinDusman = null;
        float minDistance = Mathf.Infinity;

        foreach (dusmanHasar dusman in dusmanHasarlar)
        {
            float distanceToDusman = Vector3.Distance(transform.position, dusman.transform.position);
            if (distanceToDusman < minDistance)
            {
                minDistance = distanceToDusman;
                enYakinDusman = dusman.transform;
            }
        }
        if (enYakinDusman != null)
        {
            hedefDusman = enYakinDusman;
            hedefBelirlendi = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("dusman"))
        {
            Debug.Log(collision.gameObject.name);
            collision.gameObject.GetComponent<dusmanHasar>().hasarAl(15, "kusFirlatilan");
            Destroy(gameObject);
        }
    }
}
