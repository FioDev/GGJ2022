using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public int p1Score = 0;
    public int p2Score = 0;
    public int scoreGoal = 3;
    public void UpdateScore(float ID)
    {
        if (ID == 1)
        {
            p2Score += 1;
            if (p2Score == 3)
            {
                DeclareWinner(2);
            }
        }
        else
        {
            p1Score += 1;
            if (p1Score == 3)
            {
                DeclareWinner(1);
            }
        }

        ReloadScene();
    }

    public void DeclareWinner(int ID)
    {
        if (ID == 1)
        {
            //P1 wins!
            print("Player 1 wins!");
        }
        else
        {
            //P2 wins!
            print("Player 2 wins!");
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
