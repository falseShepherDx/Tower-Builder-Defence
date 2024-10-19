using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;

public class OptionsUI : MonoBehaviour
{
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private MusicManager musicManager;
    [SerializeField] private TextMeshProUGUI soundInfoText;
    [SerializeField] private Button soundIncreaseBtn;
    [SerializeField] private Button soundDecreaseBtn;
    [SerializeField] private Button musicIncreaseBtn;
    [SerializeField] private Button musicDecreaseBtn;
    [SerializeField] private TextMeshProUGUI musicInfoText;
    
    private void Awake()
    {
        soundIncreaseBtn.onClick.AddListener((() =>
        {
            SoundManager.Instance.PlaySound(SoundManager.Sound.ButtonClick);
            SoundManager.Instance.IncreaseVolume();
            Debug.Log("increase");
            UpdateText();
        }));
        soundDecreaseBtn.onClick.AddListener((() =>
        {
            SoundManager.Instance.PlaySound(SoundManager.Sound.ButtonClick);
            SoundManager.Instance.DecreaseVolume();
            Debug.Log("decrease");
            UpdateText();
        }));
        transform.Find("musicIncreaseBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySound(SoundManager.Sound.ButtonClick);
            MusicManager.Instance.IncreaseVolume();
            UpdateText();
        });
        transform.Find("musicDecreaseBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySound(SoundManager.Sound.ButtonClick);
            MusicManager.Instance.DecreaseVolume();
            UpdateText();
        });
        transform.Find("mainMenuBtn").GetComponent<Button>().onClick.AddListener((() =>
        {
            SoundManager.Instance.PlaySound(SoundManager.Sound.ButtonClick);
            Time.timeScale = 1f;
            GameSceneManager.LoadScene(GameSceneManager.GameScenes.MainMenu);
        }));
        
        
    }
    
    private void Start()
    {
        UpdateText();
        gameObject.SetActive(false);
    }
    
    public void ToggleOptions()
    {
        gameObject.SetActive(!gameObject.activeSelf);

        if (gameObject.activeSelf)
        {   
            SoundManager.Instance.PlaySound(SoundManager.Sound.Pause);
            Time.timeScale = 0f;
        }
        else
        {
            SoundManager.Instance.PlaySound(SoundManager.Sound.Unpause);
            Time.timeScale = 1;
        }
    }

    public void UpdateText()
    
    {
        Debug.Log("UPDATETEXTUPDATETEXT");
        soundInfoText.SetText(Mathf.RoundToInt(SoundManager.Instance.GetVolume()*10).ToString());
        musicInfoText.SetText(Mathf.RoundToInt(MusicManager.Instance.GetVolume()*10).ToString());
    }
}

