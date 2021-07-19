using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astreoid : MonoBehaviour {

    public ObjectPool[] explosionPools;

    private GameObject _player;
    private float _speed;
    private int[] _degrees = { 0, 45, 90, 180, 270 };
    private ObjectPool _astreoidPool;
    private bool _canTakeDamage = false;
    private WaitForSeconds _waitForCanTakeDamage;
    private SpriteRenderer _sprite;

    private bool _initialized = false;



    private void Awake() {
        _sprite = GetComponent<SpriteRenderer>();
        _astreoidPool = this.transform.parent.GetComponent<ObjectPool>();
    }


    private void Start() {
        _initialized = true;
        _player = GameManager.instance.player;

        _waitForCanTakeDamage = new WaitForSeconds(1f);
    }


    private void Update() {
        transform.position -= new Vector3(0f, _speed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (_canTakeDamage && (other.gameObject.CompareTag("Laser") || other.gameObject.CompareTag("Player"))) {
            GameObject explosion = explosionPools[Random.Range(0, explosionPools.Length)].GetObject();
            explosion.transform.position = this.transform.position;
            explosion.SetActive(true);
            _canTakeDamage = false;

            this.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("DestroyEnemy")) {
            _canTakeDamage = false;
            this.gameObject.SetActive(false);
        }
    }

    private void OnBecameVisible() {
        StartCoroutine(EnableCollider());
    }

    private IEnumerator EnableCollider() {
        yield return _waitForCanTakeDamage;
        _canTakeDamage = true;
    }


    public void SetAstreoid() {
        _speed = Random.Range(1f, 2.25f);
        _sprite.color = new Color(Random.Range(0.65f, 1f), Random.Range(0.65f, 1f), Random.Range(0.65f, 1f));
        transform.rotation = Quaternion.Euler(0f, 0f, _degrees[Random.Range(0, _degrees.Length)]);
    }


    private void OnDisable() {
        if (!_initialized) return;

        _astreoidPool.ReturnObject(this.gameObject);
    }

}
