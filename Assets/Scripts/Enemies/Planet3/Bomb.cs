using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    public ObjectPool bombPool, explosionPool;

    [SerializeField] private float _speed = 9f;

    private Vector2[] _wayPoints;
    private int _wayPointIndex = 0;
    private bool _initialized = false;



    private void Start() {
        _initialized = true;
    }


    private void Update() {
        FollowPath();
    }

    private void FollowPath() {
        transform.position = Vector2.MoveTowards(transform.position, _wayPoints[_wayPointIndex], _speed * Time.deltaTime);

        if ((Vector2)transform.position == _wayPoints[_wayPointIndex]) {
            _wayPointIndex++;

            if (_wayPointIndex == _wayPoints.Length) {
                if (this.gameObject.activeSelf)
                    Explode();
                _wayPointIndex = 0;
            }
        }
    }



    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            Explode();
        }
    }

    public void CreateWaypoints(Vector2[] wayPoints) {
        this._wayPoints = wayPoints;
        _wayPointIndex = 0;
        transform.position = wayPoints[_wayPointIndex];
    }

    public void Explode() {
        GameObject explosion = explosionPool.GetObject();
        explosion.transform.position = this.gameObject.transform.position;
        explosion.SetActive(true);
        this.gameObject.SetActive(false);
    }


    private void OnDisable() {
        if (!_initialized) return;

        bombPool.ReturnObject(this.gameObject);
    }


}
