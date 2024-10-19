using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource audioSource;
    private float volume = 0.2f;
    public static MusicManager Instance;
    
    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = volume;
    }
    
    
    public void IncreaseVolume()
    {
        Debug.LogWarning("VOLUME INCREASED!");
        var volume1 = audioSource.volume;
        volume1 += 0.1f;
        audioSource.volume = volume1;
        volume = Mathf.Clamp01(volume1);
    }

    public void DecreaseVolume()
    {
        var volume1 = audioSource.volume;
        volume1 -= 0.1f;
        audioSource.volume = volume1;
        volume = Mathf.Clamp01(volume1);
    }

    public float GetVolume()
    {
        return volume;
    }
    
}
