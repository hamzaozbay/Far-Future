using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceMusic : MonoBehaviour {

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _musicStart;
    [SerializeField] private AudioClip _musicLoop;


    private void Start() {
        AudioManager.instance.MusicOnOff();

        if (GameManager.instance.previousScene == Scenes.MainMenu) {
            AudioFade.FadeIn(_audioSource, 4f);
            StartCoroutine(playLoop());
        }
        else {
            _audioSource.clip = _musicLoop;
            _audioSource.Play();
            AudioFade.FadeIn(_audioSource, 4f);
        }

    }

    private IEnumerator playLoop() {
        yield return new WaitForSeconds(_musicStart.length - 0.1f);
        _audioSource.clip = _musicLoop;
        _audioSource.Play();
    }

    public void FadeInMusic(float fadeTime) {
        AudioFade.FadeIn(_audioSource, fadeTime);
    }

    public void FadeOutMusic(float fadeTime) {
        AudioFade.FadeOut(_audioSource, fadeTime);
    }


    public void PauseMusic() {
        if (_audioSource.isPlaying)
            _audioSource.Pause();
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
