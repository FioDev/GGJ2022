using UnityEngine;
using UnityEditor;

public class Player : MonoBehaviour
{
    public GameObject BloodSplatter;
    public string EffectsLayerForThisPlayer;
    protected bool isDead = false;

    public void KillPlayer()
    {
        if (!isDead)
        {
            isDead = true;
            Debug.Log($"Killed {transform.tag}");
            GameObject g = Instantiate(BloodSplatter, transform.position, Quaternion.identity, null);
            g.layer = LayerMask.NameToLayer(EffectsLayerForThisPlayer);

            Destroy(gameObject);
        }
    }
}
