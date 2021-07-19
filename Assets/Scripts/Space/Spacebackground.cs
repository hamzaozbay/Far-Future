using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spacebackground : MonoBehaviour {

    public float speed = 2f;

    [SerializeField] private float _minYPosition;
    [SerializeField] private float _maxYPosition;


    void Update() {
        transform.position -= new Vector3(0, speed * Time.deltaTime);

        if (transform.position.y <= _minYPosition) {
            transform.position = new Vector2(transform.position.x, _maxYPosition);
        }
    }


}
