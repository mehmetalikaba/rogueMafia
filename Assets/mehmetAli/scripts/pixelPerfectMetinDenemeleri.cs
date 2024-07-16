using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pixelPerfectMetinDenemeleri : MonoBehaviour
{
    [SerializeField] private float pixelsPerUnit = 100f;

    void LateUpdate()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            Vector3 position = rectTransform.localPosition;
            position.x = Mathf.Round(position.x * pixelsPerUnit) / pixelsPerUnit;
            position.y = Mathf.Round(position.y * pixelsPerUnit) / pixelsPerUnit;
            rectTransform.localPosition = position;
        }
    }
}