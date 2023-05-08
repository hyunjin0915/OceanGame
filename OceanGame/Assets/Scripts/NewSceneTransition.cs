using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NewSceneTransition :MonoBehaviour
{
    public string sceneToLoad;
   
    public void SceneTransition()
    {
        PlayerController.Instance.fromMapName = PlayerController.Instance.toMapName;
        PlayerController.Instance.toMapName = sceneToLoad;
        SceneManager.LoadScene(sceneToLoad);
    }
}
