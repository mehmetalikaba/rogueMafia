using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "yetenekAgaclari", menuName = "Scriptable Objects/yetenekAgaclari")]
public class yetenekAgaclari : ScriptableObject
{
    public List<yetenekObjesi> menzilliYetenekler, pasifYetenekler, yakinYetenekler;
}
