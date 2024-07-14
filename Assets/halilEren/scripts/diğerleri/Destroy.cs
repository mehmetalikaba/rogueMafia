using UnityEngine;

public class Destroy : MonoBehaviour
{
    public float kaybolmaSuresi;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject,kaybolmaSuresi);
    }
}
