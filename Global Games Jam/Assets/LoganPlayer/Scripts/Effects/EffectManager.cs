using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectManager : MonoBehaviour
{

    private float randomNumber = 0;
    private float storeRandom = 0;
    public Transform player;
    public Movement2D move2D;
    public TerrainMvmt terrainMove;
    public GameObject lamp;

    public GameObject playerEffect;
    private Text playerEffectText;
    public int playerNumber;

    private void Awake()
    {
        playerEffectText = playerEffect.GetComponent<Text>();
    }

    private void Update()
    {
        if (Input.GetButtonUp("Test" + playerNumber))
        {
            RandomizeEffect();
        }
    }
    public void RandomizeEffect()
    {
        //Select an effect at random, can't be last effect used
        do
        {
            randomNumber = Random.Range(0, 7);
        }
        while (randomNumber == storeRandom);

        storeRandom = randomNumber;

        switch (randomNumber)
        {
            case 6:
                Grow();
                break;
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
        playerEffect.SetActive(true);
        FindObjectOfType<AudioManager>().Play("LightsOut");
        playerEffectText.text = "Lights, Sabotaged!";
        lamp.SetActive(true);
        StartCoroutine(EffectWaitTime(0));
    }

    private void ScrollSpeedUp() //The stage scrolls faster
    {
        playerEffect.SetActive(true);
        FindObjectOfType<AudioManager>().Play("ScrollSpeedUp");
        playerEffectText.text = "Keep the pace!";
        terrainMove.MaximumSpeed = 6;
        StartCoroutine(EffectWaitTime(1));
    }

    private void PlayerSpeedUp() //The player's movement speed is increased
    {
        playerEffect.SetActive(true);
        FindObjectOfType<AudioManager>().Play("SpeedUp");
        playerEffectText.text = "Speed, Maximum!";
        move2D.runSpeed *= 3;
        StartCoroutine(EffectWaitTime(2));
    }

    private void NonStop() //The player cannot stop moving
    {
        playerEffect.SetActive(true);
        FindObjectOfType<AudioManager>().Play("NonStop");
        playerEffectText.text = "Can't stop!";
        move2D.nonStop = true;
        StartCoroutine(EffectWaitTime(3));
    }

    private void ReverseControls() //The player cannot stop moving
    {
        playerEffect.SetActive(true);
        FindObjectOfType<AudioManager>().Play("ReverseControls");
        playerEffectText.text = "Controls, Inverted!";
        move2D.reverse = true;
        StartCoroutine(EffectWaitTime(4));
    }

    private void Grow() //The player gets larger
    {
        playerEffect.SetActive(true);
        FindObjectOfType<AudioManager>().Play("Grow");
        playerEffectText.text = "Gigantic!";
        player.transform.position += new Vector3(0.0f, 1.0f, 0.0f);
        player.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        StartCoroutine(EffectWaitTime(5));
    }

    private void ReverseScroll() //Stage starts scrolling in reverse
    {
        playerEffect.SetActive(true);
        FindObjectOfType<AudioManager>().Play("ReverseScroll");
        playerEffectText.text = "Going in reverse!";
        terrainMove.direction *= -1;
        StartCoroutine(EffectWaitTime(6));
    }

    IEnumerator EffectWaitTime(int ID) //Wait 8 seconds then remove effect
    {
        yield return new WaitForSeconds(8);
        playerEffect.SetActive(false);
        switch (ID)
        {
            case 6:
                terrainMove.direction *= -1;
                break;
            case 5:
                player.localScale = new Vector3(1, 1, 1);
                break;
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
