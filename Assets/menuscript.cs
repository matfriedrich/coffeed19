using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuscript : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("mapscene");
        Debug.Log("Loading game scene");
    }

    public void mainmenuload()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void exit()
    {
        Application.Quit();
        Debug.Log("quiting the game");
    }


}
