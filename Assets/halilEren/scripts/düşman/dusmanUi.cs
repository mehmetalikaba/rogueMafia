using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dusmanUi : MonoBehaviour
{
    RectTransform rectTransform;

    public Transform target;
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        gameObject.SetActive(false);
    }
    void FixedUpdate()
    {
        rectTransform.position = new Vector2(target.position.x,target.position.y+0.75f);
    }
    public void gorunur()
    {
        gameObject.SetActive(true);
        StartCoroutine(kapanmaSuresi());
    }
    IEnumerator kapanmaSuresi()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);

    }
}
