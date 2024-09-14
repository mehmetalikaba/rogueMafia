using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Keypad0))
        {
            RestartSceneFonk();
        }
        if(Input.GetKeyUp(KeyCode.Keypad9))
        {
            Menuye();
        }
    }
    // Bu fonksiyon mevcut sahneyi yeniden başlatır
    public void RestartSceneFonk()
    {
        // Mevcut sahnenin adı
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Mevcut sahneyi yeniden yükle
        SceneManager.LoadScene(currentSceneName);
    }
    public void Menuye()
    {
        SceneManager.LoadScene("_GistMenü");
    }
}
