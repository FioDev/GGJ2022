using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class menu : MonoBehaviour
{
    public void PlayGame()
    {
        FindObjectOfType<AudioManager>().Play("OnGameStart");
        SceneManager.LoadScene("CynScene");
    }

    public void QuitGame()
    {
        FindObjectOfType<AudioManager>().Play("Quit");
        Application.Quit();
    }
}
