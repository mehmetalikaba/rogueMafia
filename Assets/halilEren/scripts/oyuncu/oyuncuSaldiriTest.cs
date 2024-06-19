using UnityEngine;

public class oyuncuSaldiriTest : MonoBehaviour
{
    public bool saldirdi;
    public float saldiriMenzil;
    public LayerMask hitLayer;
    RaycastHit2D hit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            hit = Physics2D.Raycast(transform.position, transform.right, saldiriMenzil, hitLayer);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.name);
                saldirdi = true;
            }
        }
    }
}
