using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimationLength {

    public static float ClipLength(Animator anim, string clipName) {
        AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
        foreach (AnimationClip ac in clips) { if (ac.name == clipName) return ac.length; }
        return 0.0f;
    }

}
