using UnityEngine;
using DG.Tweening;

public static class AudioFade {

    public static void FadeOut(AudioSource audio, float fadeTime) {
        DOTween.To(() => audio.volume, x => audio.volume = x, 0f, fadeTime);
    }

    public static void FadeIn(AudioSource audio, float fadeTime) {
        DOTween.To(() => audio.volume, x => audio.volume = x, 1f, fadeTime);
    }


}
