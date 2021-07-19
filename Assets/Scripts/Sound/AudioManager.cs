using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class AudioManager : MonoBehaviour {


    #region Singleton
    public static AudioManager instance;
    private void Awake() {
        if (instance != this) {
            if (instance != null) Destroy(instance.gameObject); //Destroy old Audio Manager.

            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        SetupAudioManager();
    }
    #endregion

    [SerializeField] private Sound[] _sounds;

    [SerializeField] private bool _effectsOn = true;
    [SerializeField] private bool _musicOn = true;



    private void SetupAudioManager() {
        foreach (Sound s in _sounds) {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.defaultClip;
            s.audioSource.volume = s.volume;
            s.audioSource.playOnAwake = false;
        }
    }



    public void PlaySound(string name) {
        Sound s = Array.Find(_sounds, sound => sound.name == name);
        s.audioSource.loop = s.loop;
        s.audioSource.Play();
    }

    public void PlayRandomSound(string name) {
        Sound s = Array.Find(_sounds, sound => sound.name == name);
        s.audioSource.clip = s.clips[UnityEngine.Random.Range(0, s.clips.Length - 1)];
        s.audioSource.loop = s.loop;
        s.audioSource.Play();
    }

    public void PlaySoundWithIndex(string name, int index) {
        Sound s = Array.Find(_sounds, sound => sound.name == name);
        s.audioSource.clip = s.clips[index];
        s.audioSource.loop = s.loop;
        s.audioSource.Play();
    }


    public void EffecsOnOffButton() {
        _effectsOn = !_effectsOn;

        if (!_effectsOn) {
            foreach (Sound s in _sounds) {
                s.audioSource.enabled = false;
                _effectsOn = false;
            }
        }
        else {
            foreach (Sound s in _sounds) {
                s.audioSource.enabled = true;
                _effectsOn = true;
            }
        }
    }

    public void MusicOnOffButton() {
        _musicOn = !_musicOn;
        MusicOnOff();
    }


    public void MusicOnOff() {
        GameObject[] backgroundMusics = GameObject.FindGameObjectsWithTag("BackgroundMusic");
        if (!_musicOn) {
            foreach (GameObject g in backgroundMusics) {
                g.GetComponent<AudioSource>().enabled = false;
                _musicOn = false;
            }
        }
        else {
            foreach (GameObject g in backgroundMusics) {
                g.GetComponent<AudioSource>().enabled = true;
                _musicOn = true;
            }
        }
    }


    public bool IsEffectsOn { get { return _effectsOn; } }
    public bool IsMusicOn { get { return _musicOn; } }

}





