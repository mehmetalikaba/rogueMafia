using UnityEngine;

[CreateAssetMenu(fileName = "antikaYadigarOzellikleri", menuName = "Scriptable Objects/antikaYadigarOzellikleri")]

public class antikaYadigarOzellikleri : ScriptableObject
{
    public bool antika;
    public int antikaDegeri;
    public string antikaAdi, antikaAciklamaKeyi;
    public Sprite antikaIcon;

    public bool yadigar;
    public int yadigarDegeri;
    public string yadigarAdi, yadigarAciklamaKeyi;
    public Sprite yadigarIcon;
}

