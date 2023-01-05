using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip[] sound;
    public void ButtonSound()
    {
        GetComponent<AudioSource>().PlayOneShot(sound[0]);
    }

    public void GetScoreSound()
    {
        GetComponent<AudioSource>().PlayOneShot(sound[1]);
    }
}
