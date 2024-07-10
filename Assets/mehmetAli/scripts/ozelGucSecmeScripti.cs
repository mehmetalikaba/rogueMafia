using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ozelGucSecmeScripti : MonoBehaviour
{
    public Sprite[] ozelGucIkonlari;
    public GameObject[] ozelGucObjeleri;
    public bool ozelGuc1Secildi, ozelGuc2Secildi;

    public ozelGucKullanmaScripti ozelGuc1KullanmaScript, ozelGuc2KullanmaScript;
    public Button buton1, buton2;

    private List<System.Action> skillMetotlari;

    void Start()
    {
        skillMetotlari = new List<System.Action>
        {
            () => ozelGucSecimi(0),
            () => ozelGucSecimi(1),
            () => ozelGucSecimi(2),
            () => ozelGucSecimi(3),
            () => ozelGucSecimi(4),
            () => ozelGucSecimi(5)
        };

        RastgeleSkillAtama();
    }

    void RastgeleSkillAtama()
    {
        int index1 = Random.Range(0, skillMetotlari.Count);
        int index2;
        do
        {
            index2 = Random.Range(0, skillMetotlari.Count);
        } while (index1 == index2);

        buton1.onClick.AddListener(() => skillMetotlari[index1]());
        buton2.onClick.AddListener(() => skillMetotlari[index2]());

        buton1.GetComponent<Image>().sprite = ozelGucIkonlari[index1];
        buton2.GetComponent<Image>().sprite = ozelGucIkonlari[index2];
    }

    void ozelGucSecimi(int index)
    {
        if (!ozelGuc1Secildi)
        {
            ozelGuc1KullanmaScript.ozelGucObjesi = ozelGucObjeleri[index];
            ozelGuc1KullanmaScript.ozelGuc1Image.sprite = ozelGucIkonlari[index];
            ozelGuc1Secildi = true;
        }
        else
        {
            ozelGuc2KullanmaScript.ozelGucObjesi = ozelGucObjeleri[index];
            ozelGuc2KullanmaScript.ozelGuc2Image.sprite = ozelGucIkonlari[index];
            ozelGuc2Secildi = true;
        }
    }
}
