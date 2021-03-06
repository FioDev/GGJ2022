using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    public OrbUI orbUIManager;
    public EffectManager effectMan;
    public int requiredCollectables; //Number of orbs a player needs to collect to send a debuff

    private int currentCollectables = 0; //Number of orbs player currently has


    private void Awake()
    {
        requiredCollectables = orbUIManager.requiredCollectables;
        currentCollectables = 0; //Reset orbs each round
    }

    public void AddOrbs(int orbValue)
    {
        currentCollectables += orbValue; //Player gains debuff progress based on orb value

        orbUIManager.UpdateOrbs(currentCollectables);

        if (currentCollectables >= requiredCollectables)
        {
            //Send Debuff here..
            effectMan.RandomizeEffect();
            currentCollectables = 0;
        }
    }

}
