using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingPlant : Enemy {

    public ObjectPool bulletPool;

    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _fireRate;

    private float _time = 0f;



    protected override void Start() {
        base.Start();

        _time = _fireRate - AnimationLength.ClipLength(anim, "Grow");
    }


    private void Update() {
        if (canMove && !dead) {
            _time += Time.deltaTime;

            if (_time >= _fireRate) {
                RotateToPlayer();
                anim.Play("attack", 0, 0f);
                _time = 0f;
            }
        }
    }


    private void Idle() {
        anim.Play("idle", 0, 0f);
    }

    private void Fire() {
        GameObject bullet = bulletPool.GetObject();
        bullet.transform.position = _firePoint.position;
        bullet.transform.right = _firePoint.right;
        bullet.SetActive(true);
    }



}
