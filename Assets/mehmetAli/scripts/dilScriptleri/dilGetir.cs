using UnityEngine;
public class dilGetir
{
    public static string dilSecimiGetir()
    {
        SystemLanguage dil = Application.systemLanguage;

        switch (dil)
        {
            case SystemLanguage.Turkish:
                return dilAdlari.TR;
            case SystemLanguage.English:
                return dilAdlari.EN;
            case SystemLanguage.Japanese:
                return dilAdlari.JA;
            default:
                return varsayilanDilSeciminiGetir();
        }
    }
    static string varsayilanDilSeciminiGetir()
    {
        return dilAdlari.TR;
    }
}