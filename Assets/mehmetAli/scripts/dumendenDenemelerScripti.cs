using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class dumendenDenemelerScripti : MonoBehaviour
{
    public Image redRectangle;
    public GameObject shadow;

    public Image screenOverlay;
    public float effectDuration = 1f;
    public Image dumenden1;


    private Vector3 originalPos;
    private Transform cameraTransform;
    private Transform playerTransform;
    public CinemachineVirtualCamera kameraVirtualKamera;
    public float shakeDuration = 0.05f;
    public float shakeAmount = 0.2f;

    void Start()
    {
        StartCoroutine(Pulsate());


        shadow = new GameObject("Shadow");
        shadow.AddComponent<Image>();
        shadow.GetComponent<Image>().sprite = redRectangle.GetComponent<Image>().sprite;
        shadow.transform.SetParent(redRectangle.transform);
        shadow.transform.localPosition = new Vector3(5, -5, 0);
        shadow.GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
        screenOverlay.gameObject.SetActive(false);


        cameraTransform = kameraVirtualKamera.transform;
        playerTransform = GameObject.Find("Oyuncu").transform;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            CreateRipple();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            StartCoroutine(ShowEffect());
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            StartCoroutine(Shake());
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            StartCoroutine(ClickEffect());
        }
    }
    public IEnumerator Shake()
    {
        originalPos = cameraTransform.localPosition;
        kameraVirtualKamera.Follow = null;

        float elapsedTime = 0f;
        while (elapsedTime < shakeDuration)
        {
            elapsedTime += Time.deltaTime;
            float offsetX = Random.Range(-1f, 1f) * shakeAmount;
            float offsetY = Random.Range(-1f, 1f) * shakeAmount;
            cameraTransform.localPosition = originalPos + new Vector3(offsetX, offsetY, 0);
            yield return null;
        }

        cameraTransform.localPosition = originalPos;
        kameraVirtualKamera.Follow = playerTransform;
    }

    IEnumerator Pulsate()
    {
        while (true)
        {
            float t = Mathf.PingPong(Time.time * 10f, 1f);
            redRectangle.color = Color.Lerp(Color.red, Color.white, t);
            yield return null;
        }
    }

    void CreateRipple()
    {
        GameObject ripple = new GameObject("Ripple");
        ripple.AddComponent<Image>().sprite = redRectangle.GetComponent<Image>().sprite;
        ripple.transform.SetParent(redRectangle.transform);
        ripple.transform.localPosition = Vector3.zero;
        ripple.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);

        StartCoroutine(RippleEffect(ripple));
    }

    IEnumerator RippleEffect(GameObject ripple)
    {
        float duration = 0.5f;
        float time = 0;
        Vector3 initialScale = ripple.transform.localScale;
        while (time < duration)
        {
            time += Time.deltaTime;
            float scale = Mathf.Lerp(1, 2, time / duration);
            ripple.transform.localScale = initialScale * scale;
            ripple.GetComponent<Image>().color = new Color(1, 1, 1, Mathf.Lerp(0.5f, 0, time / duration));
            yield return null;
        }
        Destroy(ripple);
    }
    IEnumerator ShowEffect()
    {
        screenOverlay.gameObject.SetActive(true);
        float elapsedTime = 0f;
        Color originalColor = screenOverlay.color;
        while (elapsedTime < effectDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, elapsedTime / effectDuration);
            screenOverlay.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }
        screenOverlay.gameObject.SetActive(false);
    }



    IEnumerator ClickEffect()
    {
        Vector3 originalScale = dumenden1.transform.localScale;
        Vector3 clickedScale = originalScale * 1.1f;
        float elapsedTime = 0f;
        float duration = 0.1f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            dumenden1.transform.localScale = Vector3.Lerp(originalScale, clickedScale, elapsedTime / duration);
            yield return null;
        }

        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            dumenden1.transform.localScale = Vector3.Lerp(clickedScale, originalScale, elapsedTime / duration);
            yield return null;
        }
    }
}
