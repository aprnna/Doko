using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.SceneManagement.SceneManager;

public class ScenaManager : MonoBehaviour
{
    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void ChangeSceneWithButton(string scene)
    {
        gameObject.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene(scene);
    }
}
