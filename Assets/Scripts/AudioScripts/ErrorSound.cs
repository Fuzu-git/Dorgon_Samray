using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ErrorSound : MonoBehaviour
{
    public List<AudioClip> ErrorSoundList = new List<AudioClip>();
    public AudioSource errorAudioSource; 

    void PlayErrorSound()
    {
        var randomSound = Random.Range(0, ErrorSoundList.Count);
        errorAudioSource.clip = ErrorSoundList[randomSound]; 
        errorAudioSource.Play();
    }
}
