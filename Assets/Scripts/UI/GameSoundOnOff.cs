using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSoundOnOff : MonoBehaviour {

    [SerializeField] private Sprite _onSprite;
    [SerializeField] private Sprite _offSprite;
    [SerializeField] private Sprite _onDownSprite;
    [SerializeField] private Sprite _offDownSprite;
    [SerializeField] private SoundType _type;


    private bool _on = true;
    private Image _image;



    private void Start() {
        _image = GetComponent<Image>();

        if (_type == SoundType.MUSIC) {
            _on = AudioManager.instance.IsMusicOn;

            if (_on) {
                _image.sprite = _onSprite;
            }
            else {
                _image.sprite = _offSprite;
            }
        }
        else if (_type == SoundType.EFFECT) {
            _on = AudioManager.instance.IsEffectsOn;

            if (_on) {
                _image.sprite = _onSprite;
            }
            else {
                _image.sprite = _offSprite;
            }
        }
    }



    public void EffectsOnOffButton() {
        AudioManager.instance.EffecsOnOffButton();
        _on = AudioManager.instance.IsEffectsOn;

        if (_on) {
            _image.sprite = _onSprite;
        }
        else {
            _image.sprite = _offSprite;
        }
    }

    public void MusicOnOffButton() {
        AudioManager.instance.MusicOnOffButton();
        _on = AudioManager.instance.IsMusicOn;

        if (_on) {
            _image.sprite = _onSprite;
        }
        else {
            _image.sprite = _offSprite;
        }
    }

    public void ButtonDown() {
        if (_on) {
            _image.sprite = _onDownSprite;
        }
        else {
            _image.sprite = _offDownSprite;
        }
    }


    private enum SoundType {
        MUSIC,
        EFFECT
    }

}
