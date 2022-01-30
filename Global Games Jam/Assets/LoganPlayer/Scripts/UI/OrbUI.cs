using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrbUI : MonoBehaviour
{
    [Range(0, 10)] public int requiredCollectables = 5; //Number of orbs we need to create UI for

    public Sprite offSprite;
    public Sprite onSprite;

    public int id;

    private void Awake()
    {
        //Set as many UI nodes active as is needed, per required number of collectables for sending effects
        if (id == 1)
        {
            for (int i = 0; i < requiredCollectables; i++)
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        else
        {
            for (int i = 9; i > (9 - requiredCollectables); i--)
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    public void UpdateOrbs(int value)
    {
        //If effect is sent, turn all UI orbs off
        if (value >= requiredCollectables)
        {
            //Set all orbs to off
            for (int i = 0; i < 10; i++)
            {
                gameObject.transform.GetChild(i).gameObject.GetComponent<Image>().sprite = offSprite;
            }
        }
        else
        {
            //Turn on orbs, player 2 does this in reverse order
            if (id == 1)
            {
                for (int i = 0; i < value; i++)
                {
                    gameObject.transform.GetChild(i).gameObject.GetComponent<Image>().sprite = onSprite;
                }
            }
            else
            {
                for (int i = 9; i > (9 - value); i--)
                {
                    gameObject.transform.GetChild(i).gameObject.GetComponent<Image>().sprite = onSprite;
                }
            }
        }
    }
}
