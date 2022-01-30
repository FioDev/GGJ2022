using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    private void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            Destroy(GameObject.FindGameObjectWithTag("ScoreKeeper"), 0);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

}
