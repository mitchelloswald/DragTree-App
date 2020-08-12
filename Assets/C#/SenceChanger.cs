using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void playSinglePlayer()
    {
        SceneManager.LoadScene("SinglePlayerTree");
    }
    public void playMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
