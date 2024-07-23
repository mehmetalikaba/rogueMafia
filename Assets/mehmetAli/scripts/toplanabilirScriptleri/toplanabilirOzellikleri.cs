using TMPro;
using UnityEngine;

public class toplanabilirOzellikleri : MonoBehaviour
{
    public Sprite toplanabilirIcon;
    public string toplanabilirKeyi, toplanabilirAdi, toplanabilirAciklamaKeyi;
    public toplanabilirKullanmaScripti toplanabilirKullanmaScripti;
    public sandiktanCikanToplanabilirHareketi sandiktanCikanToplanabilirHareketi;
    public Rigidbody2D rb;

    void Start()
    {
        toplanabilirKullanmaScripti = FindObjectOfType<toplanabilirKullanmaScripti>();
        sandiktanCikanToplanabilirHareketi = GetComponent<sandiktanCikanToplanabilirHareketi>();
        rb = GetComponent<Rigidbody2D>();
    }
}