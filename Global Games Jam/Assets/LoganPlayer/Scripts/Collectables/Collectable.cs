using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public CollectableManager manager;

    public int value = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2") //If collided with player..
        {
            // Check that this is the root player object and not one of the other colliders
            if (collision.TryGetComponent(out Player player))
            {
                // Play the particle effect
                player.PlayPowerupParticle(transform.position);

                manager = collision.gameObject.GetComponent<CollectableManager>(); //Get players collectable manager
                manager.AddOrbs(value); //Add value to player

                AudioManager.Instance.Play("PowerUp");
                Destroy(gameObject, 0); //Remove orb
            }
        }
    }
}
