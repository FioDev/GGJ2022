using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject BloodSplatter;

    public void KillPlayer()
    {
        Debug.Log($"Killed {transform.tag}");

        Instantiate(BloodSplatter, transform.position, Quaternion.identity, null);
        //Destroy(gameObject);
    }
}
