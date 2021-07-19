using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    [SerializeField] private ObjectPool _laserPool;
    [SerializeField] private Transform _laserPos;
    [SerializeField] private float _fireRate = 0.4f;

    private bool _canFire = true;
    private Animator _anim;
    private WaitForSeconds _waitForFireRate;
    private bool _fireButtonDown = false;


    private void Start() {
        _anim = GetComponent<Animator>();
        _waitForFireRate = new WaitForSeconds(_fireRate);
    }


    private void Update() {
        FireLogic();
    }



    private void FireLogic() {
        if (_fireButtonDown && _canFire) {
            Fire();
        }
    }


    public void Fire() {
        _anim.SetLayerWeight(1, 1f);

        GameObject laser = _laserPool.GetObject();
        laser.transform.position = _laserPos.position;
        laser.transform.rotation = _laserPos.rotation;

        laser.SetActive(true);
        _canFire = false;

        AudioManager.instance.PlaySound("LaserShoot");

        StartCoroutine(fireCoroutine());
    }

    private IEnumerator fireCoroutine() {
        yield return _waitForFireRate;
        _canFire = true;
    }

    public void FireButtonDown() {
        if (!GameManager.instance.isGamePaused) {
            _fireButtonDown = true;
        }
    }

    public void FireButtonUp() {
        if (!GameManager.instance.isGamePaused) {
            _anim.SetLayerWeight(1, 0f);
            _fireButtonDown = false;
        }
    }


    public void CanFireTrue() {
        StopAllCoroutines();
        _canFire = true;
    }
    public void CanFireFalse() {
        StopAllCoroutines();
        _canFire = false;
    }

}
