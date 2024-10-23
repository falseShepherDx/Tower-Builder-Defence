using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.Mathematics;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }
    private CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin perlin;
    private float timer, timerMax;
    private float startingIntensity;
    private void Awake()
    {
        Instance = this;
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        perlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        
    }

    private void Update()
    {
        if (timer < timerMax)
        {
            timer += Time.deltaTime;
            float amplitude = Mathf.Lerp(startingIntensity, 0, timer/timerMax);
            perlin.m_AmplitudeGain = amplitude;
        }
    }

    public void Shake(float intensity, float m_timer)
    {

        timerMax = m_timer;
        timer = 0f;
        startingIntensity = intensity;
        perlin.m_AmplitudeGain = intensity;
        

    }
}
