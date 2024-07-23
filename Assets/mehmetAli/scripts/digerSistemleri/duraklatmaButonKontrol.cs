using UnityEngine;
using UnityEngine.EventSystems;

public class duraklatmaButonKontrol : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject aciklamaObjesi;

    void Start()
    {
        aciklamaObjesi.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        aciklamaObjesi.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        aciklamaObjesi.SetActive(false);
    }
}
