using UnityEngine;

[RequireComponent(typeof(Collider))]
public class KillOnTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.KillPlayer();
        }
    }
}
