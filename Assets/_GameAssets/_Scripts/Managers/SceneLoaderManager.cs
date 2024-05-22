using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SceneLoaderManager : MonoSingleton<SceneLoaderManager>
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene(3);
    }
}