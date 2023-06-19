using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonSfx : MonoBehaviour
{
    public AudioSource audio;

    public void playButtons()
    {
        audio.Play();
    }
}
