using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public CollectableManager manager;

    public int value = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player") //If collided with player..
        {
            manager = collision.gameObject.GetComponent<CollectableManager>(); //Get players collectable manager
            manager.AddOrbs(value); //Add value to player
            Destroy(gameObject, 0);
        }
    }
}
