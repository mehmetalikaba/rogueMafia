using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class tusAtamalariMenu : MonoBehaviour
{
    public tusDizilimleri tusDizilimleri;
    public GameObject tusAtamaEkran, birTusaBasin;
    public Button ziplamaButonu;
    public Button atilmaButonu;
    public Button cakilmaButonu;
    public Button silah1Butonu;
    public Button silah2Butonu;

    public void Start()
    {
        ziplamaButonu.onClick.AddListener(() => StartRebinding("zipla"));
        atilmaButonu.onClick.AddListener(() => StartRebinding("atilma"));
        cakilmaButonu.onClick.AddListener(() => StartRebinding("cakilma"));
        silah1Butonu.onClick.AddListener(() => StartRebinding("silah1Tus"));
        silah2Butonu.onClick.AddListener(() => StartRebinding("silah2Tus"));

    }

    public void StartRebinding(string action)
    {
        tusAtamaEkran.SetActive(false);
        birTusaBasin.SetActive(true);
        StartCoroutine(RebindKey(action));
    }

    public IEnumerator RebindKey(string action)
    {
        yield return null;
        while (!Input.anyKeyDown)
        {
            yield return null;
        }

        foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(keyCode))
            {
                tusDizilimleri.SetKeyForAction(action, keyCode);
                birTusaBasin.SetActive(false);
                tusAtamaEkran.SetActive(true);
                break;
            }
        }
    }
}