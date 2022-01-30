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

    public void PlayPowerupParticle(Vector3 pos)
    {
        GameObject g = Instantiate(PowerupParticle, pos, PowerupParticle.transform.rotation, null);
        g.layer = LayerMask.NameToLayer(EffectsLayerForThisPlayer);
    }

    public void KillPlayer()
    {
        if (!isDead)
        {
            Debug.Log("Kill");
            isDead = true;
            FindObjectOfType<AudioManager>().Play("PlayerDeath");
            Debug.Log($"{transform.tag} survived for {TimeSurvivedSeconds.ToString("0.0")} seconds");
            GameObject g = Instantiate(BloodSplatter, transform.position, Quaternion.identity, null);
            g.layer = LayerMask.NameToLayer(EffectsLayerForThisPlayer);

            Destroy(gameObject);
        }
    }
}
