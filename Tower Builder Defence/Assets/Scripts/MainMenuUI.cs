using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        transform.Find("playBtn").GetComponent<Button>().onClick.AddListener((() =>
        {
            
            GameSceneManager.LoadScene(GameSceneManager.GameScenes.GameScene);
        }));
        transform.Find("quitBtn").GetComponent<Button>().onClick.AddListener(Quit);
    }

    public void Quit()
    {
        Application.Quit();
    }
}

