using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfCameraDie : MonoBehaviour
{

    private ScoreManager scoreKeeper;

    private void Awake()
    {
        scoreKeeper = GameObject.FindGameObjectWithTag("ScoreKeeper").GetComponent<ScoreManager>();
    }

    // BoxCollider2D bxCol;

    // // Start is called before the first frame update
    // void Start()
    // {
    //     bxCol = GetComponent<BoxCollider2D>();
    //     Debug.Log(bxCol.OverlapCollider());
    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     if(bxCol.IsTouchingLayers(LayerMask.GetMask("Player"))){
    //         Debug.Log("HELLO");
    //     }
    // }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.TryGetComponent(out Player player))
        {
            var id = col.GetComponent<Movement2D>().playerNumber;
            scoreKeeper.UpdateScore(id);
            player.KillPlayer();
        }
    }

}