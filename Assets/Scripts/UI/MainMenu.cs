using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenu : MonoBehaviour {

    [SerializeField] private Animator _sceneTransition;
    [SerializeField] private Button _loadButton;
    [SerializeField] private RectTransform _title;
    [SerializeField] private float titleEndYValue, _titleDuration;
    [SerializeField] private float _fadeDuration;
    [SerializeField] private CanvasGroup _buttonsCanvasGroup;



    private void Start() {
        _sceneTransition.SetTrigger("Open");
        StartCoroutine(menuTweening());

        LookForLoad();
    }

    private IEnumerator menuTweening() {
        yield return new WaitForSeconds(AnimationLength.ClipLength(_sceneTransition, "levelClosing"));
        _title.DOAnchorPosY(titleEndYValue, _titleDuration);
        yield return new WaitForSeconds(_titleDuration);
        _buttonsCanvasGroup.DOFade(1f, _fadeDuration).OnComplete(() => {
            _buttonsCanvasGroup.interactable = true;
        });
    }


    public void LoadGameScene() {
        StartCoroutine(WaitForTransition());
    }

    private IEnumerator WaitForTransition() {
        _sceneTransition.SetTrigger("Close");
        yield return new WaitForSeconds(AnimationLength.ClipLength(_sceneTransition, "levelClosing"));
        SceneLoader.Load(GameManager.instance.nextScene);
    }



    private void LookForLoad() {
        ColorBlock colors = _loadButton.colors;

        if (SaveSystem.Load() == null) {
            colors.normalColor = Color.HSVToRGB(0f, 0f, 0.25f);
            colors.disabledColor = Color.HSVToRGB(0f, 0f, 0.25f);
            _loadButton.colors = colors;
            _loadButton.interactable = false;
        }
        else {
            colors.normalColor = Color.HSVToRGB(0f, 0f, 1f);
            colors.disabledColor = Color.HSVToRGB(0f, 0f, 1f);
            _loadButton.colors = colors;
            _loadButton.interactable = true;
        }
    }


    public void Load() {
        GameManager.instance.Load();
    }


    public void MenuButtonSound() {
        AudioManager.instance.PlaySound("MenuButton");
    }

    public void LoadCreditsScene() {
        StartCoroutine(loadCredits());
    }
    private IEnumerator loadCredits() {
        _sceneTransition.SetTrigger("Close");
        yield return new WaitForSeconds(AnimationLength.ClipLength(_sceneTransition, "levelClosing"));
        SceneLoader.Load(Scenes.Credits.ToString());
    }


    public void Quit() {
        AppHelper.Quit();
    }

}
