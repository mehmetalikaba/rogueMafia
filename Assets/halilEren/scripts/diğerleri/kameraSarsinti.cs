using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class kameraSarsinti : MonoBehaviour
{
    CinemachineVirtualCamera cinemachineVirtualCamera;
    float shakeIntensity = 1f;
    float shakeTime = 0.2f;

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

        timer = shakeTime;
    }
    public void StopShake()
    {
        CinemachineBasicMultiChannelPerlin cbmcp = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cbmcp.m_AmplitudeGain = 0;

        timer = shakeTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
