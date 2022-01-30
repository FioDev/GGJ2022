using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public CollectableManager manager;

    public int value = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Player Entered Orb");
        if(collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2") //If collided with player..
        {
            // Play the particle effect
            if (collision.TryGetComponent(out Player player))
            {
                player.PlayPowerupParticle(transform.position);
            }

            Debug.Log("Player Entered Orb");
            manager = collision.gameObject.GetComponent<CollectableManager>(); //Get players collectable manager
            manager.AddOrbs(value); //Add value to player
            Destroy(gameObject, 0); //Remove orb
        }
    }
}
