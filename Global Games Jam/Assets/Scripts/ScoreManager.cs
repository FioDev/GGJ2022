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
    public void UpdateScore(float ID)
    {
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
            ReloadScene();
        }

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
        StartCoroutine(Reset());
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(1);
        update = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
