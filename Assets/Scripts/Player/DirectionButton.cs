using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionButton : MonoBehaviour {

    [SerializeField] private DirectionType _axis;
    private bool _down = false;
    private float _direction;


    private void Awake() {
        if (_axis == DirectionType.LEFT) {
            GameManager.instance.leftButton = this;
        }
        else {
            GameManager.instance.rightButton = this;
        }
    }


    public void Move() {
        if (!_down) {
            _direction = (int)_axis;
            _down = true;
        }
        else {
            _direction = 0f;
            _down = false;
        }
    }


    public float Direction { get { return _direction; } }

    private enum DirectionType {
        LEFT = -1,
        RIGHT = 1
    }

}
