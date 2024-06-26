using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GrafikAyari : MonoBehaviour
{
    public TMP_Dropdown cozunurlukSecenekler;

    Resolution[] resolutions;
    List<Resolution> resolutionList;

    float currentRefreshRate;
    int currentResolutionIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionList = new List<Resolution>();

        cozunurlukSecenekler.ClearOptions();
        currentRefreshRate = Screen.currentResolution.refreshRate;

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].refreshRate==currentRefreshRate)
            {
                resolutionList.Add(resolutions[i]); 
            }
        }

        List<string> options = new List<string>();
        for (int i = 0; i < resolutionList.Count; i++)
        {
            string resOp = resolutionList[i].width + "x" + resolutionList[i].height + " " + resolutionList[i].refreshRate + "Hz";
            options.Add(resOp);
            if (resolutionList[i].width ==Screen.width&&resolutionList[i].height ==Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        cozunurlukSecenekler.AddOptions(options);
        cozunurlukSecenekler.value = currentResolutionIndex;
        cozunurlukSecenekler.RefreshShownValue();
    }

    public void SetRes(int resolutionIndex)
    {
        Resolution res = resolutionList[resolutionIndex];
        Screen.SetResolution(res.width,res.height,true);
    }

}
