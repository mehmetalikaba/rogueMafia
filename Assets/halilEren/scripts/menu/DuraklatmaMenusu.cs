using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DuraklatmaMenusu : MonoBehaviour
{
    public GameObject duraklatmaMenusu;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            duraklatmaMenusu.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void DevamEt()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        duraklatmaMenusu.SetActive(false);
        Time.timeScale = 1;
    }
    public void Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void Masaustu()
    {
        Time.timeScale = 1;
        Application.Quit();
    }

}
