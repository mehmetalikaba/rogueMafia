using UnityEngine;

[CreateAssetMenu(fileName = "yemekOzellikleri", menuName = "Scriptable Objects/yemekOzellikleri")]

public class yemekOzellikleri : ScriptableObject
{
    public string yemekAdi, yemekAciklamaKeyi;
    public float yemekFiyati;
    public Sprite yemekSprite;
}
