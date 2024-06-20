using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "silahlarTest", menuName = "Scriptable Objects/silahlarTest")]
public class silahlarTest : ScriptableObject
{

    public string silahTuru;
    public string silahAdi;
    public float silahSaldiriHasari;
    public float silahSaldiriHizi;
    public Animator karakterAnimator;
    public Sprite silahIcon;

}