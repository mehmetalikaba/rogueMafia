using UnityEngine;

public class silahKontrol : MonoBehaviour
{

    public GameObject silah1, silah2;


    void Start()
    {
        silah1.gameObject.SetActive(true);
        silah2.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            silah1.gameObject.SetActive(true);
            silah2.gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            silah1.gameObject.SetActive(false);
            silah2.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {

        }
        if (Input.GetKeyDown(KeyCode.R))
        {

        }
    }
}
