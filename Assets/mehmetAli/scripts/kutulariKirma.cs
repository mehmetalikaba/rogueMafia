using UnityEngine;

public class kutulariKirma : MonoBehaviour
{

    public bool kirilabilir;

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            kirilabilir = true;
        }
    }
}
