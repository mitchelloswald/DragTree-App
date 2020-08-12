using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangerr : MonoBehaviour
{
    public void playSinglePlayer()
    {
        SceneManager.LoadScene(1);
    }
    public void playMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
