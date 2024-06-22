using UnityEngine;

public class envanterKontrol : MonoBehaviour
{

    public bool oyunDurdu;

    void Start()
    {

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {

        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            oyunDurdu = Input.GetKeyDown(KeyCode.P) ? !oyunDurdu : oyunDurdu;
        }
    }
}
