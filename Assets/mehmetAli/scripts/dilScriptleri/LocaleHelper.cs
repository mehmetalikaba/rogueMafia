using UnityEngine;
public class LocaleHelper
{
    public static string dilSecimiGetir()
    {
        SystemLanguage dil = Application.systemLanguage;

        switch (dil)
        {
            case SystemLanguage.Turkish:
                return LocaleApplication.TR;
            case SystemLanguage.English:
                return LocaleApplication.EN;
            case SystemLanguage.Japanese:
                return LocaleApplication.JA;
            default:
                return varsayilanDilSeciminiGetir();
        }
    }
    static string varsayilanDilSeciminiGetir()
    {
        return LocaleApplication.TR;
    }
}