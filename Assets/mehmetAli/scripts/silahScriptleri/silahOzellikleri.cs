using UnityEngine;

[CreateAssetMenu(fileName = "silahOzellikleri", menuName = "Scriptable Objects/silahOzellikleri")]
public class silahOzellikleri : ScriptableObject
{
    public string silahTuru;
    public string silahAdi;
    public float silahSaldiriHasari;
    public float silahSaldiriMenzili;
    public float silahDayanikliligi, silahDayanikliligiAzalmaMiktari;
    public RuntimeAnimatorController karakterAnimator;
    public Sprite silahIcon;
    public string aciklamaKeyi;
    public AnimationClip[] animasyonClipleri;
    public GameObject solMenzilli, sagMenzilli;
    public AudioClip[] saldiriSesleri;
}