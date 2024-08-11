using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class mouseUzerindeMi : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button buton;
    public Text text;
    public sefPanelScripti sefPanelScripti;
    public bool mouseUzerinde;
    public string objeAdi;

    void Start()
    {
        sefPanelScripti = FindObjectOfType<sefPanelScripti>();
    }

    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseUzerinde = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseUzerinde = false;
    }
}
