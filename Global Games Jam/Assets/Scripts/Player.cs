using UnityEngine;
using UnityEditor;

public class Player : MonoBehaviour
{
    public GameObject BloodSplatter;
    public GameObject PowerupParticle;
    public string EffectsLayerForThisPlayer;
    protected bool isDead = false;

    public float TimeSurvivedSeconds = 0;

    private void Update()
    {
        TimeSurvivedSeconds += Time.deltaTime;
    }

    public void PlayPowerupParticle()
    {
        GameObject g = Instantiate(PowerupParticle, transform.position, Quaternion.identity, null);
        g.layer = LayerMask.NameToLayer(EffectsLayerForThisPlayer);
    }

    public void KillPlayer()
    {
        if (!isDead)
        {
            Debug.Log("Kill");
            isDead = true;
            Debug.Log($"{transform.tag} survived for {TimeSurvivedSeconds.ToString("0.0")} seconds");
            GameObject g = Instantiate(BloodSplatter, transform.position, Quaternion.identity, null);
            g.layer = LayerMask.NameToLayer(EffectsLayerForThisPlayer);

            Destroy(gameObject);
        }
    }
}
