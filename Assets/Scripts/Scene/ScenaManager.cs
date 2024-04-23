using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.SceneManagement.SceneManager;

public class ScenaManager : MonoBehaviour
{
    private bool _isPlaying;

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void ChangeSceneWithSound(string scene)
    {
        if (!_isPlaying)
        {
            StartCoroutine(PlayAudioAndChangeScene(scene));
        }
    }
    
    private IEnumerator PlayAudioAndChangeScene(string scene)
    {
        _isPlaying = true;
        var audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);

        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
    }
}
