using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeButtonSprite : MonoBehaviour {

    [SerializeField] private Sprite upSprite;
    [SerializeField] private Sprite downSprite;

    private Sprite _activeSprite;
    private Image _buttonImage;



    private void Start() {
        _activeSprite = upSprite;
        _buttonImage = GetComponent<Image>();
    }


    public void Change() {
        if (_activeSprite == upSprite) {
            _activeSprite = downSprite;
            _buttonImage.sprite = _activeSprite;
        }
        else {
            _activeSprite = upSprite;
            _buttonImage.sprite = _activeSprite;
        }
    }



}
