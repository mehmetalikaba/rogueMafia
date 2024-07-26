using System.Collections;
using UnityEngine;

public class randomMetinGetirici : MonoBehaviour
{
    public Animator animator;
    public localizedText localizedText;
    public string[] keys;
    void Start()
    {
        localizedText = GetComponent<localizedText>();
        animator = GetComponent<Animator>();
        StartCoroutine(randomKeyGetir());
    }

    IEnumerator randomKeyGetir()
    {
        float randomSayi = Random.Range(20, 40);
        yield return new WaitForSeconds(randomSayi);
        animator.SetTrigger("textGetir");
        int randomKey = Random.Range(0, keys.Length);
        localizedText.key = keys[randomKey];
        StartCoroutine(randomKeyGetir());
    }

}
