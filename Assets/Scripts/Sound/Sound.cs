using UnityEngine.Audio;
using UnityEngine;


[System.Serializable]
public class Sound {

    public string name;
    public AudioClip defaultClip;
    public AudioClip[] clips;
    public bool loop = false;

    [Range(0f, 1f)]
    public float volume = 1f;

    [HideInInspector]
    public AudioSource audioSource;
    


}
