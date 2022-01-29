using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbUI : MonoBehaviour
{
    [Range(0, 10)] public int requiredCollectables = 5; //Number of orbs we need to create UI for

    public Sprite offSprite;
    public Sprite onSprite;

    private void Awake()
    {
        for (int i = 0; i < requiredCollectables; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void UpdateOrbs(int value)
    {
        if(value >= requiredCollectables)
        {
            //Set all orbs to off
            for (int i = 0; i < requiredCollectables; i++)
            {
                gameObject.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().sprite = offSprite;
            }
        }  
        else
        {
            for (int i = 0; i < value; i++)
            {
                gameObject.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().sprite = onSprite;
            }
        }



    }




}
