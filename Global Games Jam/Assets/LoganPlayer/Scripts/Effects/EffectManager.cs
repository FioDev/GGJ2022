using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{

    private float randomNumber = 0;
    private float storeRandom = 0;
    public Movement2D move2D;
    public TerrainMvmt terrainMove;
    public GameObject lamp;

    public int playerNumber;

    private void Update()
    {
        if (Input.GetButtonUp("Test" + playerNumber))
        {
            Debug.Log("Sent effect");
            RandomizeEffect();
        }
    }
    public void RandomizeEffect()
    {
        do
        {
            randomNumber = Random.Range(0, 6);
        }
        while (randomNumber == storeRandom);

        storeRandom = randomNumber;

        switch (randomNumber)
        {
            case 5:
                ReverseControls();
                break;
            case 4:
                ReverseScroll();
                break;
            case 3:
                NonStop();
                break;
            case 2:
                PlayerSpeedUp();
                break;
            case 1:
                ScrollSpeedUp();
                break;
            case 0:
                LightsOut();
                break;
        }
    }

    private void LightsOut() //The player's vision is reduced to a moving cone
    {
        print("Lights out!");
        lamp.SetActive(true);
        StartCoroutine(EffectWaitTime(0));
    }

    private void ScrollSpeedUp() //The stage scrolls faster
    {
        print("Scroll speed up!");
        terrainMove.MaximumSpeed = 6;
        StartCoroutine(EffectWaitTime(1));
    }

    private void PlayerSpeedUp() //The player's movement speed is increased
    {
        print("Speed up!");
        move2D.runSpeed *= 3;
        StartCoroutine(EffectWaitTime(2));
    }

    private void NonStop() //The player cannot stop moving
    {
        print("Nonstop!");
        move2D.nonStop = true;
        StartCoroutine(EffectWaitTime(3));
    }

    private void ReverseControls() //The player cannot stop moving
    {
        print("Reverse controls!");
        move2D.reverse = true;
        StartCoroutine(EffectWaitTime(4));
    }

    private void ReverseScroll() //Stage starts scrolling in reverse
    {
        print("Reverse!");
        terrainMove.direction *= -1;
    }

    IEnumerator EffectWaitTime(int ID)
    {
        yield return new WaitForSeconds(8);
        switch (ID)
        {
            case 4:
                move2D.reverse = false;
                break;
            case 3:
                move2D.nonStop = false;
                break;
            case 2:
                move2D.runSpeed = 30;
                break;
            case 1:
                terrainMove.MaximumSpeed = 4;
                break;
            case 0:
                lamp.SetActive(false);
                break;
        }
    }


}
