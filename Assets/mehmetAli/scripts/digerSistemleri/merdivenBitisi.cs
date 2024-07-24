using UnityEngine;

public class merdivenBitisi : MonoBehaviour
{
    private PlatformEffector2D effector;
    public float waitTime;
    private tirmanma tirmanma;

    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.S))
            waitTime = 0.5f;
        if (waitTime <= 0)
            effector.rotationalOffset = 0f;
        else
        {
            waitTime -= Time.deltaTime;
            effector.rotationalOffset = 180f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("oyuncu"))
        {
            tirmanma = collision.gameObject.GetComponent<tirmanma>();
            if (tirmanma != null)
            {
                tirmanma.tirmaniyor = false;
            }
        }
    }
}