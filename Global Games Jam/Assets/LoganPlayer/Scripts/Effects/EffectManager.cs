using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{

    private float randomNumber = 0;
    private float storeRandom = 0;
    public Movement2D move2D;
    public TerrainMvmt terrainMove;
    public void RandomizeEffect()
    {

        do
        {
            randomNumber = Random.Range(0, 4);
        }
        while (randomNumber == storeRandom);

        storeRandom = randomNumber;

        switch (randomNumber)
        {
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

    private void NonStop() //The player c annot stop moving
    {
        print("Nonstop!");
        move2D.nonStop = true;
        StartCoroutine(EffectWaitTime(3));
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

                break;
        }
    }


}
