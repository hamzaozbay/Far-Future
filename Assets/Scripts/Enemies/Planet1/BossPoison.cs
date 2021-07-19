using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPoison : MonoBehaviour {

    public Vector2[] wayPoints;
    
    [SerializeField] private float _speed = 9f;
    [SerializeField] private float _stayDuration = 10f;

    private int _wayPointIndex = 0;
    private ObjectPool _pool;
    private Animator _anim;
    private bool _follow = true;
    private bool _initialized = false;



    private void Awake() {
        _pool = this.transform.parent.GetComponent<ObjectPool>();
        _anim = this.GetComponent<Animator>();
    }


    private void Start() {
        _initialized = true;
    }


    private void Update() {
        if (_follow) {
            FollowPath();
        }
    }


    private void FollowPath() {
        transform.position = Vector2.MoveTowards(transform.position, wayPoints[_wayPointIndex], _speed * Time.deltaTime);

        if ((Vector2)transform.position == wayPoints[_wayPointIndex]) {
            _wayPointIndex++;

            if (_wayPointIndex == wayPoints.Length) {
                if (this.gameObject.activeSelf) {
                    _anim.Play("PoisonBlob", 0, 0f);
                    _follow = false;
                }

                _wayPointIndex = 0;
            }
        }
    }


    private void BlobStay() {
        _anim.Play("PoisonBlobStay", 0, 0f);
        Invoke("BlobFadeOut", _stayDuration);
    }

    private void BlobFadeOut() {
        _anim.Play("PoisonFadeOut", 0, 0f);
    }

    private void DisableObject() {
        this.gameObject.SetActive(false);
    }



    private void OnDisable() {
        if (!_initialized) return;

        _pool.ReturnObject(this.gameObject);
    }

    private void OnEnable() {
        if (!_initialized) return;

        _anim.Play("Poison", 0, 0f);
        _follow = true;
    }

}
