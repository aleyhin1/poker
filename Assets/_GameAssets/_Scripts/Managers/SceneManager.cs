using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);
    }

    public void LoadGameScene(Scene scene)
    {
        string sceneName = "";

        switch (scene)
        {
            case Scene.Login:
                sceneName = "LoginScene";
                break;
            case Scene.Menu:
                sceneName = "MainMenu";
                break;
            case Scene.Game:
                sceneName = "GameScene";
                break;
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}