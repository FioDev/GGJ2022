using UnityEngine;

[RequireComponent(typeof(Collider))]
public class KillOnTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        Destroy(collision.gameObject);
    }
}
