using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DiedMenu : MonoBehaviour {

    [SerializeField] private Sprite _retryButton;
    [SerializeField] private Sprite _retryButtonDown;
    [SerializeField] private Sprite _watchAdButton;
    [SerializeField] private Sprite _watchAdButtonDown;
    [SerializeField] private Image _buttonImage;
    [SerializeField] private TextMeshProUGUI _adText;

    private Sprite activeSprite;
    private int adCount = 0;



    private void OnEnable() {
        adCount = GameManager.instance.watchedAdCount;

        if (adCount % 4 == 0) {
            _adText.text = "Continue from last checkpoint *WATCH AD*";
            activeSprite = _watchAdButton;
            _buttonImage.sprite = activeSprite;
        }
        else {
            _adText.text = "Continue from last checkpoint *FREE*";
            activeSprite = _retryButton;
            _buttonImage.sprite = activeSprite;
        }
    }



    public void EnableObject() {
        this.gameObject.SetActive(true);
    }

    public void StartOver() {
        GameManager.instance.StartAtBeginingLevel();
    }

    public void WatchAdReload() {
        if (adCount % 4 == 0) {
            GameManager.instance.watchedAdCount++;
            GameManager.instance.ShowAd();
        }
        else {
            GameManager.instance.watchedAdCount++;
            GameManager.instance.ReloadPlanetScene();
        }

    }

    public void ChangeIcon() {
        if (adCount % 4 == 0) {
            if (activeSprite == _watchAdButton) {
                activeSprite = _watchAdButtonDown;
                _buttonImage.sprite = activeSprite;
            }
            else {
                activeSprite = _watchAdButton;
                _buttonImage.sprite = activeSprite;
            }
        }
        else {
            if (activeSprite == _retryButton) {
                activeSprite = _retryButtonDown;
                _buttonImage.sprite = activeSprite;
            }
            else {
                activeSprite = _retryButton;
                _buttonImage.sprite = activeSprite;
            }

        }
    }


}
