using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Serialization;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;
    public static SoundManager Instance { get; private set; }
    private float volume = 0.5f;
    
    
    public enum Sound
    {
        BuildingConstruction,
        EnemyHit,
        BuildingDamaged,
        EnemyDie,
        GameOver,
        BuildingDestruction,
        BuildingRepair,
        Pause,
        Unpause,
        ButtonClick
    }

    public Dictionary<Sound, AudioClip> dictionarySounds;
    
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (Instance == null)
        {
            Instance = this;
        }

        dictionarySounds = new Dictionary<Sound, AudioClip>();

        foreach (Sound sound in Enum.GetValues(typeof(Sound)))
        {
            dictionarySounds[sound] = Resources.Load<AudioClip>(sound.ToString());
        }
        volume = audioSource.volume;
        
    }

    public void PlaySound(Sound sound)
    {
        audioSource.PlayOneShot(dictionarySounds[sound],volume);
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
