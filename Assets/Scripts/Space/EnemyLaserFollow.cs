using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserFollow : MonoBehaviour {

    [SerializeField] private float _speed = 6f;

    private Transform _target;
    private ObjectPool _enemyLaserPool;
    private bool _initialized = false;



    private void Awake() {
        _enemyLaserPool = this.transform.parent.GetComponent<ObjectPool>();
    }

    private void Start() {
        _initialized = true;
    }


    private void Update() {
        transform.position += this.transform.up * _speed * Time.deltaTime;
    }


    public void SetTarget(Transform t) {
        this._target = t;
        transform.up = this._target.position - this.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("DestroyEnemy")) {
            this.gameObject.SetActive(false);
        }
    }


    private void OnDisable() {
        if (!_initialized) return;

        _enemyLaserPool.ReturnObject(this.gameObject);
    }


}
