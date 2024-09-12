using UnityEngine;

[CreateAssetMenu(fileName = "iksirOzellikleri", menuName = "Scriptable Objects/iksirOzellikleri")]
public class iksirOzellikleri : ScriptableObject
{
    public string iksirAdi, iksirAciklamaKeyi;
    public float iksirSuresi;
    public Sprite iksirIcon;
    public GameObject fx;
}
