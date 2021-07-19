using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabBossBomb : MonoBehaviour {

    public ObjectPool explosionPool;

    [SerializeField] private float _speed = 8f;
    
    private ObjectPool _pool;
    private Rigidbody2D _rb;
    private bool _canGo = true;
    private AudioSource _audioSource;
    private bool _initialized = false;



    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
        _pool = this.transform.parent.GetComponent<ObjectPool>();
        _rb = this.GetComponent<Rigidbody2D>();
    }


    private void Start() {
        _initialized = true;
    }


    private void Update() {
        if (_canGo) {
            transform.position += transform.up * _speed * Time.deltaTime;
        }
    }



    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("CrabBombTrigger")) {
            _canGo = false;
            Invoke("DropBomb", Random.Range(2f, 4f));
        }
        else if (other.gameObject.CompareTag("Ground")) {
            GameObject explosion = explosionPool.GetObject();
            explosion.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - .55f);
            this.gameObject.SetActive(false);
            explosion.SetActive(true);
        }
    }


    private void DropBomb() {
        this.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        float targetPosX = GameManager.instance.player.transform.position.x + Random.Range(-4, 4);
        targetPosX = Mathf.Clamp(targetPosX, 240f, 250f);
        this.transform.position = new Vector3(targetPosX, transform.position.y, 0f);
        _canGo = true;
    }


    private void OnEnable() {
        if (_rb != null) {
            _rb.isKinematic = true;
            _canGo = true;
        }

        if (!AudioManager.instance.IsEffectsOn) {
            _audioSource.enabled = false;
        }
        else {
            _audioSource.enabled = true; ;
        }
    }


    private void OnDisable() {
        if (!_initialized) return;

        _pool.ReturnObject(this.gameObject);
    }

}
