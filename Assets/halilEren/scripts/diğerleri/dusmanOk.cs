using UnityEngine;

public class dusmanOk : MonoBehaviour
{
    public float hasar;
    canKontrol canKontrol;
    // Start is called before the first frame update
    void Start()
    {
        canKontrol = FindObjectOfType<canKontrol>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("oyuncu"))
        {
            canKontrol.canAzalmasi(hasar, "firlatilan");
            if (!canKontrol.oyuncuHareket.atiliyor)
                Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("zemin"))
        {

            string newLayerName = "YerdekiProjectile";
            int newLayer = LayerMask.NameToLayer(newLayerName);
            gameObject.layer = newLayer;
        }
    }
}
