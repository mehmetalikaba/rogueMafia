using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "YeniYetenekAgaci", menuName = "Yetenek Aðacý/Yetenek Aðacý")]
public class YetenekAgaci : ScriptableObject
{
    public List<Yetenek> yakinSaldiriYetenekleri;
    public List<Yetenek> uzakSaldiriYetenekleri;
    public List<Yetenek> ozelGucYetenekleri;
}
