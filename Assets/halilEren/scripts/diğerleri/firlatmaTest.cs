using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class firlatmaTest : MonoBehaviour
{
    public GameObject agirCekimVolume;
    public GameObject firlatma1,firlatma2;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(firlatma1,transform.position,Quaternion.identity);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;

            agirCekimVolume.SetActive(true);

        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            Cursor.lockState = CursorLockMode.Locked;
            agirCekimVolume.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Instantiate(firlatma2, transform.position, Quaternion.identity);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;

            agirCekimVolume.SetActive(true);

        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            Cursor.lockState = CursorLockMode.Locked;
            agirCekimVolume.SetActive(false);
        }

    }
}
