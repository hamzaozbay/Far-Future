using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour {

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _fadeTime = 4f;



    private void Start() {
        AudioManager.instance.MusicOnOff();
        AudioFade.FadeIn(_audioSource, _fadeTime);
    }


    public void FadeOutMusic(float fadeTime) {
        StartCoroutine(stopMusic(fadeTime));
    }

    private IEnumerator stopMusic(float fadeTime) {
        AudioFade.FadeOut(_audioSource, fadeTime);
        yield return new WaitForSeconds(fadeTime + 0.1f);
        _audioSource.Stop();
    }


    public void PauseMusic() {
        if (_audioSource.isPlaying) {
            _audioSource.Pause();
        }
    }

    public void ResumeMusic() {
        if (_audioSource.time != 0f && !_audioSource.isPlaying) {
            _audioSource.UnPause();
        }
    }

    public void PlayMusic() {
        _audioSource.Play();
    }

}
