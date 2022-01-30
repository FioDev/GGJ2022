using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadScore : MonoBehaviour
{
    public int ID;

    private Text score;

    private ScoreManager scoreMan;

    public void Awake()
    {
        score = gameObject.GetComponent<Text>();
        scoreMan = GameObject.FindGameObjectWithTag("ScoreKeeper").GetComponent<ScoreManager>();

        if (ID == 1)
        {
            score.text = scoreMan.p1Score.ToString();
        }
        else
        {
            score.text = scoreMan.p2Score.ToString();
        }

    }
}
