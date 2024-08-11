using UnityEngine;
using UnityEngine.UI;

public class antikaYadigarKontrol : MonoBehaviour
{
    public antikaYadigarOzellikleri[] antikaObjesi, yadigarObjesi;
    public Image[] antikalarImage, yadigarlarImage;
    public bool[] antikaSlotBos = new bool[3], yadigarSlotBos = new bool[3];

    void Start()
    {
        for (int i = 0; i < antikaSlotBos.Length; i++)
            antikaSlotBos[i] = true;
        for (int i = 0; i < yadigarSlotBos.Length; i++)
            yadigarSlotBos[i] = true;
    }

    void Update()
    {

    }

    public void antikaNeYapacak()
    {

    }

    public void yadigarNeYapacak()
    {

    }
}
