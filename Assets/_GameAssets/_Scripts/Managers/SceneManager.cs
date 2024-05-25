using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SceneManager : MonoSingleton<SceneManager>
{
    private void Awake()
    {
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