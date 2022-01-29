using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    public int requiredCollectables = 5; //Number of orbs a player needs to collect to send a debuff

    private int currentCollectables = 0; //Number of orbs player currently has

    private void Awake()
    {
       currentCollectables = 0; //Reset orbs each round
    }

    public void AddOrbs(int orbValue)
    {
        currentCollectables += orbValue;

        if(currentCollectables >= requiredCollectables)
        {
            //Send Debuff here..
            Debug.Log("Send debuff!");

            currentCollectables = 0;
        }
    }

}
