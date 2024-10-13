using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameSceneManager
{
    public enum GameScenes
    {
        GameScene,
        MainMenu
    }
    public static void LoadScene(GameScenes scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
    
}
