using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
            SceneManager.LoadScene(sceneToLoad);
        /*if (other.CompareTag("Player") && !other.isTrigger)
        {
            SceneManager.LoadScene(sceneToLoad);

        }*/
    }
}
