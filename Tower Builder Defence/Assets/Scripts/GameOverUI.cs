using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI Instance
    {
        get; private set;
        
    }

   

    private void Awake()
    {
        Instance = this;
        Hide();
        transform.Find("RetryButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            GameSceneManager.LoadScene(GameSceneManager.GameScenes.GameScene);
        });
        transform.Find("MainMenuButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            GameSceneManager.LoadScene(GameSceneManager.GameScenes.MainMenu);
        });
        
       
    }

    public void Show()
    {
        gameObject.SetActive(true);
        transform.Find("numberOfWavesText").GetComponent<TextMeshProUGUI>().SetText("You Survived " +EnemyWaveManager.Instance.GetWaveNumber()+
            " Number Of Waves! ");
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
