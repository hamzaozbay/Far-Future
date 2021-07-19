using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser1 : MonoBehaviour {

    [SerializeField] private float _speed = 7f;

    private ObjectPool _enemyLaserPool;
    private bool _initialized = false;



    private void Awake() {
        _enemyLaserPool = this.transform.parent.GetComponent<ObjectPool>();
    }

    private void Start() {
        _initialized = true;
    }


    private void Update() {
        transform.position += this.transform.right * _speed * Time.deltaTime;
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
