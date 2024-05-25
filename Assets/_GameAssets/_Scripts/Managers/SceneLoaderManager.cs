 using UnityEngine.SceneManagement;

public static class SceneLoaderManager
{
    public static void LoadMainMenuScene()
    {
        SceneManager.LoadScene(2);
    }

    public static void LoadGameScene()
    {
        SceneManager.LoadScene(3);
    }
}