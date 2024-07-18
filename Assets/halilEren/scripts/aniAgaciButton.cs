using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aniAgaciButton : MonoBehaviour
{
    public GameObject panel;
    bool butonda;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(butonda)
        {
            panel.transform.position=Input.mousePosition;
        }
    }
    private void OnMouseEnter()
    {
        Debug.Log("girdi");
        butonda = true;
    }
    private void OnMouseExit()
    {
        Debug.Log("cikti");
        butonda = false;
    }
}
