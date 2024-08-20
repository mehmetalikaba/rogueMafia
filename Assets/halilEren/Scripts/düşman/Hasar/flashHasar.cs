using UnityEngine;
using System.Collections;

public class flashHasar : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Color hitColor = Color.red;
    private Color originalColor;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    public void Flash()
    {
        if (spriteRenderer != null)
        {
            StartCoroutine(FlashColor());
        }
    }

    IEnumerator FlashColor()
    {
        spriteRenderer.color = hitColor;
        yield return new WaitForSeconds(0.25f); // Kısa bir süre bekle
        spriteRenderer.color = originalColor;
    }
}
