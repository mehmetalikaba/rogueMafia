using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class silahSecimleri
{
    public silahIconlariTest silahIconlariTest;

    public enum silahTurleri { menzilli, rogue, yakin }
    public silahTurleri silahinTuru;

    public enum silahAdlari { katana, nunchaku, kusarigama, kunai, tessen, ryuPistol, ryuUzi, yumi }
    public silahAdlari silahAdi;

    public enum IkonAdlari { KatanaIcon, NunchakuIcon, KusarigamaIcon, KunaiIcon, TessenIcon, RyuPistolIcon, RyuUziIcon, YumiIcon }
    public IkonAdlari ikonAdi;

    private Dictionary<IkonAdlari, Sprite> iconDictionary;

    public void InitializeIconDictionary()
    {
        iconDictionary = new Dictionary<IkonAdlari, Sprite>
        {
            { IkonAdlari.KatanaIcon, silahIconlariTest.katanaIcon },
            { IkonAdlari.NunchakuIcon, silahIconlariTest.nunchakuIcon },
            { IkonAdlari.KusarigamaIcon, silahIconlariTest.kusarigamaIcon },
            { IkonAdlari.KunaiIcon, silahIconlariTest.kunaiIcon },
            { IkonAdlari.TessenIcon, silahIconlariTest.tessenIcon },
            { IkonAdlari.RyuPistolIcon, silahIconlariTest.ryuPistolIcon },
            { IkonAdlari.RyuUziIcon, silahIconlariTest.ryuUziIcon },
            { IkonAdlari.YumiIcon, silahIconlariTest.yumiIcon },
        };
    }

    public Sprite GetIcon()
    {
        if (iconDictionary == null)
        {
            InitializeIconDictionary();
        }

        if (iconDictionary.TryGetValue(ikonAdi, out Sprite icon))
        {
            return icon;
        }
        else
        {
            Debug.LogWarning("Icon not found for: " + ikonAdi);
            return null;
        }
    }
}
