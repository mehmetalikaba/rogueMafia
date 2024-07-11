using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class kameraSarsinti : MonoBehaviour
{
    public GameObject normalEffect, fightEffect;
    CinemachineVirtualCamera cinemachineVirtualCamera;
    float shakeIntensity = 1.5f;
    float shakeTime = 0.25f;

    float timer;
    CinemachineBasicMultiChannelPerlin cbmcp;
    // Start is called before the first frame update
    private void Awake()
    {
        cinemachineVirtualCamera=GetComponent<CinemachineVirtualCamera>();
    }
    void Start()
    {
        StopShake();
    }

    public void Shake()
    {
        CinemachineBasicMultiChannelPerlin cbmcp = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cbmcp.m_AmplitudeGain = shakeIntensity;
        normalEffect.SetActive(false);
        fightEffect.SetActive(true);
        timer = shakeTime;
    }
    public void StopShake()
    {
        CinemachineBasicMultiChannelPerlin cbmcp = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cbmcp.m_AmplitudeGain = 0;
        normalEffect.SetActive(true);
        fightEffect.SetActive(false);
        timer = shakeTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        if(timer>0)
        {
            timer -= Time.deltaTime;
            if(timer<=0)
            {
                StopShake();
            }
        }
    }
}
