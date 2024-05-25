using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirebaseUIManager : MonoBehaviour
{
    public static FirebaseUIManager instance;

    //Screen object variables
    public GameObject loginUI;
    public GameObject registerUI;
    public GameObject userDataUI;
    // public GameObject scoreboardUI;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }

    //Functions to change the login screen UI

    public void ClearScreen() //Turn off all screens
    {
        loginUI.SetActive(false);
        registerUI.SetActive(false);
        userDataUI.SetActive(false);
        // scoreboardUI.SetActive(false);
    }

    public void LoginScreen() //Back button
    {
        ClearScreen();
        loginUI.SetActive(true);
    }
    public void RegisterScreen() // Regester button
    {
        ClearScreen();
        registerUI.SetActive(true);
    }

    public void UserDataScreen() //Logged in
    {
        ClearScreen();
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        userDataUI.SetActive(true);
    }

    /*  public void ScoreboardScreen() //Scoreboard button
      {
          ClearScreen();
          scoreboardUI.SetActive(true);
      }
      */
}
