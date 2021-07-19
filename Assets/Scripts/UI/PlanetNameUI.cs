using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlanetNameUI : MonoBehaviour {

    [SerializeField] private GameObject _planetNamePrefab;
    [SerializeField] private Transform _target;

    private Transform _ui;
    private TypeEffect _effect;
    private TextMeshProUGUI _textMesh;
    private string _planetName = "";



    private void Start() {
        foreach (Canvas c in FindObjectsOfType<Canvas>()) {
            if (c.renderMode == RenderMode.WorldSpace) {
                _ui = Instantiate(_planetNamePrefab, c.transform).transform;
                _effect = _ui.GetComponent<TypeEffect>();
                _textMesh = _ui.GetComponent<TextMeshProUGUI>();
                break;
            }
        }
    }



    private void LateUpdate() {
        _ui.position = _target.position;
    }


    public void SetName(string planetName) {
        this._planetName = planetName;
    }

    public void TypeName() {
        _textMesh.text = "Planet: " + _planetName;
        _effect.TypeText();
    }


    public Transform Target { get { return _target; } }

}