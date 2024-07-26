using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tesekkur : MonoBehaviour
{
    private void Start()
    {
       Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
    }
    public void menuye()
    {
        SceneManager.LoadScene("menuTest");
    }
}
