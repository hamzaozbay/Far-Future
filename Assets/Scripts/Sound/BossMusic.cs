using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMusic : MonoBehaviour {

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _bossMusicStart;
    [SerializeField] private AudioClip _bossMusicLoop;
    [SerializeField] private AudioClip _bossWin;



    public void FadeInMusic(float fadeTime) {
        AudioFade.FadeIn(_audioSource, fadeTime);
        PlayBossMusic();
    }

    public void PlayBossMusic() {
        _audioSource.Play();
        StartCoroutine(waitForLoop());
    }
    private IEnumerator waitForLoop() {
        yield return new WaitForSeconds(_bossMusicStart.length);
        _audioSource.clip = _bossMusicLoop;
        _audioSource.Play();
    }


    public void Win(float fadeTime) {
        StartCoroutine(BossWin(fadeTime));
    }
    private IEnumerator BossWin(float fadeTime) {
        AudioFade.FadeOut(_audioSource, fadeTime);
        yield return new WaitForSeconds(fadeTime);
        _audioSource.volume = 1f;
        _audioSource.clip = _bossWin;
        _audioSource.loop = false;
        _audioSource.Play();
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
