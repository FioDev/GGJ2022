using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public float p1Score = 0;
    public float p2Score = 0;
    public int scoreGoal = 3;

    public bool update;

    public Text winText;

    private bool gameOver = false;
    public void UpdateScore(float ID)
    {
        //Only update once per scene reload
        if (!update)
        {
            if (ID == 1)
            {
                p2Score += 1;
                if (p2Score == 3)
                {
                    DeclareWinner(2);
                }
            }
            else if (ID == 2)
            {
                p1Score += 1f;
                if (p1Score == 3)
                {
                    DeclareWinner(1);
                }
            }

            update = true;
            if (!gameOver)
            {
                ReloadScene();
            }
            else
            {
                EndGame();
            }
        }

    }

    public void DeclareWinner(int ID)
    {
        if (ID == 1)
        {
            //P1 wins!
            winText.text = "Player 1 Wins!";
            winText.color = Color.magenta;
        }
        else
        {
            //P2 wins!
            winText.text = "Player 2 Wins!";
            winText.color = Color.cyan;

        }

        gameOver = true;
    }

    public void ReloadScene()
    {
        StartCoroutine(Reset());
    }

    public void EndGame()
    {
        StartCoroutine(End());
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(1);
        update = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator End()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        //Destroy(gameObject, 0); //Remove this, this will be used once main menu exists

        //Temp solution below
        winText.text = "";
        p1Score = 0;
        p2Score = 0;
    }
}
